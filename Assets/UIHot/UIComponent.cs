using UnityEngine;
using UnityEngine.UIElements;

namespace DM.UIHot {
    public class UIComponent: MonoBehaviour {
        public VisualTreeAsset block2UIDoc;
        public VisualTreeAsset block3UIDoc;

        void OnEnable() {
            var uiDocComponent = this.GetComponent<UIDocument>();

            var lv = uiDocComponent.rootVisualElement.Q<Common.UI.ListView>(name: "block2-list-view");
            lv.SetItems(Models.Block2Item.datas, this.block2UIDoc,
                (Models.Block2Item model, TemplateContainer item, int _) => {
                    item.Q<Label>(className: "block2-item-title").text = model.title;
                    item.Q<Label>(className: "block2-item-content").text = model.content;
                });

            var gv = uiDocComponent.rootVisualElement.Q<Common.UI.GridView>(name: "block3-grid-view");
            gv.SetItems(Models.Block3Item.datas, this.block3UIDoc, 3,
                (Models.Block3Item model, TemplateContainer item, int _, int _) => {
                    item.Q<VisualElement>(className: "block3-item-image").style.backgroundImage = 
                        Resources.Load<Texture2D>(model.imgUrl);
                    item.Q<Label>(className: "block3-item-rank").text = model.rank;
                    item.Q<Label>(className: "block3-item-name").text = model.name;
                });
        }
    }
}
