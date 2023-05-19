namespace DM.Mathlite.Core.Models {
    public class ScriptModel: Model {
        public Model baseModel, superModel, subModel;

        public ScriptModel(Model baseModel, Model superModel, Model subModel) {
            this.baseModel = baseModel;
            this.superModel = superModel;
            this.subModel = subModel;
        }

        internal override Views.View toView(Renderer r) {
            r.incrScriptLevel();
            var superView = this.superModel?.toView(r);
            var subView = this.subModel?.toView(r);
            r.decrScriptLevel();
            return new Views.ScriptView(r, this.baseModel.toView(r), superView, subView, DefaultVerticalSpaceTimes);
        }
    }
}
