using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class DelimitedModel: Model {
        public Model mainModel;
        public Delimiter? beginDelimiter, endDelimiter;
        public bool isHorizontal;

        public DelimitedModel(Model mainModel, Delimiter? leftDelimiter, Delimiter? rightDelimiter,
                bool isHorizontal = true) {
            this.mainModel = mainModel;
            this.beginDelimiter = leftDelimiter;
            this.endDelimiter = rightDelimiter;
            this.isHorizontal = isHorizontal;
        }

        internal override Views.View toView(Renderer r) {
            var mainView = this.mainModel.toView(r);
            int centralIndex = 0;
            var views = new List<Views.View>(3);
            if (this.beginDelimiter != null) {
                views.Add(Delimition.delimiterToView(r, this.beginDelimiter.Value, mainView, !this.isHorizontal));
                centralIndex++;
            }
            views.Add(mainView);
            if (this.endDelimiter != null) {
                views.Add(Delimition.delimiterToView(r, this.endDelimiter.Value, mainView, !this.isHorizontal));
            }
            return this.isHorizontal ? new Views.HorizontalListView(r, views) :
                new Views.VerticalListView(r, views, Alignment.Center, centralIndex);
        }
    }
}
