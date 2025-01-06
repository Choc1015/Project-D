Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Thickness", Float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 c = tex2D(_MainTex, i.uv);
                if (c.a == 0)
                {
                    float2 outlineUV = i.uv;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            outlineUV.x = i.uv.x + x * _OutlineThickness;
                            outlineUV.y = i.uv.y + y * _OutlineThickness;
                            half4 outlineColor = tex2D(_MainTex, outlineUV);
                            if (outlineColor.a > 0)
                            {
                                return _OutlineColor;
                            }
                        }
                    }
                }
                return c;
            }
            ENDCG
        }
    }
}