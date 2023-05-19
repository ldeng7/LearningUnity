using System.Collections.Generic;
using UnityEngine.UIElements;

namespace DM.Common.UI {
    public class GridView: VisualElement {
        public const string RowClass = "gridview-row";

        public new class UxmlFactory: UxmlFactory<GridView, UxmlTraits> { }

        public void SetItems<Model>(IEnumerable<Model> models, VisualTreeAsset itemTemp, int nCols,
                System.Action<Model, TemplateContainer, int, int> onInstantiateItem) {
            int iRow = 0, iCol = 0;
            VisualElement row = null;
            foreach (var model in models) {
                if (iCol == 0) {
                    row = new VisualElement();
                    row.AddToClassList(RowClass);
                    row.style.flexDirection = FlexDirection.Row;
                }
                var item = itemTemp.Instantiate();
                onInstantiateItem(model, item, iRow, iCol);
                row.Add(item);
                if (++iCol == nCols) {
                    iRow++;
                    iCol = 0;
                    this.Add(row);
                }
            }
        }
    }
}
