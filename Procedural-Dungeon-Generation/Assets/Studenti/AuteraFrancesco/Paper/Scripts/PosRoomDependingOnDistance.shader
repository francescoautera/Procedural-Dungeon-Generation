// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PosRoomDependingOnDistance"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Radius("Radius", Range(0.001, 500)) = 10
        _PosPlayer("Player",Vector) = (.0,.0,.0,.0)
        _FadeMultipl("Fade",Range(0,50)) = 5
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }

            Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
               
                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float4 worldPos : TEXCOORD1;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata_base v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }

                float _Radius;
                float4 _PosPlayer;
                float _FadeMultipl;
                
                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float dist = distance(i.worldPos, _PosPlayer.xyz);
                    /*if (dist < _Radius) {
                        dist = 0;
                    }
                    else {
                        dist = 1;
                    }*/
                    
                    //col.a= 1 - saturate(lerp(col, (0,0,0,0), _Radius - dist));
                    col.a = 1 - saturate(lerp(1,0,(_Radius-dist)* _FadeMultipl));
                    return col;
                }

                ENDCG
            }
        }
}