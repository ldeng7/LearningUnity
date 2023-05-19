using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public enum Delimiter: byte {
        LBracket, RBracket, LSquare, RSquare, LBrace, RBrace,
        LFloor, RFloor, LCeil, RCeil, Divisor, Parallel,
    }

    internal static class Delimition {
        const FontId DelimitionFontId = FontId.Cmex;

        internal class DelimiterInfo {
            internal byte top = 0, mid = 0, bot = 0, rep = 0;
            internal Alignment alignment = Alignment.Center;
            internal float joint = -0.04f;
            internal float? topSpace = null, midSpace = null, botSpace = null, repSpace = null;
            internal Symbol[] uppers = null;
        }

        static readonly Dictionary<Delimiter, DelimiterInfo> delimiterTable = new() {
            {
                Delimiter.LBracket,
                new DelimiterInfo() {
                    top = 48, bot = 64, rep = 66, alignment = Alignment.Left, joint = -0.08f,
                    uppers = new Symbol[] {
                        Symbol.LBracket, Symbol.LBracket1, Symbol.LBracket2, Symbol.LBracket3, Symbol.LBracket4
                    }
                }
            },
            {
                Delimiter.RBracket,
                new DelimiterInfo() {
                    top = 49, bot = 65, rep = 66, alignment = Alignment.Right, joint = -0.08f,
                    uppers = new Symbol[] {
                        Symbol.RBracket, Symbol.RBracket1, Symbol.RBracket2, Symbol.RBracket3, Symbol.RBracket4
                    }
                }
            },
            {
                Delimiter.LSquare,
                new DelimiterInfo() {
                    top = 50, bot = 52, rep = 54, alignment = Alignment.Left,
                    uppers = new Symbol[] {
                        Symbol.LSquare, Symbol.LSquare1, Symbol.LSquare2, Symbol.LSquare3, Symbol.LSquare4
                    }
                }
            },
            {
                Delimiter.RSquare,
                new DelimiterInfo() {
                    top = 51, bot = 53, rep = 54, alignment = Alignment.Right,
                    uppers = new Symbol[] {
                        Symbol.RSquare, Symbol.RSquare1, Symbol.RSquare2, Symbol.RSquare3, Symbol.RSquare4
                    }
                }
            },
            {
                Delimiter.LBrace,
                new DelimiterInfo() {
                    top = 56, mid = 60, bot = 58, rep = 62, joint = -0.08f,
                    topSpace = 54, midSpace = -55, botSpace = 54,
                    uppers = new Symbol[] {
                        Symbol.LBrace, Symbol.LBrace1, Symbol.LBrace2, Symbol.LBrace3, Symbol.LBrace4
                    }
                }
            },
            {
                Delimiter.RBrace,
                new DelimiterInfo() {
                    top = 57, mid = 61, bot = 59, rep = 62, joint = -0.08f,
                    topSpace = -55, midSpace = 54, botSpace = -55,
                    uppers = new Symbol[] {
                        Symbol.RBrace, Symbol.RBrace1, Symbol.RBrace2, Symbol.RBrace3, Symbol.RBrace4
                    }
                }
            },
            {
                Delimiter.LFloor,
                new DelimiterInfo() {
                    bot = 52, rep = 54, alignment = Alignment.Left,
                    uppers = new Symbol[] {
                        Symbol.LFloor, Symbol.LFloor1, Symbol.LFloor2, Symbol.LFloor3, Symbol.LFloor4
                    }
                }
            },
            {
                Delimiter.RFloor,
                new DelimiterInfo() {
                    bot = 53, rep = 54, alignment = Alignment.Right,
                    uppers = new Symbol[] {
                        Symbol.RFloor, Symbol.RFloor1, Symbol.RFloor2, Symbol.RFloor3, Symbol.RFloor4
                    }
                }
            },
            {
                Delimiter.LCeil,
                new DelimiterInfo() {
                    top = 50, rep = 54, alignment = Alignment.Left,
                    uppers = new Symbol[] {
                        Symbol.LCeil, Symbol.LCeil1, Symbol.LCeil2, Symbol.LCeil3, Symbol.LCeil4
                    }
                }
            },
            {
                Delimiter.RCeil,
                new DelimiterInfo() {
                    top = 51, rep = 54, alignment = Alignment.Right,
                    uppers = new Symbol[] {
                        Symbol.RCeil, Symbol.RCeil1, Symbol.RCeil2, Symbol.RCeil3, Symbol.RCeil4
                    }
                }
            },
            {
                Delimiter.Divisor,
                new DelimiterInfo() { rep = 63 }
            },
            {
                Delimiter.Parallel,
                new DelimiterInfo() { rep = 119 }
            },
        };

        class ElementFactory {
            internal Renderer r;
            internal bool isHorizontal;

            internal Views.View get(byte c, float? space, ref float size) {
                var charView = new Views.CharView(this.r, CharModel.byteToCode(c, DelimitionFontId));
                size = charView.metrics.TotalHeight();
                if (isHorizontal) {
                    charView.rotate();
                }
                if (space == null) {
                    return charView;
                }

                List<Views.View> views;
                var spaceView = new Views.SpaceView(r, System.MathF.Abs(space.Value) * charView.factor, !this.isHorizontal);
                if (space.Value > 0) {
                    views = new() { spaceView, charView };
                } else {
                    views = new() { charView, spaceView };
                }
                return isHorizontal ? new Views.VerticalListView(r, views, Alignment.Left, 0) :
                    new Views.HorizontalListView(r, views);
            }
        }

        internal static Views.View delimiterToView(Renderer r, Delimiter delimiter,
                Views.View delimitedView, bool isHorizontal) {
            var minSize = isHorizontal ? delimitedView.metrics.width : delimitedView.metrics.TotalHeight();
            var info = delimiterTable[delimiter];
            if (info.uppers != null) {
                foreach (var upper in info.uppers) {
                    var charView = new Views.CharView(r, (ushort)upper);
                    if (charView.metrics.TotalHeight() >= minSize) {
                        if (isHorizontal) {
                            charView.rotate();
                        }
                        return charView;
                    }
                }
            }

            var ef = new ElementFactory() { r = r, isHorizontal = isHorizontal };
            float repSize = 0, size = 0;
            var repView = ef.get(info.rep, info.repSpace, ref repSize);
            var views = new List<Views.View>((int)(minSize / repSize) + 3);
            Views.View botView = null;
            if (info.top != 0) {
                var topView = ef.get(info.top, info.topSpace, ref size);
                minSize -= size;
                views.Add(topView);
            }
            if (info.bot != 0) {
                botView = ef.get(info.bot, info.botSpace, ref size);
                minSize -= size;
            }
            if (info.mid == 0) {
                int n = (int)System.MathF.Ceiling(minSize / repSize);
                for (int i = 0; i < n; i++) {
                    views.Add(repView);
                }
            } else {
                var midView = ef.get(info.mid, info.midSpace, ref size);
                minSize -= size;
                int n = (int)System.MathF.Ceiling(minSize / (repSize * 2));
                for (int i = 0; i < n; i++) {
                    views.Add(repView);
                }
                views.Add(midView);
                for (int i = 0; i < n; i++) {
                    views.Add(repView);
                }
            }
            if (botView != null) {
                views.Add(botView);
            }

            views = Views.View.createViewListWithSpace(r, views, isHorizontal, info.joint);
            if (isHorizontal) {
                views.Reverse();
                return new Views.HorizontalListView(r, views, info.alignment);
            }
            return new Views.VerticalListView(r, views, info.alignment);
        }
    }
}
