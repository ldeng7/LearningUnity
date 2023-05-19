namespace DM.Mathlite.Core {
    public class Metrics {
        internal float width;
        internal float height;
        internal float depth;

        public Metrics(float width, float height, float depth) {
            this.width = width;
            this.height = height;
            this.depth = depth;
        }

        internal Metrics(Metrics m, float factor) {
            this.width = m.width * factor;
            this.height = m.height * factor;
            this.depth = m.depth * factor;
        }

        internal float TotalHeight() => this.height + this.depth;

        internal void setVerticalCenter(float meanHeight) {
            var th = this.TotalHeight();
            this.height = (th + meanHeight) / 2;
            this.depth = th - this.height;
        }
    }

    public enum FontId: byte {
        Cmmi, Cmr, Cmex, Cmsy, Msam, Msbm, Eufm, Rsfs,
    }

    internal class FontInfo {
        internal readonly Metrics[] metricses = new Metrics[Context.EndChar];
    }

    public class Context {
        public const byte NumFonts = 8;
        public const ushort EndChar = 256;

        public struct Conf {
            public System.Action<byte, Metrics[]> loadFont;
        }

        internal readonly Conf conf;
        internal readonly FontInfo[] fontInfoTable;
        internal readonly float fontMeanWidth, fontMeanHeight;

        void fixFontChar() {
            FontInfo fi;
            fi = this.fontInfoTable[(byte)FontId.Cmr];
            fi.metricses[185].height = fi.metricses[185].TotalHeight();
            fi.metricses[185].depth = 0;

            fi = this.fontInfoTable[(byte)FontId.Cmex];
            foreach (var m in fi.metricses) {
                m?.setVerticalCenter(this.fontMeanHeight);
            }
        }

        public Context(ref Conf conf) {
            this.conf = conf;
            this.fontInfoTable = new FontInfo[NumFonts];
            for (byte i = 0; i < NumFonts; i++) {
                this.fontInfoTable[i] = new FontInfo();
                this.conf.loadFont(i, this.fontInfoTable[i].metricses);
            }
            var m = this.fontInfoTable[(byte)FontId.Cmr].metricses['x'];
            this.fontMeanWidth = m.width;
            this.fontMeanHeight = m.height;
            this.fixFontChar();
        }
    }
}
