using System.Collections.ObjectModel;
using WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements;

namespace WpfSamples40.ComplexSamples.GraphEditor
{
    public class SelectionManager
    {
        public SelectionManager()
        {
            SelectedElements = new ObservableCollection<ElementViewModel>();
        }

        public ObservableCollection<ElementViewModel> SelectedElements { get; private set; }

        public bool IsMultiSelect { get; set; }

        public void AddElement(ElementViewModel element)
        {
            if (!IsMultiSelect) Clear();

            SelectedElements.Add(element);
//            element.IsSelected = true;
            element.Select();
        }

        public void RemoveElement(ElementViewModel element)
        {
            if (!IsMultiSelect)
            {
                Clear();
                return;
            }

            SelectedElements.Remove(element);
//            element.IsSelected = false;
            element.Unselect();
        }

        public void Clear()
        {
            for (int i = SelectedElements.Count - 1; i >= 0; i--)
            {
//                RemoveElement(SelectedElements[i]);
                var element = SelectedElements[i];
                SelectedElements.Remove(element);
                element.Unselect();
            }
        }
    }
}