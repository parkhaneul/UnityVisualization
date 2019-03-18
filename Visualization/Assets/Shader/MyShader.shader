Shader "Custom/MyShader" {
	Properties{
		_Main("MainColor", Color) = (1, 1, 1, 1)
		_Sub("SubColor", Color) = (0, 0, 0, 0)
	}
	SubShader{
		Pass{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Blend SrcAlpha one

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#pragma target 5.0

	struct Particle
	{
		float3 position;
		float3 dir;
		float velocity;
		float life;
	};

	struct PS_INPUT {
		float4 position : SV_POSITION;
		float4 color : COLOR;
		float life : LIFE;
	};

	StructuredBuffer<Particle> particleBuffer;
	float4 _Main;
	float4 _Sub;

	PS_INPUT vert(uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID)
	{
		PS_INPUT o = (PS_INPUT)0;

		float life = particleBuffer[instance_id].life;
		float lerpVal = life * 0.5f;
		o.color = _Main * (1 - lerpVal) + _Sub * lerpVal + float4(0,0,0,-(1-lerpVal));
		o.position = UnityObjectToClipPos(float4(particleBuffer[instance_id].position, 1.0f));

		return o;
	}

	float4 frag(PS_INPUT i) : COLOR
	{
		return i.color;
	}
		ENDCG
	}
	}
		FallBack Off
}
