using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareParticle : MonoBehaviour {

    public ComputeShader computeShader;
    public ComputeBuffer particleBuffer;
    private int mComputeShaderKernelID;

    public Material material;

    struct Particle
    {
        public Vector3 position;
    };

	// Use this for initialization
	void Start () {
        Init();
	}

    void Init()
    {
        Particle[] particleArray = new Particle[1000];

        for(int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    particleArray[100 * x + 10 * y + z].position = new Vector3(x, y, z);
                }
            }
        }

        particleBuffer = new ComputeBuffer(1000, 12);
        particleBuffer.SetData(particleArray);
        mComputeShaderKernelID = computeShader.FindKernel("CSParticle");
        computeShader.SetBuffer(mComputeShaderKernelID, "particleBuffer", particleBuffer);
        material.SetBuffer("particleBuffer", particleBuffer);
    }

    private void OnDestroy()
    {
        if (particleBuffer != null)
            particleBuffer.Release();
    }

    void OnRenderObject()
    {
        material.SetPass(0);
        Graphics.DrawProcedural(MeshTopology.Points, 1, 1000);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
