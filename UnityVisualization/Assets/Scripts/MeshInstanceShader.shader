Shader "Custom/MeshInstanceShader"
{
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        Pass {
            Cull Off
            Lighting Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5
            
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "AutoLight.cginc"

            sampler2D _MainTex;
            
            struct Particle{
                float4 _color;
                float3 position;
            };
            
        #if SHADER_TARGET >= 45
            StructuredBuffer<Particle> particleBuffer;
        #endif
        
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv_MainTex : TEXCOORD0;
                float3 color : TEXCOORD3;
                SHADOW_COORDS(4)
            };
            
            v2f vert (appdata_full v, uint instanceID : SV_InstanceID)
            {
            #if SHADER_TARGET >= 45
                float3 position = particleBuffer[instanceID].position;
                float4 color = particleBuffer[instanceID]._color;
            #else
                float3 position = 0;
                float4 color = 0;
            #endif
                v2f o;
                float4 pos = v.vertex;
                
                float3 localPosition = v.vertex.xyz * 0.2;
                float3 worldPosition = position.xyz + localPosition;
                
                o.pos = mul(UNITY_MATRIX_VP,float4(worldPosition,1.0f));
                o.uv_MainTex = v.texcoord;
                o.color = v.color * color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //fixed shadow = SHADOW_ATTENUATION(i);
                fixed4 albedo = tex2D(_MainTex, i.uv_MainTex * 0.5 + 0.5);
                //float3 lighting = i.diffuse * shadow + i.ambient;
                fixed4 output = fixed4(albedo.rgb * i.color, albedo.w);
                //UNITY_APPLY_FOG(i.fogCoord, output);
                return output;
            }
            ENDCG
        }
    }
}

