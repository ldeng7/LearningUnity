using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class DivisorModel: Model {
        public Model numerator, denominator;

        public DivisorModel(Model numerator, Model denominator) {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        internal override Views.View toView(Renderer r) {
            var va = this.numerator.toView(r);
            var vb = this.denominator.toView(r);
            var delimiterView = Delimition.delimiterToView(r, Delimiter.Divisor,
                (va.metrics.width > vb.metrics.width) ? va : vb, true);
            var spaceView = new Views.SpaceView(r, false, DefaultVerticalSpaceTimes);
            var views = new List<Views.View>() { va, spaceView, delimiterView, spaceView, vb };
            return new Views.VerticalListView(r, views, Alignment.Center, 2);
        }
    }
}
