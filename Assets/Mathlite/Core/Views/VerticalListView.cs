using System.Collections.Generic;
using System.Linq;

namespace DM.Mathlite.Core.Views {
    internal class VerticalListView: View {
        readonly List<View> elements;
        readonly Models.Alignment alignment;

        internal VerticalListView(Renderer r, List<View> views, Models.Alignment alignment, 
                int? centralIndex = null) : base(r) {
            this.elements = views;
            this.alignment = alignment;
            this.metrics = new Metrics(this.elements.Max(e => e.metrics.width), 0, 0);
            if (centralIndex == null) {
                this.metrics.height = this.elements.Sum(e => e.metrics.TotalHeight());
                this.metrics.setVerticalCenter(this.meanHeight());
            } else {
                for (int i = 0; i < centralIndex; i++) {
                    this.metrics.height += views[i].metrics.TotalHeight();
                }
                var cm = views[centralIndex.Value].metrics;
                this.metrics.height += cm.height;
                this.metrics.depth = cm.depth;
                for (int i = centralIndex.Value + 1; i < views.Count; i++) {
                    this.metrics.depth += views[i].metrics.TotalHeight();
                }
            }
        }

        internal override void render(float x, float y) {
            var pw = this.metrics.width;
            y += this.metrics.TotalHeight();
            foreach (var e in this.elements) {
                float leftSpace = 0, _ = 0;
                verticalAlign(pw, e.metrics.width, this.alignment, ref leftSpace, ref _);
                y -= e.metrics.TotalHeight();
                e.render(x + leftSpace, y);
            }
        }
    }
}
