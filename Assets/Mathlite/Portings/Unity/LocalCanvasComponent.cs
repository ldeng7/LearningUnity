using UnityEngine;

namespace DM.Mathlite.Portings.Unity {
    public class LocalCanvasComponent: MonoBehaviour {
        static readonly int fontShaderIdColor = Shader.PropertyToID("_Color");
        static readonly int[] meshTriangles = new int[] { 0, 1, 2, 0, 2, 3 };
        static readonly Vector3 rotation = new(0, 0, -90);

        public GameObject contextGameObject;
        public Color color;
        protected Core.Renderer r;
        protected Material[] materials;
        protected MathliteContextComponent cc;

        void renderChar(byte fontId, byte c, float x, float y, float factor, bool isRotated) {
            var font = this.cc.fonts[fontId];
            CharacterInfo ci;
            font.GetCharacterInfo((char)c, out ci);

            var go = new GameObject();
            var mf = go.AddComponent<MeshFilter>();
            var mesh = mf.mesh = new Mesh();
            float width = ci.glyphWidth, height = ci.glyphHeight;
            mesh.vertices = new Vector3[] {
                new Vector3(0, height, 0),
                new Vector3(width, height, 0),
                new Vector3(width, 0, 0),
                Vector3.zero,
            };
            mesh.triangles = meshTriangles;
            mesh.uv = new Vector2[] { ci.uvTopLeft, ci.uvTopRight, ci.uvBottomRight, ci.uvBottomLeft };
            go.transform.SetParent(this.transform, false);
            go.transform.localScale = new Vector3(factor, factor, 1);
            if (!isRotated) {
                go.transform.localPosition = new Vector3(x, y, 0);
            } else {
                go.transform.Rotate(rotation);
                go.transform.localPosition = new Vector3(x, y + width * factor, 0);
            }

            var mr = go.AddComponent<MeshRenderer>();
            mr.material = this.materials[fontId];
        }

        protected virtual void Start() {
            this.cc = this.contextGameObject.GetComponent<MathliteContextComponent>();
            this.r = new Core.Renderer(this.cc.context, this.renderChar);
            this.materials = new Material[Core.Context.NumFonts];
            for (byte i = 0; i < Core.Context.NumFonts; i++) {
                var material = this.materials[i] = new Material(this.cc.fonts[i].material);
                material.SetColor(fontShaderIdColor, this.color);
            }
        }
    }
}
