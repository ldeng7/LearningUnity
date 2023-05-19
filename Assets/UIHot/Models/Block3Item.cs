using System.Collections.Generic;

namespace DM.UIHot.Models {
    public class Block3Item {
        public static readonly List<Block3Item> datas;

        static Block3Item() {
            datas = new();
            datas.Add(new() { imgUrl = "UIHotBlock3Item01", name = "王文盲", rank = "SS" });
            datas.Add(new() { imgUrl = "UIHotBlock3Item02", name = "Sister Phone", rank = "SSR" });
            datas.Add(new() { imgUrl = "UIHotBlock3Item03", name = "一吱花", rank = "A" });
            datas.Add(new() { imgUrl = "UIHotBlock3Item04", name = "张油田", rank = "A+" });
            datas.Add(new() { imgUrl = "UIHotBlock3Item05", name = "老逼登", rank = "S" });
            datas.Add(new() { imgUrl = "UIHotBlock3Item06", name = "虞法令", rank = "A-" });
        }

        public string imgUrl;
        public string name;
        public string rank;
    }
}
