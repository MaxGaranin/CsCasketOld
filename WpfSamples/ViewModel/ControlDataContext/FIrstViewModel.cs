using GalaSoft.MvvmLight;

namespace WpfSamples.ViewModel.ControlDataContext
{
    public class FirstViewModel : ViewModelBase
    {
        private int _intValue;

        public int IntValue
        {
            get { return _intValue; }
            set
            {
                if (Equals(value, _intValue)) return;
                _intValue = value;
                RaisePropertyChanged("IntValue");
            }
        }

    }
}