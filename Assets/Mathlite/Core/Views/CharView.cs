namespace DM.Mathlite.Core.Views {
    internal class CharView: View {
        readonly FontId fontId;
        readonly byte c;
        bool isRotated;

        internal CharView(Renderer r, ushort code): base(r) {
            this.fontId = (FontId)(code >> 8);
            this.c = (byte)(code & 0xff);
            this.isRotated = false;

            var fontInfo = r.context.fontInfoTable[(byte)this.fontId];
            var m = fontInfo.metricses[this.c];
            if (m == null) {
                //ldeng7:
                UnityEngine.Debug.Log("null metric:"+fontId+":"+c);
                this.fontId = FontId.Cmr; this.c = (byte)'x';
                fontInfo = r.context.fontInfoTable[(byte)this.fontId];
                m = fontInfo.metricses[this.c];
            }
            this.metrics = new Metrics(m, this.factor);
        }

        internal void rotate() {
            this.isRotated = true;
            var w = this.metrics.width;
            this.metrics.width = this.metrics.TotalHeight();
            this.metrics.height = w;
            this.metrics.depth = 0;
        }

        internal override void render(float x, float y) =>
            this.renderer.renderChar((byte)this.fontId, this.c, x, y, this.factor, this.isRotated);
    }
}
