Shader "Unlit/Spotlight"
{
    Properties
    {
        _Alpha("Alpha", Range(0,1)) = 0.3
        _MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
        _CutOff("Cut off", Range(0,1)) = 0.1
        _CharacterPosition("Position",vector) = (0,0,0,0)
        _CircleRadius("Spotlight size", Range(0, 20)) = 3
        _RingSize("Ring size", Range(0, 5)) = 1
        _ColorTint("OutSide of the Spotlight color", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
         Tags { "RenderType"="Opaque" }
         LOD 100
         Blend SrcAlpha OneMinusSrcAlpha
         AlphaTest Greater 0.1
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            uniform float _CutOff;
            uniform float _Alpha;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos :TEXCOORD1;//worldPos of that Vertex
                //float3 normalMinusWorld::TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _CharacterPosition;
            float _CircleRadius;
            float _RingSize;
            float4 _ColorTint;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 color=0;
                float dist = distance(i.worldPos, _CharacterPosition.xyz);
                if (dist < _CircleRadius) {
                    if (color.a < _CutOff) discard;
                    else color.a = _ColorTint;
                }
                else if (dist > _CircleRadius && dist < _CircleRadius + _RingSize) {
                    color.a = _ColorTint;
                    
                }
                return color;
            }
            ENDCG
        }
    }
}
