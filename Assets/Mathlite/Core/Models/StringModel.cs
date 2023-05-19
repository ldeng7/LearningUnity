using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class StringModel: Model {
        public string s;
        public TextStyle? ts;
        public float? spaceTimes;

        public StringModel(string s, TextStyle? ts = null, float? spaceTimes = DefaultHorizontalSpaceTimes) {
            this.s = s;
            this.ts = ts;
            this.spaceTimes = spaceTimes;
        }

        internal override Views.View toView(Renderer r) {
            var listModel = new HorizontalListModel(new List<Model>(this.s.Length), this.spaceTimes);
            foreach (var c in this.s) {
                listModel.elements.Add(new CharModel(c, this.ts));
            }
            return listModel.toView(r);
        }
    }
}
