using GalaSoft.MvvmLight;

namespace WpfSamples40.ViewModel.HelperClasses
{
    public class ModelObject : ObservableObject
    {
        private int _shoeSize;
        public int ShoeSize
        {
            get { return _shoeSize; }
            set { _shoeSize = value; RaisePropertyChanged("ShowSize"); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; RaisePropertyChanged("Height"); }
        }
    }
}