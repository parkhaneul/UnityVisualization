Shader "Custom/NewSurfaceShader" {
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
			};

			struct PS_INPUT {
				float4 position : SV_POSITION;
				float4 color : COLOR;
			};

			StructuredBuffer<Particle> particleBuffer;

			PS_INPUT vert(uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID) {
				PS_INPUT o = (PS_INPUT)0;

				o.color = float4(particleBuffer[instance_id].position, 1.0f);
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
