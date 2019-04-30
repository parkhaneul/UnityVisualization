Shader "Unlit/vColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : Color
            {
                fixed PI = 3.14159265359;

                //Angle gradient
                fixed aGrad = (atan2(1 - i.uv.x - 0.5, 1 - i.uv.y - 0.5) + PI) / (2 * PI);
                fixed ang = aGrad * PI * 2;

                //Calculate hue
                fixed4 cWheel = 1;
                
                cWheel.r = clamp(2/PI * asin(cos(ang)) * 1.5 + 0.5, 0, 1);
                cWheel.g = clamp(2/PI * asin(cos(2 * PI * (1.0/3.0) - ang)) * 1.5 + 0.5, 0, 1);
                cWheel.b = clamp(2/PI * asin(cos(2 * PI * (2.0/3.0) - ang)) *  1.5 + 0.5, 0, 1);
                
                return cWheel;
            }
            ENDCG
        }
    }
}
