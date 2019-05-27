using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualization : MonoBehaviour
{
    public ComputeBuffer computeBuffer;
    public ComputeShader computeShader;
    private int mComputeShaderKernelID;

    public Material material;
    public Mesh mesh;

    private int maxParticle;
    private int particleSize = 16 + 12;

    //private int dataSize = 4;

    private int axisParticle;
    private int axisSize = 12 + 16;

    private int weightParticle;
    private int weightSize = 40 + 40;

    // Start is called before the first frame update
    void Start()
    {
        maxParticle = FileReader.dataList.Length+1;
        mComputeShaderKernelID = computeShader.FindKernel("dataVisualization");
        computeBuffer = new ComputeBuffer(maxParticle, particleSize);
        computeBuffer.SetData(FileReader.dataList);

        ComputeBuffer floatBuffer = new ComputeBuffer(maxParticle * 50, 4);
        floatBuffer.SetData(FileReader.floatList);
        computeShader.SetBuffer(mComputeShaderKernelID, "floatBuffer", floatBuffer);
        computeShader.SetBuffer(mComputeShaderKernelID, "computeBuffer", computeBuffer);

        material.SetBuffer("computeBuffer", computeBuffer);
    }

    void OnRenderObject()
    {
        material.SetPass(0);
        //Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
        //Graphics.DrawProceduralNow(MeshTopology.Points, 1, maxParticle);
    }

    void OnDestroy()
    {
        if (computeBuffer != null)
            computeBuffer.Release();
    }

    public void changeAxis()
    {
        var manager = AxisDataManager.Instance();

        axisParticle = manager.GetAxisSampleCount();
        computeShader.SetInt("axisCount", axisParticle);

        var axisData = manager.GetAxisSamples();
        ComputeBuffer axisBuffer = new ComputeBuffer(axisParticle, axisSize);
        axisBuffer.SetData(axisData);

        var weightData = manager.GetSampleWeights();
        weightParticle = weightData.Length;
        ComputeBuffer weightBuffer = new ComputeBuffer(weightParticle, weightSize);
        weightBuffer.SetData(weightData);

        computeShader.SetBuffer(mComputeShaderKernelID, "weightBuffer", weightBuffer);
        computeShader.SetBuffer(mComputeShaderKernelID, "axisBuffer", axisBuffer);
        computeShader.Dispatch(mComputeShaderKernelID, 8, 8, 8);
        material.SetBuffer("computeBuffer", computeBuffer);
    }
}
