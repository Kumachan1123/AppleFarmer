Shader "Custom/Ripple"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [HideInInspector]
        _StartTime ("Start Time", Float) = 0

        _AnimationTime ("Animation Time", Range(0.1, 10.0)) = 1.5
        _Width ("Width", Range(0.1, 3.0)) = 0.3
        _StartWidth ("Start Width", Range(0, 1.0)) = 0.3
        [Toggle] _isAlpha ("Use Alpha", Float) = 1
        [Toggle] _isColorShift ("Use Color Shift", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            float _StartTime;
            float _AnimationTime;
            float _Width;
            float _StartWidth;
            float _isAlpha;
            float _isColorShift;
            fixed4 _Color;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            v2f vert(appdata IN)
            {
                v2f OUT;

                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                OUT.color = IN.color * _Color;
                return OUT;
            }

            fixed3 shift_col(fixed3 RGB, half3 shift)
            {
                fixed3 RESULT = RGB;

                float VSU = shift.z * shift.y * cos(shift.x * UNITY_PI / 180);
                float VSW = shift.z * shift.y * sin(shift.x * UNITY_PI / 180);

                RESULT.r =
                    (.299 * shift.z + .701 * VSU + .168 * VSW) * RGB.r +
                    (.587 * shift.z - .587 * VSU + .330 * VSW) * RGB.g +
                    (.114 * shift.z - .114 * VSU - .497 * VSW) * RGB.b;

                RESULT.g =
                    (.299 * shift.z - .299 * VSU - .328 * VSW) * RGB.r +
                    (.587 * shift.z + .413 * VSU + .035 * VSW) * RGB.g +
                    (.114 * shift.z - .114 * VSU + .292 * VSW) * RGB.b;

                RESULT.b =
                    (.299 * shift.z - .300 * VSU + 1.250 * VSW) * RGB.r +
                    (.587 * shift.z - .588 * VSU - 1.050 * VSW) * RGB.g +
                    (.114 * shift.z + .886 * VSU - .203 * VSW) * RGB.b;

                return RESULT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, IN.uv) * IN.color;

                float2 pos = (IN.uv - 0.5) * 2.0;

                float radius = (_Time.y - _StartTime) / _AnimationTime + _StartWidth;

                float dis = radius - length(pos);

                clip(dis);
                clip(_Width - dis);

                float alpha = lerp(1.0, saturate((_Width - dis) * 3.0), _isAlpha);

                fixed3 rgb = lerp(color.rgb,
                                  shift_col(color.rgb, half3(_Time.w * 10, 1, 1)),
                                  _isColorShift);

                return fixed4( color.rgb,color.a * alpha);
            }

            ENDCG
        }
    }

    Fallback "Sprites/Default"
}