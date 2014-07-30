using System.ComponentModel;
using WpfSamples40.Annotations;

namespace WpfSamples40.ComplexSamples.GraphEditor.Model
{
    public class Coord : INotifyPropertyChanged
    {
        private double _x;
        private double _y;

        public double X
        {
            get { return _x; }
            set
            {
                if (value.Equals(_x)) return;
                _x = value;
                RaisePropertyChanged("X");
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (value.Equals(_y)) return;
                _y = value;
                RaisePropertyChanged("Y");
            }
        }

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