using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfSamples40.ComplexSamples.GraphEditor.Model;
using WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements;

namespace WpfSamples40.ComplexSamples.GraphEditor.Operations
{
    public class AddPipeOperation : IOperation
    {
        private readonly GraphEditorView _graphEditorView;
        private bool _isFirstPointMode;
        private bool _isSecondPointMode;
        private PipeViewModel _currentPipe;

        public AddPipeOperation(GraphEditorView graphEditorView)
        {
            _graphEditorView = graphEditorView;
            _graphEditorView.ElementMouseDownEventHandler += GraphEditorView_ElementMouseDownEventHandler;
            _graphEditorView.CanvasMouseMoveEventHandler += GraphEditorView_CanvasMouseMoveEventHandler;
            _isFirstPointMode = true;
        }

        private void GraphEditorView_CanvasMouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(_graphEditorView.EditorCanvas);

            if (_isSecondPointMode)
            {
                var line = (Line)_currentPipe.Shape;
                line.X2 = pos.X;
                line.Y2 = pos.Y;
            }
        }

        private void GraphEditorView_ElementMouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(_graphEditorView.EditorCanvas);
            PosToCoord(pos, _graphEditorView.ClickCoord);
            
            var targetShape = ((ElementViewModel) sender).Shape;
            var coord = GetCenterCoord(targetShape);
            
            if (_isFirstPointMode)
            {
                _currentPipe = new PipeViewModel();
                var line = (Line)_currentPipe.Shape;
                line.IsHitTestVisible = false;
                line.X1 = line.X2 = coord.X;
                line.Y1 = line.Y2 = coord.Y;
            
                _graphEditorView.EditorCanvas.Children.Add(_currentPipe.Shape);
            
                _isFirstPointMode = false;
                _isSecondPointMode = true;
            }
            else if (_isSecondPointMode)
            {
                var line = (Line)_currentPipe.Shape;
                line.IsHitTestVisible = true;
                line.X2 = coord.X;
                line.Y2 = coord.Y;

                _graphEditorView.EditorCanvas.Children.Remove(_currentPipe.Shape);
                _graphEditorView.AddElement(_currentPipe);
                _isSecondPointMode = false;
                _isFirstPointMode = true;
            }
            
            e.Handled = true;
        }

        private void PosToCoord(Point pos, Coord coord)
        {
            coord.X = pos.X;
            coord.Y = pos.Y;
        }

        private Coord GetCenterCoord(Shape shape)
        {
            var coord = new Coord();
            coord.X = Canvas.GetLeft(shape) + shape.Width/2;
            coord.Y = Canvas.GetTop(shape) + shape.Height/2;
            return coord;
        }

        public void Dispose()
        {
            _graphEditorView.ElementMouseDownEventHandler -= GraphEditorView_ElementMouseDownEventHandler;
            _graphEditorView.CanvasMouseMoveEventHandler -= GraphEditorView_CanvasMouseMoveEventHandler;
        }
    }
}