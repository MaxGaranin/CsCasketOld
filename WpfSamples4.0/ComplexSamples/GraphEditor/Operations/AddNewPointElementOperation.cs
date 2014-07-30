using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfSamples40.ComplexSamples.GraphEditor.Model;
using WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements;

namespace WpfSamples40.ComplexSamples.GraphEditor.Operations
{
    public class AddNewPointElementOperation : IOperation
    {
        private GraphEditorView _graphEditorView;
        private GraphEditorMode _editorMode;

        public AddNewPointElementOperation(GraphEditorView graphEditorView, GraphEditorMode editorMode)
        {
            _graphEditorView = graphEditorView;
            _editorMode = editorMode;
            graphEditorView.CanvasMouseDownEventHandler += graphEditorView_CanvasMouseDownEventHandler;
        }

        public void graphEditorView_CanvasMouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            if (_editorMode == GraphEditorMode.AddWell)
            {
                AddElement(new WellViewModel(), e.GetPosition(_graphEditorView.EditorCanvas));
            }
            else if (_editorMode == GraphEditorMode.AddUpsv)
            {
                AddElement(new UpsvViewModel(), e.GetPosition(_graphEditorView.EditorCanvas));
            }
        }

        private void AddElement(ElementViewModel elem, Point pos)
        {
            Canvas.SetLeft(elem.Shape, pos.X - elem.Shape.Width / 2);
            Canvas.SetTop(elem.Shape, pos.Y - elem.Shape.Height / 2);
            _graphEditorView.AddElement(elem);
        }

        public void Dispose()
        {
            _graphEditorView.CanvasMouseDownEventHandler -= graphEditorView_CanvasMouseDownEventHandler;
        }
    }
}