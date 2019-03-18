using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRunCompute : MonoBehaviour
{
	struct Particle
	{
		public Vector3 position;
		public Vector3 dir;
		public float velocity;
		public float life;
	}

	public Material material;
	public ComputeShader computeShader;
	
	private int particleCount = 100000;
	private int mComputeShaderKernelID;
	private ComputeBuffer particleBuffer;
	private int mWarpCount;

	private const int SIZE_PARTICLE = 32;
	private const int WARP_SIZE = 256;

	private int index = 0;

	private void Start()
	{
		InitComputeShader();
	}
	void InitComputeShader()
	{
		mWarpCount = Mathf.CeilToInt((float)particleCount / WARP_SIZE);

		Particle[] particleArray = new Particle[particleCount];

		for (int i = 0; i < particleCount; i++)
		{
			particleArray[i].position = new Vector3(0, 0, 0);
			particleArray[i].dir = (new Vector3(Random.Range(-1f, 1f), 
				Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
			particleArray[i].velocity = Random.Range(3f, 10f);
			particleArray[i].life = 0;//Random.value * 2.0f;
		}

		particleBuffer = new ComputeBuffer(particleCount, SIZE_PARTICLE);
		particleBuffer.SetData(particleArray);

		mComputeShaderKernelID = computeShader.FindKernel("MyCSParticle");

		computeShader.SetBuffer(mComputeShaderKernelID, "particleBuffer", particleBuffer);
		material.SetBuffer("particleBuffer", particleBuffer);
	}

	private void OnRenderObject()
	{
		material.SetPass(0);
		Graphics.DrawProcedural(MeshTopology.Points, 1, particleCount);
	}

	private void OnDestroy()
	{
		if (particleBuffer != null) particleBuffer.Release();
	}

	private void Update()
	{
		computeShader.SetFloat("deltaTime", Time.deltaTime);
		computeShader.Dispatch(mComputeShaderKernelID, mWarpCount, 1, 1);//어떠한 스레드를 몇개 써서 실행시킬지
		
		if (Input.GetMouseButtonDown(0))
		{
			if (index == 10) index = 0;
			computeShader.SetInt("index", index++);
			computeShader.SetBool("clicked", true);
		}
		else
		{
			computeShader.SetBool("clicked", false);
		}
		
	}
}
