namespace DM.Mathlite.Core.Models {
    public class OverlapModel: Model {
        public Model ma, mb;
        public Alignment alignment;

        public OverlapModel(Model ma, Model mb, Alignment alignment = Alignment.Center) {
            this.ma = ma;
            this.mb = mb;
            this.alignment = alignment;
        }

        internal override Views.View toView(Renderer r) {
            var va = this.ma.toView(r);
            var vb = this.mb.toView(r);
            if (va.metrics.width > vb.metrics.width) {
                (va, vb) = (vb, va);
            }
            return new Views.OverlapView(r, va, vb, this.alignment);
        }
    }
}
