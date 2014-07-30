using System.ComponentModel;

namespace Test40.ObservableNotifiableCollection
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

//                if(_correlation != null)
//                    _correlation.PropertyChanged -= CorrelationPropertyChanged;

                _correlation = value;

//                if (_correlation != null)
//                    _correlation.PropertyChanged += CorrelationPropertyChanged;

                RaisePropertyChanged("Correlation");
            }
        }

        void CorrelationPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null) 
                PropertyChanged(sender, e);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public double MyDouble
        {
            get { return _a; }
            set
            {
                if (_a == value) return;
                _a = value;
                RaisePropertyChanged("MyDouble");
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}