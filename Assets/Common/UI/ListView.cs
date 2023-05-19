using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DM.Common.UI {
    public class ListView: VisualElement {
        public new class UxmlFactory: UxmlFactory<ListView, UxmlTraits> { }

        public void SetItems<Model>(IEnumerable<Model> models, VisualTreeAsset itemTemp,
                System.Action<Model, TemplateContainer, int> onInstantiateItem) {
            int index = 0;
            foreach (var model in models) {
                var item = itemTemp.Instantiate();
                onInstantiateItem(model, item, index);
                this.Add(item);
                index++;
            }
        }
    }
}
