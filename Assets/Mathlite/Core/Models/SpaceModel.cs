namespace DM.Mathlite.Core.Models {
    public class SpaceModel: Model {
        public float times;
        public bool isHorizontal;

        public SpaceModel(float times, bool isHorizontal = true) {
            this.times = times;
            this.isHorizontal = isHorizontal;
        }

        internal override Views.View toView(Renderer r) =>
            new Views.SpaceView(r, this.isHorizontal, this.times);
    }
}
