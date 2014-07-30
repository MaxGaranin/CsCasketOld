using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfSamples40.ComplexSamples.TestNestedControls
{
    public class WellModelViewModel : ViewModelBase
    {
        private BankFluidViewModel _bankFluidViewModel;
        private LocalFluidViewModel _localFluidViewModel;
        private bool _isBankModel;

        public BankFluidViewModel BankFluidViewModel
        {
            get { return _bankFluidViewModel; }
            set { _bankFluidViewModel = value; RaisePropertyChanged("BankFluidViewModel"); }
        }

        public LocalFluidViewModel LocalFluidViewModel
        {
            get { return _localFluidViewModel; }
            set { _localFluidViewModel = value; RaisePropertyChanged("LocalFluidViewModel"); }
        }

        public bool IsBankModel
        {
            get { return _isBankModel; }
            set { _isBankModel = value; RaisePropertyChanged("IsBankModel"); }
        }

        #region ApplyCommand

        private RelayCommand _applyCommand;
        public RelayCommand ApplyCommand
        {
            get
            {
                if (_applyCommand == null)
                    _applyCommand = new RelayCommand(Apply);
                return _applyCommand;
            }
        }

        public void Apply()
        {
            Console.WriteLine(BankFluidViewModel.SelectedBankFluidModel);
        }
        #endregion

    }
}