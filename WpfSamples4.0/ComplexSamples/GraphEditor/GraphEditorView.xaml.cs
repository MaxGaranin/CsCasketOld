using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using WpfSamples40.Annotations;
using WpfSamples40.ComplexSamples.GraphEditor.Model;
using WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements;
using WpfSamples40.ComplexSamples.GraphEditor.Operations;

namespace WpfSamples40.ComplexSamples.GraphEditor
{
    public partial class GraphEditorView : Window, INotifyPropertyChanged
    {
        public event KeyEventHandler RootKeyDownEventHandler;
        public event KeyEventHandler RootKeyUpEventHandler;
        public event MouseEventHandler CanvasMouseMoveEventHandler;
        public event MouseButtonEventHandler CanvasMouseDownEventHandler;
        public event MouseButtonEventHandler CanvasMouseUpEventHandler;

        public event MouseEventHandler ElementMouseMoveEventHandler;
        public event MouseButtonEventHandler ElementMouseDownEventHandler;
        public event MouseButtonEventHandler ElementMouseUpEventHandler;
        
        private GraphEditorMode _graphEditorMode;
        private Coord _currentCoord;
        private Coord _clickCoord;

        private IOperation _currentOperation;
        private ElementViewModel _currentElement;

        public GraphEditorView()
        {
            InitializeComponent();
            DataContext = this;

            Elements = new ObservableCollection<ElementViewModel>();
            GraphEditorMode = GraphEditorMode.Select;
            CurrentOperation = new SelectOperation(this);
            CurrentCoord = new Coord();
            ClickCoord = new Coord();

            SelectedElements = GraphEditorApp.Instance.SelectionManager.SelectedElements;
        }

        #region Properties

        public ObservableCollection<ElementViewModel> Elements { get; set; }

        public ObservableCollection<ElementViewModel> SelectedElements { get; set; }

        #region GraphEditorMode

        public GraphEditorMode GraphEditorMode
        {
            get { return _graphEditorMode; }
            set
            {
                if (Equals(value, _graphEditorMode)) return;
                _graphEditorMode = value;
                RaisePropertyChanged("GraphEditorMode");

                CurrentOperation.Dispose();

                if (_graphEditorMode == GraphEditorMode.Select)
                {
                    CurrentOperation = new SelectOperation(this);
                }
                else if ((_graphEditorMode == GraphEditorMode.AddWell) ||
                    _graphEditorMode == GraphEditorMode.AddUpsv)
                {
                    CurrentOperation = new AddNewPointElementOperation(this, value);
                }
                else if (_graphEditorMode == GraphEditorMode.AddPipe)
                {
                    CurrentOperation = new AddPipeOperation(this);
                }
            }
        }
        
        #endregion

        #region CurrentCoord

        public Coord CurrentCoord
        {
            get { return _currentCoord; }
            set
            {
                if (Equals(value, _currentCoord)) return;
                _currentCoord = value;
                RaisePropertyChanged("CurrentCoord");
            }
        }

        #endregion

        #region ClickCoord

        public Coord ClickCoord
        {
            get { return _clickCoord; }
            set
            {
                if (Equals(value, _clickCoord)) return;
                _clickCoord = value;
                RaisePropertyChanged("ClickCoord");
            }
        }

        #endregion

        #region CurrentOperation

        public IOperation CurrentOperation
        {
            get { return _currentOperation; }
            set
            {
                if (Equals(value, _currentOperation)) return;
                _currentOperation = value;
                RaisePropertyChanged("CurrentOperation");
            }
        } 
        #endregion

        #region CurrentElement

        public ElementViewModel CurrentElement
        {
            get { return _currentElement; }
            set
            {
                if (Equals(value, _currentElement)) return;
                _currentElement = value;
                RaisePropertyChanged("CurrentElement");
            }
        }
        
        #endregion

        #endregion

        #region EventHandlers

        private void LayoutRoot_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (RootKeyDownEventHandler == null) return;
            RootKeyDownEventHandler(sender, e);

            if (e.Key == Key.Escape)
            {
                GraphEditorMode = GraphEditorMode.Select;
            }
        }

        private void LayoutRoot_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (RootKeyUpEventHandler == null) return;
            RootKeyUpEventHandler(sender, e);
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CanvasMouseDownEventHandler == null) return;
            CanvasMouseDownEventHandler(sender, e);
        }

        private void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (CanvasMouseUpEventHandler == null) return;
            CanvasMouseUpEventHandler(sender, e);
        }

        private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(EditorCanvas);
            PosToCoord(pos, CurrentCoord);

            if (CanvasMouseMoveEventHandler == null) return;
            CanvasMouseMoveEventHandler(sender, e);
        }

        private void Element_ElementMouseDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            if (ElementMouseDownEventHandler == null) return;
            ElementMouseDownEventHandler(sender, e);
        }

        private void Element_ElementMouseUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            if (ElementMouseUpEventHandler == null) return;
            ElementMouseUpEventHandler(sender, e);
        }

        private void Element_ElementMouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            if (ElementMouseMoveEventHandler == null) return;
            ElementMouseMoveEventHandler(sender, e);
        }

        #endregion

        #region Methods

        public void AddElement(ElementViewModel element)
        {
            EditorCanvas.Children.Add(element.Shape);
            Elements.Add(element);

            element.ElementMouseMoveEventHandler += Element_ElementMouseMoveEventHandler;
            element.ElementMouseDownEventHandler += Element_ElementMouseDownEventHandler;
            element.ElementMouseUpEventHandler += Element_ElementMouseUpEventHandler;
        }

        public void RemoveElement(ElementViewModel element)
        {
            EditorCanvas.Children.Remove(element.Shape);
            Elements.Remove(element);

            element.ElementMouseMoveEventHandler -= Element_ElementMouseMoveEventHandler;
            element.ElementMouseDownEventHandler -= Element_ElementMouseDownEventHandler;
            element.ElementMouseUpEventHandler -= Element_ElementMouseUpEventHandler;
        }

        private static void PosToCoord(Point pos, Coord coord)
        {
            coord.X = pos.X;
            coord.Y = pos.Y;
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
