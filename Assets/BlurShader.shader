Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 200

        Pass
        {
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _BlurSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 color = tex2D(_MainTex, uv);
                color += tex2D(_MainTex, uv + float2(_BlurSize, 0));
                color += tex2D(_MainTex, uv - float2(_BlurSize, 0));
                color += tex2D(_MainTex, uv + float2(0, _BlurSize));
                color += tex2D(_MainTex, uv - float2(0, _BlurSize));
                color /= 5; // Rata-rata warna untuk efek blur
                return color;
            }
            ENDCG
        }
    }
}