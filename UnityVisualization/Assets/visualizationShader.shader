﻿Shader "Custom/visualizationShader"
{
    SubShader
    {
        Pass{
            Tags { "RenderType"="Opaque" }
            LOD 200

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #pragma target 5.0

            struct Particle{
                float4 _color;
                float3 position;
            };
            
            struct PS_INPUT {
                float4 position : SV_POSITION;
                float4 color : COLOR;
            };
            
            StructuredBuffer<Particle> computeBuffer;
            
            PS_INPUT vert(uint vertex_id : SV_VertexID, uint instance_id : SV_InstanceID) {
                PS_INPUT o = (PS_INPUT)0;

                o.color = computeBuffer[instance_id]._color;
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
    FallBack "Diffuse"
}
