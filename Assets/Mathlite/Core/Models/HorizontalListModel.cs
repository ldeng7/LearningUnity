using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public class HorizontalListModel: Model {
        public List<Model> elements;
        public float? spaceTimes;

        public HorizontalListModel(List<Model> elements, float? spaceTimes = DefaultHorizontalSpaceTimes) {
            this.elements = elements;
            this.spaceTimes = spaceTimes;
        }

        public HorizontalListModel(object[] objs, float? spaceTimes = DefaultHorizontalSpaceTimes) :
                this(new List<Model>(objs.Length), spaceTimes) {
            foreach (var e in objs) {
                if (e is char c) {
                    this.elements.Add(new CharModel(c));
                } else if (e is string s) {
                    foreach (var c1 in s) {
                        this.elements.Add(new CharModel(c1));
                    }
                } else if (e is System.ValueTuple<char, TextStyle> tp) {
                    this.elements.Add(new CharModel(tp.Item1, tp.Item2));
                } else if (e is System.ValueTuple<string, TextStyle> tp1) {
                    foreach (var c2 in tp1.Item1) {
                        this.elements.Add(new CharModel(c2, tp1.Item2));
                    }
                } else if (e is Symbol sb) {
                    this.elements.Add(new SymbolModel(sb));
                } else if (e is Model m) {
                    this.elements.Add(m);
                }
            }
        }

        internal override Views.View toView(Renderer r) {
            var views = createViewListWithSpace(r, this.elements, true, this.spaceTimes);
            return new Views.HorizontalListView(r, views);
        }
    }
}
