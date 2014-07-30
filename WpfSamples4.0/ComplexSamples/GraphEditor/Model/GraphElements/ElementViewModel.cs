using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Shapes;
using WpfSamples40.Annotations;
using WpfSamples40.ComplexSamples.GraphEditor.Domain;

namespace WpfSamples40.ComplexSamples.GraphEditor.Model.GraphElements
{
    public class ElementViewModel : INotifyPropertyChanged
    {
        private Shape _shape;
        private bool _isSelected;

        public event MouseEventHandler ElementMouseMoveEventHandler;
        public event MouseButtonEventHandler ElementMouseDownEventHandler;
        public event MouseButtonEventHandler ElementMouseUpEventHandler;

        public DomainObject DomainObject { get; set; }
        public Coord CenterCoord { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value.Equals(_isSelected)) return;
                _isSelected = value;

                if (_isSelected)
                {
                    GraphEditorApp.Instance.SelectionManager.AddElement(this);
                }
                else
                {
                    GraphEditorApp.Instance.SelectionManager.RemoveElement(this);
                }

                RaisePropertyChanged("IsSelected");
            }
        }

        public void Select()
        {
            _isSelected = true;
            RaisePropertyChanged("IsSelected");
        }

        public void Unselect()
        {
            _isSelected = false;
            RaisePropertyChanged("IsSelected");
        }

        public Shape Shape
        {
            get { return _shape; }
            set
            {
                if (_shape != null) UnregisterHandlers();
                _shape = value;
                if (value != null) RegisterHandlers();
            }
        }

        private void RegisterHandlers()
        {
            Shape.MouseDown += Shape_MouseDown;
            Shape.MouseUp += Shape_MouseUp;
            Shape.MouseMove += Shape_MouseMove;
        }

        private void UnregisterHandlers()
        {
            Shape.MouseDown -= Shape_MouseDown;
            Shape.MouseUp -= Shape_MouseUp;
            Shape.MouseMove -= Shape_MouseMove;
        }

        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (ElementMouseMoveEventHandler == null) return;
            ElementMouseMoveEventHandler(this, e);
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ElementMouseDownEventHandler == null) return;
            ElementMouseDownEventHandler(this, e);
        }

        private void Shape_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ElementMouseUpEventHandler == null) return;
            ElementMouseUpEventHandler(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}