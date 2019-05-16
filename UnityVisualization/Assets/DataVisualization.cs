using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualization : MonoBehaviour
{
    public ComputeBuffer computeBuffer;
    public ComputeBuffer axisBuffer;
    public ComputeShader computeShader;
    private int mComputeShaderKernelID;

    public Material material;

    private int maxParticle;
    private int particleSize;

    private int axisParticle;
    private int axisSize = 40 + 40 + 12 + 16;

    // Start is called before the first frame update
    void Start()
    {
        maxParticle = FileReader.dataList.Length;
        particleSize = 16 + 12 + 2000;
        mComputeShaderKernelID = computeShader.FindKernel("dataVisualization");

        computeBuffer = new ComputeBuffer(maxParticle,particleSize);
        computeBuffer.SetData(FileReader.dataList);
        computeShader.SetBuffer(mComputeShaderKernelID, "computeBuffer", computeBuffer);
        material.SetBuffer("computeBuffer", computeBuffer);
        material.SetInt("axisCount", axisParticle);
    }

    void OnRenderObject()
    {
        material.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Points, 1, maxParticle);
    }

    void OnDestroy()
    {
        if (computeBuffer != null)
            computeBuffer.Release();
    }

    void changeAxis()
    {
        var manager = AxisDataManager.Instance();
        axisBuffer = new ComputeBuffer(axisParticle, axisSize);
        computeShader.SetInt("axisCount", axisParticle);
        axisBuffer.SetData(manager.GetAxisAll());
        computeShader.SetBuffer(mComputeShaderKernelID, "axisBuffer", axisBuffer);
    }

    public void Update()
    {
        changeAxis();
        computeShader.Dispatch(mComputeShaderKernelID, 512, 1, 1);
    }
}
