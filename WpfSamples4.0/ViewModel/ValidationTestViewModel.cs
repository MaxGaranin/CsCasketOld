using System.Windows.Markup;
using GalaSoft.MvvmLight;

namespace WpfSamples40.ViewModel
{
    public class ValidationTestViewModel : ViewModelBase
    {
        public ValidationTestViewModel()
        {
            WaterCut = 0.6;
            MyLength = 3;
        }

        private double _waterCut;

        public double WaterCut
        {
            get { return _waterCut; }
            set
            {
                _waterCut = value;
                RaisePropertyChanged("WaterCut");
            }
        }

        private double _myDoubleValue;

        public double MyDoubleValue
        {
            get { return _myDoubleValue; }
            set
            {
                _myDoubleValue = value;
                RaisePropertyChanged("MyDoubleValue");
            }
        }

        private int _myLength;

        public int MyLength
        {
            get { return _myLength; }
            set
            {
                if (Equals(value, _myLength)) return;
                _myLength = value;
                RaisePropertyChanged("MyLength");
            }
        }
    }
}