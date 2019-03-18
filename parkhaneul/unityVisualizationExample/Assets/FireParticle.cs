using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour {

    public ComputeShader computeShader;
    public ComputeBuffer computeBuffer;
    private int mComputeShaderKernelID;

    public Material material;

    public int maxParticle;
    public float volumeValue;
    private Vector2 cursorPos;

    struct Particle {
        public Vector3 position;
        public float life;
    };


	// Use this for initialization
	void Start () {
        init();
	}

    void init()
    {
        Particle[] particleArray = new Particle[maxParticle];

        for (int i = 0; i < maxParticle; i++){
            float x = Random.value * volumeValue;
            float y = Random.value * volumeValue;
            float z = Random.value * volumeValue;
            Vector3 xyz = new Vector3(x, y, z);
            xyz.Normalize();
            xyz *= Random.value;

            particleArray[i].position = xyz;
            particleArray[i].life = Random.value * volumeValue * 0.1f;
        }

        computeBuffer = new ComputeBuffer(maxParticle, 16);
        computeBuffer.SetData(particleArray);
        mComputeShaderKernelID = computeShader.FindKernel("FireParticle");
        computeShader.SetBuffer(mComputeShaderKernelID, "computeBuffer", computeBuffer);
        material.SetBuffer("computeBuffer", computeBuffer);
    }

    void OnRenderObject()
    {
        material.SetPass(0);
        Graphics.DrawProcedural(MeshTopology.Points, 1,maxParticle);
    }

    void OnDestroy()
    {
        if (computeBuffer != null)
            computeBuffer.Release();
    }

    // Update is called once per frame
    void Update () {
        float[] mousePosition2D = { cursorPos.x, cursorPos.y };

        computeShader.SetFloat("deltaTime", Time.deltaTime);
        computeShader.Dispatch(mComputeShaderKernelID, 256, 1, 1);
	}
}
