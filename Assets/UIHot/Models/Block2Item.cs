using System.Collections.Generic;

namespace DM.UIHot.Models {
    public class Block2Item {
        public static readonly List<Block2Item> datas;

        static Block2Item() {
            datas = new();
            datas.Add(new() { title = "厉不厉害你坤哥", content = "其实我平时很厉害的" });
            datas.Add(new() { title = "你们食不食油饼", content = "你让我拿什么荔枝，你们毁了他的一生！" });
        }

        public string title;
        public string content;
    }
}
