Shader "Custom/JaggedExplosionShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ExplosionProgress ("Explosion Progress", Range(0, 1)) = 0.0
        _ExplosionPower ("Explosion Power", Range(0, 1)) = 0.5 
        _BaseColor ("Base Color", Color) = (1, 0.5, 0.5, 1)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float _ExplosionProgress;
            float _ExplosionPower;
            float4 _BaseColor;

            float rand(float2 co) {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert(appdata_t v) {
                v2f o;
                o.uv = v.uv;

                
                float explosionEffect = pow(_ExplosionProgress, 2.0);
                float baseDisplacement = _ExplosionPower * 0.1;
                float displacement = explosionEffect * baseDisplacement * (0.5 + rand(v.vertex.xy) * 0.5); 

                float3 jaggedOffset = normalize(v.normal + rand(v.vertex.xy) * 0.2) * displacement;
                v.vertex.xyz += jaggedOffset;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target {
                float4 texColor = tex2D(_MainTex, i.uv);
                float3 colorBlend = lerp(texColor.rgb, _BaseColor.rgb, _ExplosionProgress);
                texColor.rgb = colorBlend;
                texColor.a *= (1.0 - _ExplosionProgress);
                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
