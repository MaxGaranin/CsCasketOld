using GalaSoft.MvvmLight;
using WpfSamples40.ComplexSamples.FluidModels;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    public class BankFluidViewModel : ViewModelBase
    {
        private FluidModel _selectedBankFluidModel;
        private bool _isBankModel;

        public BankFluidViewModel()
        {
        }

        public FluidModel SelectedBankFluidModel
        {
            get { return _selectedBankFluidModel; }
            set { _selectedBankFluidModel = value; RaisePropertyChanged("SelectedBankFluidModel"); }
        }

        public bool IsBankModel
        {
            get { return _isBankModel; }
            set
            {
                _isBankModel = value;
                RaisePropertyChanged("IsBankModel");
            }
        }
    }
}