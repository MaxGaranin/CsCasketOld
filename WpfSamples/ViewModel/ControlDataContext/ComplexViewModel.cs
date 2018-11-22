using GalaSoft.MvvmLight;

namespace WpfSamples.ViewModel.ControlDataContext
{
    public class ComplexViewModel : ViewModelBase
    {
        private FirstViewModel _fisrtViewModel;
        private SecondViewModel _secondViewModel;

        public FirstViewModel FirstViewModel
        {
            get { return _fisrtViewModel; }
            set
            {
                if (Equals(value, _fisrtViewModel)) return;
                _fisrtViewModel = value;
                RaisePropertyChanged("FirstViewModel");
            }
        }

        public SecondViewModel SecondViewModel
        {
            get { return _secondViewModel; }
            set
            {
                if (Equals(value, _secondViewModel)) return;
                _secondViewModel = value;
                RaisePropertyChanged("SecondViewModel");
            }
        }

    }
}