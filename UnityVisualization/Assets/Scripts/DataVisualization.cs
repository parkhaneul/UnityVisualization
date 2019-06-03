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

    public ComputeShader computeShader; // 연산 쉐이더
    private int mComputeShaderKernelID;

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

    private float currentTime = 0;
    private float stackFPS = 0;
    private float fps = 0;
    // Start is called before the first frame update
    void Start()
    {
        mesh = crossQuad();
        // 메시 그리.
        mComputeShaderKernelID = computeShader.FindKernel("dataVisualization");
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
            updateBuffer();
            isChange = false;
        }
        Graphics.DrawMeshInstancedIndirect(mesh, 0, material, new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), argsBuffer);
        // 연산된 데이터에 맞게 다수의 매쉬를 화면에 표현.
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
        computeShader.Dispatch(mComputeShaderKernelID, 1024, 1, 1);

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

        material.SetBuffer("particleBuffer", particleBuffer);
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
    }

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
