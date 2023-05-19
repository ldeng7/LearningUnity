using System.Collections.Generic;
using System.Linq;

namespace DM.Mathlite.Core.Models {
    public class MatrixModel: Model {
        public List<List<Model>> elements;
        public float horizontalSpaceTimes, verticalSpaceTimes;

        public MatrixModel(List<List<Model>> elements,
                float horizontalSpaceTimes = DefaultGridSpaceTimes, float verticalSpaceTimes = DefaultGridSpaceTimes) {
            this.elements = elements;
            this.horizontalSpaceTimes = horizontalSpaceTimes;
            this.verticalSpaceTimes = verticalSpaceTimes;
        }

        internal override Views.View toView(Renderer r) {
            var columnAlignments = new List<Alignment>(
                Enumerable.Repeat(Alignment.Center, this.elements[0].Count));
            var mainModel = new GridModel(this.elements, columnAlignments,
                this.horizontalSpaceTimes, this.verticalSpaceTimes);
            return new DelimitedModel(mainModel, Delimiter.LSquare, Delimiter.RSquare).toView(r);
        }
    }
}
