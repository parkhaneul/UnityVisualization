using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCompute : MonoBehaviour
{
	struct Particle
	{
		public Vector3 position;
		public Vector3 velocity;
		public float life;
	}

	public Material material;
	public ComputeShader computeShader;

	private Vector2 cursorPos;
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

		for(int i = 0; i < particleCount; i++)
		{
			float x = Random.value * 2 - 1.0f;
			float y = Random.value * 2 - 1.0f;
			float z = Random.value * 2 - 1.0f;

			Vector3 xyz = new Vector3(x, y, z);
			xyz.Normalize();
			xyz *= Random.value * 0.5f;

			particleArray[i].position = xyz;
			particleArray[i].velocity = Vector3.zero;
			particleArray[i].life = Random.value * 5.0f + 1.0f;
		}

		particleBuffer = new ComputeBuffer(particleCount, SIZE_PARTICLE);
		particleBuffer.SetData(particleArray);

		mComputeShaderKernelID = computeShader.FindKernel("CSParticle");

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
		float[] mousePosition2D = { cursorPos.x, cursorPos.y };
		computeShader.SetFloat("deltaTime", Time.deltaTime);
		computeShader.SetFloats("mousePosition", mousePosition2D);
		computeShader.Dispatch(mComputeShaderKernelID, mWarpCount, 1, 1);
	}

	private void OnGUI()
	{
		Vector3 p = new Vector3();
		Camera c = Camera.main;
		Event e = Event.current;
		Vector2 mousePos = new Vector2();

		mousePos.x = e.mousePosition.x;
		mousePos.y = c.pixelHeight - e.mousePosition.y;

		p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane + 14));

		cursorPos.x = p.x;
		cursorPos.y = p.y;
	}
}
