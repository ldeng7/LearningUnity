namespace DM.Mathlite.Core {
    public class Renderer {
        public delegate void RenderCharFunc(byte fontId, byte c, float x, float y, float factor, bool isRotated);

        const byte MaxFactorVaryingScriptLevel = 2;

        static readonly float[] scriptFactors = new float[MaxFactorVaryingScriptLevel + 1] { 1, 0.6f, 0.38f };

        internal readonly Context context;
        internal readonly RenderCharFunc renderChar;
        internal float curFactor;
        float topFactor;
        byte curScriptLevel;

        public Renderer(Context context, RenderCharFunc renderChar) {
            this.context = context;
            this.renderChar = renderChar;
        }

        public Views.View CreateView(Models.Model model, float factor) {
            this.curFactor = this.topFactor = factor;
            this.curScriptLevel = 0;
            return model.toView(this);
        }

        public void Render(Views.View view, float x, float y) => view.render(x, y);

        internal void incrScriptLevel() {
            if (++this.curScriptLevel <= MaxFactorVaryingScriptLevel) {
                this.curFactor = this.topFactor * scriptFactors[this.curScriptLevel];
            }
        }

        internal void decrScriptLevel() {
            if (--this.curScriptLevel <= MaxFactorVaryingScriptLevel) {
                this.curFactor = this.topFactor * scriptFactors[this.curScriptLevel];
            }
        }
    }
}
