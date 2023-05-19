namespace DM.Mathlite.Core.Views {
    internal class SpaceView: View {
        const float NegInf = float.NegativeInfinity;

        internal SpaceView(Renderer r, float size, bool isHorizontal): base(r) =>
            this.metrics = isHorizontal ? new Metrics(size, NegInf, NegInf) :
                new Metrics(0, size, 0);

        internal SpaceView(Renderer r, bool isHorizontal, float times): base(r) =>
            this.metrics = isHorizontal ? new Metrics(this.meanWidth() * times, NegInf, NegInf) :
                new Metrics(0, this.meanHeight() * times, 0);
    }
}
