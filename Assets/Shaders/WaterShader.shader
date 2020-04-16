// // Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Shader "Custom/WaterShader"
// {
//     Properties
//     {
//         _MainTex("Texture", 2D) = "white" {}
//         _BumpMap("Normal Map", 2D) = "bump" {}
//         _WaveFrequency("Wave Frequency", Float) = 1.0
//         _WaveAmplitude("Wave Amplitude", Float) = 0.25
//         _WaterScrollSpeed("Water Scroll Speed", Float) = 1.0
//     }
//         SubShader
//         {
//             Tags { "RenderType" = "Opaque" }
//             LOD 100
//             Pass
//             {
//                 CGPROGRAM
//                 // Physically based Standard lighting model, and enable shadows on all light types
//                 #pragma vertex vert
//                 #pragma fragment frag
//                 #pragma multi_compile_fog 
//                 #include "UnityCG.cginc" 
//                 struct v2f
//                 {
//                     float3 worldPos : TEXCOORD0;
//                     half3 tspace0 : TEXCOORD1;
//                     half3 tspace1 : TEXCOORD2;
//                     half3 tspace2 : TEXCOORD3;
//                     float2 uv : TEXCOORD4;
//                     float4 pos : SV_POSITION;
//                     UNITY_FOG_COORDS(1)
//                     };
//                     sampler2D _MainTex;
//                     float4 _MainTex_ST;
//                     sampler2D _BumpMap;
//                     float4 _BumpMap_ST;
//                     float _WaveFrequency;
//                     float _WaveAmplitude;
//                     float _WaterScrollSpeed;
//                     v2f vert(float4 vertex : POSITION, float3 normal : NORMAL, float4 tangent : TANGENT, float2 uv : TEXCOORD0)
//                     {
//                         v2f o;
//                         vertex += float4(0, sin((_Time.y + vertex.x + vertex.z) * _WaveFrequency) * _WaveAmplitude, 0, 0);
//                         o.pos = UnityObjectToClipPos(vertex);
//                         o.worldPos = mul(unity_ObjectToWorld, vertex).xyz;
//                         half3 wNormal = UnityObjectToWorldNormal(normal);
//                         half3 wTangent = UnityObjectToWorldDir(tangent.xyz);
//                         half tangentSign = tangent.w * unity_WorldTransformParams.w;
//                         half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
//                         o.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
//                         o.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
//                         o.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);
//                         o.uv = uv;
//                         UNITY_TRANSFER_FOG(o,o.vertex);
//                         return o;
//                     }
//                      fixed4 frag(v2f i) : SV_Target
//                      {
//                          half3 tnormal = UnpackNormal(tex2D(_BumpMap, i.uv + float2(_Time.y, 0)  * _WaterScrollSpeed));
//                          half3 worldNormal;
//                          worldNormal.x = dot(i.tspace0, tnormal);
//                          worldNormal.y = dot(i.tspace1, tnormal);
//                          worldNormal.z = dot(i.tspace2, tnormal);
//                          half3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
//                          half3 worldRefl = reflect(-worldViewDir, worldNormal);
//                          half4 skyData = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
//                          half3 skyColor = DecodeHDR (skyData, unity_SpecCube0_HDR);                
//                          fixed4 col = 0;
//                          col.rgb = skyColor;
//                          fixed3 wCol = tex2D(_MainTex, i.uv + float2(_Time.y, 0)  * _WaterScrollSpeed);
//                          col.rgb *= wCol;
//                          return col;
//                      }
//                      ENDCG
//                  }
//         }
// }

Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _WaveFrequency ("Wave Frequency", Float) = 1.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.25
        _WaterScrollSpeed ("Water Scroll Speed", Float) = 1.0
        _Illumi ("Illumi Color", color) = (1, 1, 1, 1)
        _EmissionLM ("Emission(Lightmapper)", Float) = 1
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
                half3 worldNormal : TEXCOORD1;
                // half3 tspace0 : TEXCOORD1; // tangent.x, bitangent.x, normal.x
                // half3 tspace1 : TEXCOORD2; // tangent.y, bitangent.y, normal.y
                //  half3 tspace2 : TEXCOORD3; // tangent.z, bitangent.z, normal.z
                //  /*half diffuse : TEXCOORD1;
                //  half night : TEXCOORD2;*/
                //  half3 viewDir : TEXCOORD4;
                //  half3 normalDir : TEXCOORD5;
            };
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            float _WaveFrequency;
            float _WaveAmplitude;
            float _WaterScrollSpeed;
            v2f vert (appdata v, float3 normal : NORMAL)
            {
                v2f o;
                v.vertex += float4(0, sin((_Time.y + v.vertex.x + v.vertex.z) * _WaveFrequency) * _WaveAmplitude, 0, 0);
                o.vertex = UnityObjectToClipPos(v.vertex);
                half3 wNormal = UnityObjectToWorldNormal(normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.worldNormal = UnityObjectToWorldNormal(normal);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target //affects the color / texture of the mesh
            {   
                half4 bump = tex2D(_BumpMap, i.worldNormal);
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv + float2(_Time.y, 0)  * _WaterScrollSpeed);
               // apply fog                    UNITY_APPLY_FOG(i.fogCoord, col);
                col *= bump * 2;
                return col;
            }
            ENDCG
        }
        
    }
}