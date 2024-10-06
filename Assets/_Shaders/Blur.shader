Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _TexelSize ("Texel Size", Vector) = (1,1,0,0)
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _TexelSize;
            float _BlurSize;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                float2 texel = _TexelSize * _BlurSize;
                half4 color = half4(0,0,0,0);
                
                // Gaussian weights
                float weights[5] = {0.204, 0.304, 0.304, 0.204, 0.102}; // Approximation of Gaussian weights
                float2 offsets[5] = { float2(-2, 0), float2(-1, 0), float2(0, 0), float2(1, 0), float2(2, 0) };
                
                for (int j = 0; j < 5; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        float2 offset = offsets[j] * texel + offsets[k] * texel;
                        color += tex2D(_MainTex, i.uv + offset) * weights[j] * weights[k];
                    }
                }
                
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
