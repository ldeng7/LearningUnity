using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class SigmaModel: Model {
        public Model baseModel, superModel, subModel;

        public SigmaModel(Model baseModel, Model superModel, Model subModel) {
            this.baseModel = baseModel;
            this.superModel = superModel;
            this.subModel = subModel;
        }

        internal override Views.View toView(Renderer r) {
            r.incrScriptLevel();
            var superView = this.superModel?.toView(r);
            var subView = this.subModel?.toView(r);
            r.decrScriptLevel();

            var views = new List<Views.View>(3);
            var spaceView = new Views.SpaceView(r, false, DefaultVerticalSpaceTimes);
            int centralIndex = 0;
            if (superView != null) {
                views.Add(superView);
                views.Add(spaceView);
                centralIndex = 2;
            }
            views.Add(this.baseModel.toView(r));
            if (subView != null) {
                views.Add(spaceView);
                views.Add(subView);
            }
            return new Views.VerticalListView(r, views, Alignment.Center, centralIndex);
        }
    }
}
