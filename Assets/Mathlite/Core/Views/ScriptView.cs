namespace DM.Mathlite.Core.Views {
    internal class ScriptView: View {
        readonly View baseView, superView, subView;
        readonly float intersect;

        internal ScriptView(Renderer r, View baseView, View superView, View subView, float intersectTimes) : base(r) {
            this.baseView = baseView;
            this.superView = superView;
            this.subView = subView;
            this.intersect = this.meanHeight() * intersectTimes;
            var w1 = (superView != null) ? superView.metrics.width : 0;
            var w2 = (subView != null) ? subView.metrics.width : 0;
            var w = baseView.metrics.width + System.Math.Max(w1, w2);
            var h = baseView.metrics.height +
                ((superView != null) ? superView.metrics.TotalHeight() - this.intersect : 0);
            var d = baseView.metrics.depth +
                ((subView != null) ? subView.metrics.TotalHeight() - this.intersect : 0);
            this.metrics = new Metrics(w, h, d);
        }

        internal override void render(float x, float y) {
            var baseViewMetrics = this.baseView.metrics;
            var baseY = y + this.metrics.depth;
            this.baseView.render(x, baseY - baseViewMetrics.depth);
            x += baseViewMetrics.width;
            this.superView?.render(x, baseY + baseViewMetrics.height - this.intersect);
            this.subView?.render(x, y);
        }
    }
}
