Shader "Custom/FireSurfaceShader" {
	Properties {
		_Main ("MainColor", Color) = (1,1,1,1)
		_Sub ("SubColor", Color) = (1,1,1,1)
	}
		SubShader{
			Pass{
				Tags { "RenderType" = "Opaque" }
				LOD 200
				Blend SrcAlpha one

				CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 5.0

		struct Particle {
			float3 position;
			float3 direction;
			float life;
		};

		struct PS_INPUT {
			float4 position : SV_POSITION;
			float4 color : COLOR;
			float life : LIFE;
		};

		float4 _Main;
		float4 _Sub;
		StructuredBuffer<Particle> computeBuffer;

		PS_INPUT vert(uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID) {
			PS_INPUT o = (PS_INPUT)0;

			float life = computeBuffer[instance_id].life;
			float4 color = _Sub * (1 - life * 0.3) + _Main * (life * 0.3);
			o.color = float4(color.x, color.y, color.z, life * 0.25);
			o.position = UnityObjectToClipPos(float4(computeBuffer[instance_id].position, 1.0f));

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
