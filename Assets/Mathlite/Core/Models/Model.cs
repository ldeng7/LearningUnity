using System.Collections.Generic;
using System.Linq;

namespace DM.Mathlite.Core.Models {
    public enum Alignment: byte { Left, Center, Right }

    public class InvalidCharException: System.Exception {
        public char c;

        public InvalidCharException(char c): base("invalid char: " + c) => this.c = c;
    }

    public abstract class Model {
        public const float DefaultHorizontalSpaceTimes = 0.13f;
        public const float DefaultVerticalSpaceTimes = 0.38f;
        public const float DefaultGridSpaceTimes = 0.7f;

        internal static List<Views.View> createViewListWithSpace(Renderer r, List<Model> models,
                bool isHorizontal, float? spaceTimes) {
            var views = models.Select(m => m.toView(r)).ToList();
            if (spaceTimes != null) {
                views = Views.View.createViewListWithSpace(r, views, isHorizontal, spaceTimes.Value);
            }
            return views;
        }

        internal abstract Views.View toView(Renderer r);

        public static implicit operator Model(char c) {
            return new CharModel(c);
        }

        public static implicit operator Model(string s) {
            return new StringModel(s);
        }

        public static implicit operator Model(Symbol symbol) {
            return new SymbolModel(symbol);
        }

        public static implicit operator Model(object[] objs) {
            return new HorizontalListModel(objs);
        }
    }
}
