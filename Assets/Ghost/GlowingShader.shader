Shader "Custom/GlowingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 1
        _Transparency ("Transparency", Range(0, 1)) = 0.8
        _FlattenAmount ("Flatten Amount", Float) = 0.1 
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // Shader Properties
            sampler2D _MainTex;
            float4 _GlowColor;
            float _GlowIntensity;
            float _Transparency;
            float _FlattenAmount;

            v2f vert (appdata v)
            {
                v2f o;

                float4 worldPos = v.vertex;
                worldPos.z *= _FlattenAmount; // Adjust the z-axis flatten amount
                o.pos = UnityObjectToClipPos(worldPos);
                // o.pos = UnityObjectToClipPos(v.vertex); 
                o.uv = v.uv;                            
                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 glow = _GlowColor * _GlowIntensity;
                fixed4 finalColor = texColor + glow;
                finalColor.a = _Transparency;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
