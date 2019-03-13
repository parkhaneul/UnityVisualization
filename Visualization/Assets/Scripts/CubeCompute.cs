using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCompute : MonoBehaviour
{
	struct Particle
	{
		public Vector3 position;
		public Vector4 color;
	}

	public Material material;
	public ComputeShader computeShader;
	
	private int particleCount = 1000000;
	private int mComputeShaderKernelID;
	private ComputeBuffer particleBuffer;
	private int mWarpCount;

	private const int SIZE_PARTICLE = 28;
	private const int WARP_SIZE = 256;

	private void Start()
	{
		InitComputeShader();
	}
	void InitComputeShader()
	{
		mWarpCount = Mathf.CeilToInt((float)particleCount / WARP_SIZE);

		Particle[] particleArray = new Particle[particleCount];
		int i = 0;
		for (int a = -50; a < 50; a++)
		{
			for (int b= -50; b < 50; b++)
			{
				for (int c = -50; c < 50; c++)
				{
					float x = a;
					float y = b;
					float z = c;

					Vector3 xyz = new Vector3(x, y, z);
					xyz.Normalize();
					xyz *= Random.value * 5f; // 산개!
					
					particleArray[i].position = xyz;
					particleArray[i].color = new Vector4((i+50)%100, (i + 50) % 100, (i + 50) % 100, 1);
					i++;
				}
			}
		}

		particleBuffer = new ComputeBuffer(particleCount, SIZE_PARTICLE);
		particleBuffer.SetData(particleArray);

		mComputeShaderKernelID = computeShader.FindKernel("CSCube");

		computeShader.SetBuffer(mComputeShaderKernelID, "particleBuffer", particleBuffer);
		material.SetBuffer("particleBuffer", particleBuffer);
	}

	private void OnRenderObject()
	{
		material.SetPass(0);
		Graphics.DrawProcedural(MeshTopology.Points, 1, particleCount);
	}
}
