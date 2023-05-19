namespace DM.Mathlite.Core.Views {
    internal class OverlapView: View {
        readonly View va, vb;
        readonly Models.Alignment alignment;

        internal OverlapView(Renderer r, View va, View vb, Models.Alignment alignment) : base(r) {
            this.va = va;
            this.vb = vb;
            this.alignment = alignment;
            Metrics ma = va.metrics, mb = vb.metrics;
            this.metrics = new Metrics(mb.width, System.MathF.Max(ma.height, mb.height),
                System.MathF.Max(ma.depth, mb.depth));
        }

        internal override void render(float x, float y) {
            Metrics ma = this.va.metrics, mb = this.vb.metrics;
            var baseY = y + this.metrics.depth;
            float leftSpace = 0, _ = 0;
            verticalAlign(mb.width, ma.width, this.alignment, ref leftSpace, ref _);
            this.va.render(x + leftSpace, baseY - ma.depth);
            this.vb.render(x, baseY - mb.depth);
        }
    }
}
