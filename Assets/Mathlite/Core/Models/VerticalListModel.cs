using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class VerticalListModel: Model {
        public List<Model> elements;
        public Alignment alignment;
        public float? spaceTimes;
        public int? centralIndex;

        public VerticalListModel(List<Model> elements, Alignment alignment = Alignment.Center,
                float? spaceTimes = DefaultVerticalSpaceTimes, int? centralIndex = null) {
            this.elements = elements;
            this.alignment = alignment;
            this.spaceTimes = spaceTimes;
            this.centralIndex = centralIndex;
        }

        internal override Views.View toView(Renderer r) {
            var views = createViewListWithSpace(r, this.elements, false, this.spaceTimes);
            if (this.centralIndex != null && this.spaceTimes != null) {
                this.centralIndex *= 2;
            }
            return new Views.VerticalListView(r, views, this.alignment, this.centralIndex);
        }
    }
}
