using GalaSoft.MvvmLight;

namespace WpfSamples40.ViewModel
{
    public class ValidationTestModel : ViewModelBase
    {
        public ValidationTestModel()
        {
            WaterCut = 0.6;
        }

        private double _waterCut;
        public double WaterCut
        {
            get { return _waterCut; }
            set { _waterCut = value; RaisePropertyChanged("WaterCut"); }
        }

        private double _myDoubleValue;
        public double MyDoubleValue
        {
            get { return _myDoubleValue; }
            set { _myDoubleValue = value; RaisePropertyChanged("MyDoubleValue");}
        }
    }
}