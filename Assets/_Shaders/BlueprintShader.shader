Shader "Custom/BlueprintShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp("Operation", Float) = 0

        _HoloIntensity("Holo Intensity", Float) = 1
        _AnimIntensity("Anim Intensity", Float) = 0

        _Rotator("Rotate", Range(0,1)) = 1

        _Color1("Color 1", color) = (1,1,1,1)
        _Color2("Color 2", color) = (1,1,1,1)

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

            float _HoloIntensity;
            float _AnimIntensity;
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
                
                float rotate = lerp(i.uv.x, i.uv.y, _Rotator);
                float anim = _AnimIntensity * _Time.y;
                float holoEffect = sin(rotate * _HoloIntensity + anim) * 0.5 + 0.5;

                float4 mixedColor = lerp(_Color1, _Color2, rotate) * holoEffect;

                return fixed4(mixedColor.rgb, holoEffect * mixedColor.a);
            }
            ENDCG
        }
    }

}