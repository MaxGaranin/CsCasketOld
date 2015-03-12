using System.Windows;
using System.Windows.Input;

namespace WpfSamples40.ComplexSamples.EventCommands
{
    public class EventCommandsViewModel : ViewModelBase
    {
        private string _coords;

        public string Coords
        {
            get { return _coords; }
            set
            {
                _coords = value; 
                //OnPropertyChanged("Coords");
                NotifyOfPropertyChange(() => Coords);
            }
        }

        private DelegateCommand<MouseEventArgs> _canvasMouseMoveCommand;

        public ICommand CanvasMouseMoveCommand
        {
            get
            {
                if (_canvasMouseMoveCommand == null)
                {
                    _canvasMouseMoveCommand = new DelegateCommand<MouseEventArgs>(OnMouseMove);
                }
                return _canvasMouseMoveCommand;
            }
        }
        
        private void OnMouseMove(MouseEventArgs e)
        {
            var pos = e.GetPosition((FrameworkElement)e.OriginalSource);
            Coords = string.Format("{0}:{1}", pos.X, pos.Y);
        }
    }
}