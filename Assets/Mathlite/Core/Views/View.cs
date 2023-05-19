using System.Collections.Generic;

namespace DM.Mathlite.Core.Views {
    public abstract class View {
        internal static void verticalAlign(float parentWidth, float width, Models.Alignment alignment,
                ref float leftSpace, ref float rightSpace) {
            leftSpace = rightSpace = 0;
            var space = parentWidth - width;
            switch (alignment) {
            case Models.Alignment.Left:
                rightSpace = space;
                break;
            case Models.Alignment.Center:
                leftSpace = rightSpace = space / 2;
                break;
            case Models.Alignment.Right:
                leftSpace = space;
                break;
            }
        }

        internal static List<View> createViewListWithSpace(Renderer r, List<View> views,
                bool isHorizontal, float spaceTimes) {
            var views1 = new List<View>(views.Count * 2);
            foreach (var v in views) {
                views1.Add(v);
                views1.Add(new SpaceView(r, isHorizontal, spaceTimes));
            }
            if (views1.Count >= 2) {
                views1.RemoveAt(views1.Count - 1);
            }
            return views1;
        }

        public Metrics metrics { get; internal set; }
        internal readonly Renderer renderer;
        internal readonly float factor;

        protected View(Renderer r) {
            this.renderer = r;
            this.factor = r.curFactor;
        }

        internal float meanWidth() => this.renderer.context.fontMeanWidth * this.factor;
        internal float meanHeight() => this.renderer.context.fontMeanHeight * this.factor;

        internal virtual void render(float x, float y) { }
    }
}
