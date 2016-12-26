using GalaSoft.MvvmLight;

namespace WpfSamples40.ViewModel.ControlDataContext
{
    public class SecondViewModel : ViewModelBase
    {
        private string _stringValue;

        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                if (Equals(value, _stringValue)) return;
                _stringValue = value;
                RaisePropertyChanged("StringValue");
            }
        }

    }
}