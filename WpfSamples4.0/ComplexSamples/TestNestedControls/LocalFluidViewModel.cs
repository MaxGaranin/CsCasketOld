using GalaSoft.MvvmLight;
using WpfSamples40.ComplexSamples.FluidModels;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    public class LocalFluidViewModel : ViewModelBase
    {
        private FluidModel _localFluidModel;
        public FluidModel LocalFluidModel
        {
            get { return _localFluidModel; }
            set { _localFluidModel = value; RaisePropertyChanged("LocalFluidModel"); }
        }
    }
}