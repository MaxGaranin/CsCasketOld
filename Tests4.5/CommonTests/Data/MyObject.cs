using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tests45.Annotations;

namespace Tests45.CommonTests.Data
{
    class MyObject : INotifyPropertyChanged
    {
        private double _a;
        private string _name;
        private Correlation _correlation;

        public Correlation Correlation
        {
            get { return _correlation; }
            set
            {
                if (Equals(value, _correlation)) return;
                if(_correlation!=null)
                    _correlation.PropertyChanged -= _correlation_PropertyChanged;
                _correlation = value;
                if (_correlation != null)
                    _correlation.PropertyChanged += _correlation_PropertyChanged;
                RaisePropertyChanged();
            }
        }

        void _correlation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) 
                PropertyChanged(sender, e);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged();
                }

            }
        }

        public double MyDouble
        {
            get { return _a; }
            set
            {
                if (_a != value)
                {
                    _a = value;
                    RaisePropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}