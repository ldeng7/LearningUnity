using UnityEngine;

namespace DM.Mathlite.Portings.Unity {
    public class MathliteContextComponent: MonoBehaviour {
        public Font[] fonts = new Font[Core.Context.NumFonts];
        internal Core.Context context;

        void loadFont(byte fontId, Core.Metrics[] charMetrics) {
            var cis = this.fonts[fontId].characterInfo;
            foreach (var ci in cis) {
                if (ci.index >= Core.Context.EndChar) {
                    continue;
                }
#pragma warning disable CS0618
                float w = ci.vert.width, h = ci.vert.y;
                float d = -ci.vert.height - h;
#pragma warning restore CS0618
                charMetrics[ci.index] = new Core.Metrics(w, h, d);
            }
        }

        void Awake() {
            var conf = new Core.Context.Conf();
            conf.loadFont = this.loadFont;
            this.context = new Core.Context(ref conf);
        }
    }
}
