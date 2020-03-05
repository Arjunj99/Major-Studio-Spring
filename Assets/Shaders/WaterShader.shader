Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveFrequency ("Wave Frequency", Float) = 1.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.25
        _WaterScrollSpeed ("Water Scroll Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog 

            #include "UnityCG.cginc" 

            struct appdata
            {
                float4 vertex : POSITION; //float4 = Vector4
                float2 uv : TEXCOORD0; //float2 = Vector2
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _WaveFrequency;
            float _WaveAmplitude;
            float _WaterScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex += float4(0, sin((_Time.y + v.vertex.x + v.vertex.z) * _WaveFrequency) * _WaveAmplitude, 0, 0);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target //affects the color / texture of the mesh
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + float2(_Time.y, 0)  * _WaterScrollSpeed);
               // apply fog                    UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
