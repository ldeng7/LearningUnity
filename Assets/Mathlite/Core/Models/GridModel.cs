using System.Collections.Generic;
using System.Linq;

namespace DM.Mathlite.Core.Models {
    public class GridModel: Model {
        public List<List<Model>> elements;
        public List<Alignment> columnAlignments;
        public float horizontalSpaceTimes;
        public float? verticalSpaceTimes;

        public GridModel(List<List<Model>> elements, List<Alignment> columnAlignments,
                float horizontalSpaceTimes = DefaultGridSpaceTimes, float? verticalSpaceTimes = DefaultGridSpaceTimes) {
            this.elements = elements;
            this.columnAlignments = columnAlignments;
            this.horizontalSpaceTimes = horizontalSpaceTimes;
            this.verticalSpaceTimes = verticalSpaceTimes;
        }

        internal override Views.View toView(Renderer r) {
            var numCols = (this.elements.Count > 0) ? this.elements[0].Count : 0;
            if (numCols == 0) {
                return new Views.SpaceView(r, 0, false);
            }

            var viewGrid = this.elements.Select(rowModels =>
                createViewListWithSpace(r, rowModels, true, this.horizontalSpaceTimes)).ToList();
            var colWidthes = new float[numCols];
            for (int i = 0; i < numCols; i++) {
                colWidthes[i] = viewGrid.Max(rowViews => rowViews[i * 2].metrics.width);
            }

            foreach (var rowViews in viewGrid) {
                rowViews.Insert(0, new Views.SpaceView(r, 0, true));
                float leftSpace = 0, rightSpace = 0;
                for (int i = 0; i < numCols; i++) {
                    var prevRightSpace = rightSpace;
                    Alignment alignment = (this.columnAlignments != null && this.columnAlignments.Count > i) ?
                        this.columnAlignments[i] : Alignment.Center;
                    Views.View.verticalAlign(colWidthes[i], rowViews[i * 2 + 1].metrics.width,
                        alignment, ref leftSpace, ref rightSpace);
                    rowViews[i * 2].metrics.width += prevRightSpace + leftSpace;
                }
            }

            var views = viewGrid.Select(rowViews => new Views.HorizontalListView(r, rowViews) as Views.View).ToList();
            if (this.verticalSpaceTimes != null) {
                views = Views.View.createViewListWithSpace(r, views, false, this.verticalSpaceTimes.Value);
            }
            return new Views.VerticalListView(r, views, Alignment.Left);
        }
    }
}
