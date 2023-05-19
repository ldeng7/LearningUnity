Shader "Custom/GeoOutline" {
Properties {
    prop_MainTex("Main Texture", 2D) = "white" {}
    prop_OutlineWidth("Outline Width", Float) = 0.07
    prop_OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
}

SubShader {
    Tags {
        "RenderType" = "Opaque"
    }
    Pass {
        Name "OUTLINE"
        Cull Front
        CGPROGRAM
        #include "UnityCG.cginc"
        #pragma vertex vert
        #pragma fragment frag

        float prop_OutlineWidth;
        float4 prop_OutlineColor;

        struct ModelVertex {
            float4 pos: POSITION;
            float4 normal: NORMAL;
        };

        float4 vert(ModelVertex mv): SV_POSITION{
            float4 pos = mul(UNITY_MATRIX_MV, mv.pos);
            float4 normal = mul(UNITY_MATRIX_IT_MV, mv.normal);
            pos += float4(normal.xyz, 0) * prop_OutlineWidth;
            return mul(UNITY_MATRIX_P, pos);
        }

        float4 frag(float4 pos: SV_POSITION): SV_TARGET {
            return float4(prop_OutlineColor.rgb, 1);
        }
        ENDCG
    }
    Pass {
        Tags {
            "LightMode" = "UniversalForward"
        }
        CGPROGRAM
        #include "UnityCG.cginc"
        #pragma vertex vert
        #pragma fragment frag

        sampler2D prop_MainTex;
        float4 prop_MainTex_ST;

        struct ModelVertex {
            float4 pos: POSITION;
            float2 uv: TEXCOORD0;
        };

        struct Vertex {
            float4 pos: SV_POSITION;
            float2 uv: TEXCOORD0;
        };

        Vertex vert(ModelVertex mv) {
            Vertex v;
            v.pos = UnityObjectToClipPos(mv.pos);
            v.uv = TRANSFORM_TEX(mv.uv, prop_MainTex);
            return v;
        }

        float4 frag(Vertex v): SV_TARGET {
            return tex2D(prop_MainTex, v.uv);
        }
        ENDCG
    }
}
}
