using System.Collections.Generic;
using System.Linq;

namespace DM.Mathlite.Core.Views {
    internal class HorizontalListView: View {
        readonly List<View> elements;

        internal HorizontalListView(Renderer r, List<View> views, Models.Alignment? alignment = null) : base(r) {
            this.elements = views;
            this.metrics = new Metrics(this.elements.Sum(e => e.metrics.width), 0, 0);
            switch (alignment) {
            case null:
                this.metrics.height = this.elements.Max(e => e.metrics.height);
                this.metrics.depth = this.elements.Max(e => e.metrics.depth);
                break;
            case Models.Alignment.Left:
                var h = this.metrics.height = this.elements.Max(e => e.metrics.TotalHeight());
                foreach (var e in this.elements) {
                    var th = e.metrics.TotalHeight();
                    e.metrics.height = h;
                    e.metrics.depth = th - h;
                }
                break;
            case Models.Alignment.Center:
                var mh = this.meanHeight();
                foreach (var e in this.elements) {
                    e.metrics.setVerticalCenter(mh);
                }
                this.metrics.height = this.elements.Max(e => e.metrics.height);
                this.metrics.depth = this.elements.Max(e => e.metrics.depth);
                break;
            case Models.Alignment.Right:
                this.metrics.height = this.elements.Max(e => e.metrics.TotalHeight());
                foreach (var e in this.elements) {
                    e.metrics.height += e.metrics.depth;
                    e.metrics.depth = 0;
                }
                break;
            }
        }

        internal override void render(float x, float y) {
            var baseY = y + this.metrics.depth;
            foreach (var e in this.elements) {
                e.render(x, baseY - e.metrics.depth);
                x += e.metrics.width;
            }
        }
    }
}
