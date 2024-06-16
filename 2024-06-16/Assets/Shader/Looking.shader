Shader "Unlit/Looking"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100

        Pass
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                
                // カメラの前方向ベクトルを取得
                float3 cameraDir = normalize(-UNITY_MATRIX_V[2].xyz);
                
                // 回転角度を計算
                float angle = atan2(cameraDir.x, cameraDir.z);

                // UV座標をテクスチャの中心に移動
                float2 centeredUV = v.uv - 0.5;

                // UV座標を回転
                float2x2 rotationMatrix = float2x2(cos(angle), -sin(angle), sin(angle), cos(angle));
                float2 rotatedUV = mul(rotationMatrix, centeredUV);

                o.uv = rotatedUV + 0.5;
                return o;
            }

            float4 frag(Varyings i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDHLSL
        }
    }
}
