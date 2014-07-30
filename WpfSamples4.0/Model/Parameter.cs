using System.ComponentModel;
using WpfSamples40.Annotations;
using WpfSamples40.Properties;

namespace WpfSamples40.Model
{
    public class Parameter : INotifyPropertyChanged
    {
        public static string PERCENT_MEASURE = "Percent";
        public static string PART_MEASURE = "Part";

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        private string _measure;
        public string Measure
        {
            get { return _measure; }
            set
            {
                if (value == _measure) return;
                _measure = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}