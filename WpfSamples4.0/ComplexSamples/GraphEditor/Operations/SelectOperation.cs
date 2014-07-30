using System;
using System.Windows.Input;
using WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements;

namespace WpfSamples40.ComplexSamples.GraphEditor.Operations
{
    public class SelectOperation : IOperation
    {
        private GraphEditorView _graphEditorView;

        public SelectOperation(GraphEditorView graphEditorView)
        {
            _graphEditorView = graphEditorView;
            _graphEditorView.ElementMouseDownEventHandler += GraphEditorView_OnElementMouseDown;
            _graphEditorView.RootKeyDownEventHandler += GraphEditorView_RootKeyDownEventHandler;
            _graphEditorView.RootKeyUpEventHandler += GraphEditorView_RootKeyUpEventHandler;
        }

        private void GraphEditorView_RootKeyDownEventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                GraphEditorApp.Instance.SelectionManager.IsMultiSelect = true;
            }
        }

        private void GraphEditorView_RootKeyUpEventHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                GraphEditorApp.Instance.SelectionManager.IsMultiSelect = false;
            }
        }

        private void GraphEditorView_OnElementMouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = (ElementViewModel) sender;
            var selectionManager = GraphEditorApp.Instance.SelectionManager;

            if (!element.IsSelected)
            {
                selectionManager.AddElement(element);
            }
            else
            {
                selectionManager.RemoveElement(element);
            }
        }

        public void Dispose()
        {
            _graphEditorView.ElementMouseDownEventHandler -= GraphEditorView_OnElementMouseDown;
            _graphEditorView.RootKeyDownEventHandler -= GraphEditorView_RootKeyDownEventHandler;
            _graphEditorView.RootKeyUpEventHandler -= GraphEditorView_RootKeyUpEventHandler;
        }
    }
}