using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualization : MonoBehaviour
{
    private ComputeBuffer particleBuffer; // 그려줄 정보 버퍼
    private ComputeBuffer argsBuffer; // 랜더링 정보 버퍼
    private ComputeBuffer dataBuffer; // float 데이터 버퍼
    private ComputeBuffer axisBuffer; // 축 데이터 버퍼
    private ComputeBuffer weightBuffer; // 축 연산 데이터 버퍼
    private ComputeBuffer clusterBuffer; // 군집화 데이터 버퍼

    public ComputeShader computeShader; // 연산 쉐이더
    private int mComputeShaderKernelID;
    private int clusteringKernelID;

    public Material material;
    public int radius;
    private Mesh mesh;

    private int maxParticle;
    private int particleSize = 16 + 12;

    private int maxAxis;
    private int axisSize = 12 + 16;

    private int maxWeight;
    private int weightSize = 40 + 40;

    private int expectedColumn = 50;

    private static bool isChange = false;
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    private int clusterSize = 16 + 12 + 12 + 4;
    private int maxCluster = 4;
    
    private float currentTime = 0;
    private float stackFPS = 0;
    private float fps = 0;
    // Start is called before the first frame update

    Cluster[] clusters;
    int times = 0;
    bool isClustered = false;

    void Start()
    {
        mesh = crossQuad();
        // 메시 그리.
        mComputeShaderKernelID = computeShader.FindKernel("dataVisualization");
        clusteringKernelID = computeShader.FindKernel("clustering");
        // 컴퓨트 쉐이더 사용 아이디 설정
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint));
        // 매쉬 랜더 버퍼 설정
        maxParticle = FileReader.dataList.Length+1;
        // 파티클 표현 갯수 설정
        particleBuffer = new ComputeBuffer(maxParticle, particleSize);
        particleBuffer.SetData(FileReader.dataList);
        // 파티클 정도를 넣을 버퍼 설정
        dataBuffer = new ComputeBuffer(maxParticle * expectedColumn, 4);
        dataBuffer.SetData(FileReader.floatList);
        // 연산에 필요한 데이터 버퍼 설정
        computeShader.SetBuffer(mComputeShaderKernelID, "floatBuffer", dataBuffer);
        computeShader.SetBuffer(mComputeShaderKernelID, "particleBuffer", particleBuffer);
        // 컴퓨트 쉐이더에 연산 데이터 전송

        if (mesh != null)
        {
            args[0] = (uint)mesh.GetIndexCount(0);
            args[1] = (uint)maxParticle;
            args[2] = (uint)mesh.GetIndexStart(0);
            args[3] = (uint)mesh.GetBaseVertex(0);
        }
        else
        {
            args[0] = args[1] = args[2] = args[3] = 0;
        }
        argsBuffer.SetData(args);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        currentTime += Time.deltaTime;
        if (currentTime > 1.0f)
        {
            stackFPS = fps * Time.deltaTime;
            fps = 0;
            currentTime = 0;
        }
        else
        {
            fps += 1 / Time.deltaTime;
        }

        if (maxParticle < 100000 || isChange)
        {
            if (!isClustered)
            {
                updateBuffer();
            }
            isChange = false;
        }
        material.SetBuffer("particleBuffer", particleBuffer);
        Graphics.DrawMeshInstancedIndirect(mesh, 0, material, new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), argsBuffer);
        // 연산된 데이터에 맞게 다수의 매쉬를 화면에 표현.
    }
    
    public void cluster()
    {
        if (!isClustered)
        {
            var temp = new Particle[maxParticle];
            clusterBuffer = new ComputeBuffer(maxCluster, clusterSize);
            particleBuffer.GetData(temp);
            clusters = new Cluster[maxCluster];
            Random.seed = 1023;
            for (int i = 0; i < maxCluster; i++)
            {
                var random = Random.Range(0, maxParticle);
                clusters[i].lastPosition = temp[random].position;
                clusters[i]._color = Color.HSVToRGB(i * 1.0f / maxCluster, 1, 1);
            }
            computeShader.SetInt("kIndex", maxCluster);
            computeShader.SetBuffer(clusteringKernelID, "particleBuffer", particleBuffer);

            isClustered = true;
            StartCoroutine("clusterCoroutine");
        }
        else
        {
            isClustered = false;
        }
        /*
         * Random seed init                                                     o
         * 
         * for 0...k --> cluster = particle[random]                             o
         * 
         * do{
         *      distance(particle, cluster)
         *          -- clustering...
         *      cluster repositioning.
         *  }(while(cluster repositioning i-1 != cluster repositioning i)       o
         * 
         * partilce[id].color = cluster[partilce[id].index].color   
         * 
         * https://en.wikipedia.org/wiki/K-means%2B%2B*/
    }

    IEnumerator clusterCoroutine()
    {
        int index = 0;
        clusterBuffer.SetData(clusters);
        computeShader.SetBuffer(clusteringKernelID, "clusterBuffer", clusterBuffer);
        computeShader.Dispatch(clusteringKernelID, 32, 32,32);
        clusterBuffer.GetData(clusters);
        for(int i = 0; i < maxCluster; i++)
        {
            var position = clusters[i].position;
            Debug.Log(position);
            if (clusters[i].index > 0)
            {
                position /= clusters[i].index;
            }
            if (Vector3.Distance(clusters[i].lastPosition,position) <= 0.05f)
            {
                index++;
            }
            else
            {
                clusters[i].lastPosition = position;
            }
            clusters[i].position = new Vector3(0, 0, 0);
            clusters[i].index = 0;
        }
        times++;
        Debug.Log(times + " times & " + index + " clustering...");
        yield return new WaitForSeconds(0.2f);
        if (index < maxCluster - 1 && isClustered)
        {
            StartCoroutine("clusterCoroutine");
        }
        else {
            Debug.Log("Clustering end...");
        }
    }

    void updateBuffer() {

        var manager = AxisDataManager.Instance();
        var floats = FileReader.floatList;
        //데이터 접근
        var axisData = manager.GetAxisDatas();
        var weightData = manager.GetWeights();
        //데이터 받아오기
        maxAxis = axisData.Length + 1;
        computeShader.SetInt("axisCount", maxAxis);
        //데이터 갯수 설정
        axisBuffer = new ComputeBuffer(maxAxis, axisSize);
        axisBuffer.SetData(axisData);
        //axisBuffer 설정
        maxWeight = weightData.Length;
        weightBuffer = new ComputeBuffer(maxWeight, weightSize);
        weightBuffer.SetData(weightData);
        //weight 버퍼 설정
        computeShader.SetBuffer(mComputeShaderKernelID, "weightBuffer", weightBuffer);
        computeShader.SetBuffer(mComputeShaderKernelID, "axisBuffer", axisBuffer);
        //연산 쉐이더에 정보 주입
        computeShader.Dispatch(mComputeShaderKernelID, 32, 32, 1);

        if (mesh != null)
        {
            args[0] = (uint)mesh.GetIndexCount(0);
            args[1] = (uint)maxParticle;
            args[2] = (uint)mesh.GetIndexStart(0);
            args[3] = (uint)mesh.GetBaseVertex(0);
        }
        else
        {
            args[0] = args[1] = args[2] = args[3] = 0;
        }
        argsBuffer.SetData(args);
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width - 240, 5, 220, 30), "Instance Count : " + maxParticle.ToString());
        GUI.Label(new Rect(Screen.width - 240, 25, 220, 30), "expected Data column : " + expectedColumn.ToString());
        GUI.Label(new Rect(Screen.width - 240, 45, 220, 30), "Axis Count : " + (maxAxis-1).ToString());
        GUI.Label(new Rect(Screen.width - 240, 65, 220, 30), "Weight Count : " + maxWeight.ToString());
        GUI.Label(new Rect(Screen.width - 240, 85, 220, 30), "Frames per Seconds: " + stackFPS.ToString());

        GUI.Label(new Rect(Screen.width - 240, 150, 220, 30), "Alt + Left Drag : Rotate By Center");
        GUI.Label(new Rect(Screen.width - 240, 170, 220, 30), "Alt + Right Drag : Rotate By Camera");
        GUI.Label(new Rect(Screen.width - 240, 190, 220, 30), "Alt + Wheel : Zoom");

        /*
        if (isClustered)
        {
            for(int i = 1; i < maxCluster; i++)
            {
                GUI.contentColor = clusters[i]._color;
                GUI.Label(new Rect(Screen.width - 240, 220 + 60 * i, 220, 60), "Means \n" + getMeans(i));
            }
        }*/
    }
    /*
    string getMeans(int index)
    {
        var returnValue = "";
        var particles = new Particle[maxParticle];
        var floats = FileReader.floatList;
        clusterBuffer.GetData(clusters);
        particleBuffer.GetData(particles);

        for (int i = 0; i < MetaData.floatArray.Length; i++)
        {
            var temp = Header.data[MetaData.floatArray[i]]; //데이터 column 이름
            float means = 0;
            int value = 0;
            for(int j = 0; j < maxParticle; j++)
            {
                if(particles[j]._color == clusters[index]._color)
                {
                    means += floats[j * 50 + i];
                    value++;
                }
            }
            means = means / value;
            temp += " : " + means.ToString() + ", ";
            returnValue += temp;
        }
        return returnValue;
    }*/

    void OnDestroy()
    {
        if(particleBuffer != null)
        {
            particleBuffer.Release();
        }
    }

    public void changeAxis()
    {
        isChange = true;
    }

    Mesh crossQuad()
    {
        Mesh mesh = new Mesh();

        var vertice = new Vector3[12];
        var triangle = new int[18];
        var uv = new Vector2[12];

        vertice[0] = new Vector3(0, radius, -radius);
        vertice[1] = new Vector3(0, radius, radius);
        vertice[2] = new Vector3(0, -radius, radius);
        vertice[3] = new Vector3(0, -radius, -radius);
        vertice[4] = new Vector3(-radius, radius, 0);
        vertice[5] = new Vector3(radius, radius, 0);
        vertice[6] = new Vector3(radius, -radius, 0);
        vertice[7] = new Vector3(-radius, -radius, 0);
        vertice[8] = new Vector3(-radius, 0, radius);
        vertice[9] = new Vector3(radius, 0, radius);
        vertice[10] = new Vector3(radius, 0, -radius);
        vertice[11] = new Vector3(-radius, 0, -radius);

        triangle[0] = 0;
        triangle[1] = 1;
        triangle[2] = 2;
        triangle[3] = 0;
        triangle[4] = 2;
        triangle[5] = 3;
        triangle[6] = 4;
        triangle[7] = 5;
        triangle[8] = 6;
        triangle[9] = 4;
        triangle[10] = 6;
        triangle[11] = 7;
        triangle[12] = 8;
        triangle[13] = 9;
        triangle[14] = 10;
        triangle[15] = 8;
        triangle[16] = 10;
        triangle[17] = 11;

        for (int i = 0; i < 4; i++)
        {
            uv[i] = new Vector2(vertice[i].z, vertice[i].y);
        }
        for (int i = 4; i < 8; i++)
        {
            uv[i] = new Vector2(vertice[i].x, vertice[i].y);
        }
        for (int i = 8; i < 12; i++)
        {
            uv[i] = new Vector2(vertice[i].x, vertice[i].z);
        }

        mesh.vertices = vertice;
        mesh.triangles = triangle;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}