using System.Collections.Generic;

namespace DM.Mathlite.Core.Models {
    public enum TextStyle: byte {
        Mathit, Mathrm, Mathbb, Mathcal, Mathfrak, Mathscr,
    }

    public class CharModel: Model {
        static readonly FontId[] textStyleFontIdTable = new FontId[] {
            FontId.Cmmi, FontId.Cmr, FontId.Msbm, FontId.Cmsy, FontId.Eufm, FontId.Rsfs,
        };

        static readonly Dictionary<char, Symbol> charSymbolTable = new() {
            { '\'', Symbol.Prime },
            { '-', Symbol.Minus },
            { '<', Symbol.Lt },
            { '>', Symbol.Gt },
            { '\\', Symbol.Backslash },
            { '_', Symbol.Underscore },
            { '`', Symbol.Grave },
            { '{', Symbol.LBrace },
            { '|', Symbol.Mid },
            { '}', Symbol.RBrace },
        };

        internal static ushort byteToCode(byte b, FontId fontId) => (ushort)((((ushort)fontId) << 8) | b);

        public char c;
        public TextStyle? ts;

        public CharModel(char c, TextStyle? ts = null) {
            this.c = c;
            this.ts = ts;
        }

        internal override Views.View toView(Renderer r) {
            if (this.c >= Context.EndChar) {
                throw new InvalidCharException(this.c);
            }
            ushort code;
            if (this.ts == null && charSymbolTable.TryGetValue(c, out Symbol s)) {
                code = (ushort)s;
            } else {
                var ts = this.ts;
                ts ??= char.IsLetter(c) ? TextStyle.Mathit : TextStyle.Mathrm;
                code = byteToCode((byte)c, textStyleFontIdTable[(byte)ts]);
            }
            return new Views.CharView(r, code);
        }
    }
}
