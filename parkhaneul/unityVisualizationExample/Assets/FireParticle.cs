using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour {

    public ComputeShader computeShader;
    public ComputeBuffer computeBuffer;
    private int mComputeShaderKernelID;

    public Material material;

    public int maxParticle;
    public float life = 0;
    public float random = 0;
    public Vector3 volume = new Vector3(0,0,0);
    public Vector3 direction = new Vector3(0,0,0);
    public Vector3 position = new Vector3(0, 0, 0);

    struct Particle {
        public Vector3 position;
        public Vector3 direction;
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
            float x = position.x;
            float y = position.y;
            float z = position.z;
            Vector3 xyz = new Vector3(x, y, z);
            xyz.Normalize();
            xyz *= Random.value;

            particleArray[i].position = xyz;
            particleArray[i].direction = direction;
            particleArray[i].life = 2 * life;
        }

        computeBuffer = new ComputeBuffer(maxParticle, 28);
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
        computeShader.SetFloat("deltaTime", Time.deltaTime);
        computeShader.SetFloats("direction", direction.x,direction.y,direction.z);
        computeShader.SetFloats("volume",volume.x,volume.y,volume.z);
        computeShader.SetFloats("position", position.x, position.y, position.z);
        computeShader.SetFloat("random", random);
        computeShader.SetFloat("life", life);
        computeShader.Dispatch(mComputeShaderKernelID, 512, 1, 1);
	}
}
