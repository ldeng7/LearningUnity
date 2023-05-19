Shader "Custom/SpurUnlit" {
Properties {
    prop_MainTex("Main Texture", 2D) = "white" {}
    [Toggle] key_Concave("Concave", Float) = 0.0
}

SubShader {
    Tags {
        "RenderType" = "Opaque"
    }
    Pass {
        CGPROGRAM
        #include "UnityCG.cginc"
        #pragma target 4.0
        #pragma vertex vert
        #pragma geometry geom
        #pragma fragment frag
        #pragma shader_feature KEY_CONCAVE_ON

        sampler2D prop_MainTex;
        float4 prop_MainTex_ST;

        struct ModelVertex {
            float4 pos: POSITION;
            float2 uv: TEXCOORD0;
        };

        struct Vertex {
            float4 pos: SV_POSITION;
            float2 uv: TEXCOORD0;
            float4 mpos: TEXCOORD1;
        };

        Vertex vert(ModelVertex mv) {
            Vertex v;
            v.pos = UnityObjectToClipPos(mv.pos);
            v.uv = TRANSFORM_TEX(mv.uv, prop_MainTex);
            v.mpos = mv.pos;
            return v;
        }

        [maxvertexcount(9)]
        void geom(inout TriangleStream<Vertex> ts, triangle Vertex vs[3]) {
            float4 p = (vs[0].mpos + vs[1].mpos + vs[2].mpos) / 3.0;
            float3 e0 = vs[1].mpos - vs[0].mpos, e1 = vs[2].mpos - vs[1].mpos;
            float3 vec = normalize(cross(e0, e1)) * (abs(e0.x) + abs(e0.y) + abs(e1.x) + abs(e1.y)) / 2.0;
            if (KEY_CONCAVE_ON) {
                vec = -vec;
            }
            Vertex v;
            v.mpos = float4(p + vec, 1.0);
            v.pos = UnityObjectToClipPos(v.mpos);

            for (uint i = 0; i < 3; i++) {
                ts.RestartStrip();
                ts.Append(vs[i]);
                ts.Append(vs[(i + 1) % 3]);
                v.uv = vs[(i + 2) % 3].uv;
                ts.Append(v);
            }
        }

        float4 frag(Vertex v): SV_TARGET {
            return tex2D(prop_MainTex, v.uv);
        }
        ENDCG
    }
}
}
