Shader "Custom/EnhancedBlueprintShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Op("Operation", Float) = 0

        _HoloIntensity("Holo Intensity", Float) = 1
        _AnimIntensity("Anim Intensity", Float) = 0
        _PulsingSpeed("Pulsing Speed", Float) = 1

        _Rotator("Rotate", Range(0,1)) = 1
        _MainTex("Main Texture", 2D) = "white" {}
        _DetailTex("Detail Texture", 2D) = "black" {}

        _Color1("Color 1", Color) = (1,1,1,1)
        _Color2("Color 2", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 100
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Op]

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
            sampler2D _DetailTex;

            float _HoloIntensity;
            float _AnimIntensity;
            float _PulsingSpeed;

            float4 _Color1;
            float4 _Color2;

            float _Rotator;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Rotator effect
                float rotate = lerp(i.uv.x, i.uv.y, _Rotator);
                
                // Animation effect
                float anim = _AnimIntensity * _Time.y;
                float holoEffect = sin(rotate * _HoloIntensity + anim) * 0.5 + 0.5;

                // Pulsing effect adjusted to go from 100% to 50%
                float pulse = sin(_Time.y * _PulsingSpeed) * 0.25 + 0.75;
                
                // Blend the colors based on the rotation and pulse effect
                float4 mixedColor = lerp(_Color1, _Color2, rotate) * holoEffect * pulse;

                // Apply detail texture if any
                fixed4 detailColor = tex2D(_DetailTex, i.uv) * 0.5; // Half opacity for detail
                mixedColor += detailColor; // Add detail color

                // Return final color
                return fixed4(mixedColor.rgb, holoEffect * mixedColor.a);
            }
            ENDCG
        }
    }
}
