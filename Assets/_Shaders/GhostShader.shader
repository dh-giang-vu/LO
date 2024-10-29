Shader "Custom/GhostSineWaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Transparency ("Transparency", Range(0, 1)) = 0.5
        _Darkness ("Darkness", Range(0, 1)) = 0.5
        _AuraColor ("Aura Color", Color) = (1, 1, 1, 1)
        _DistortionAmount ("Distortion Amount", Range(0, 1)) = 0.1
        _TimeScale ("Time Scale", Range(0, 5)) = 1.0
        
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Transparency;
            float _Darkness;
            float4 _AuraColor;
            float _AuraSize;
            float _DistortionAmount;
            float _TimeScale;

            v2f vert(appdata_t v)
            {
                v2f o;
                float time = _Time.y * _TimeScale; // Get time for animation
                float sineWave = sin(v.vertex.y * 10.0 + time) * _DistortionAmount; // Create a sine wave

                // Distort vertex position
                v.vertex.y += sineWave; // Apply distortion to the Y position
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                texColor.rgb *= (1.0 - _Darkness);
                texColor.a *= _Transparency;

                return texColor + _AuraColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
