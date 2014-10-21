using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfSamples40.ViewModel.Master
{
    // 
    //    Способ инициализации данной ViewModel
    //
    //    var viewModel = new MasterViewModel();
    //    viewModel.TabViewModels = new ObservableCollection<ITabViewModel>
    //    {
    //        new Control1 { Header = "Control1"},
    //        new Control2 { Header = "Control2"},
    //        new Control3 { Header = "Control3"},
    //    };
    //
    //    var view = new MasterView();
    //    view.DataContext = viewModel;
    //    view.Show();

    public class MasterViewModel : ViewModelBase
    {
        private int _selectedIndex;
        private bool _isEnableBack;
        private bool _isEnableForward;
        private bool _isEnableOk;

        #region Properties

        public bool DialogResult { get; set; }

        #region TabViewModels

        private ObservableCollection<ITabViewModel> _tabViewModels;
        public ObservableCollection<ITabViewModel> TabViewModels
        {
            get { return _tabViewModels; }
            set
            {
                if (Equals(value, _tabViewModels)) return;
                _tabViewModels = value;
                RaisePropertyChanged("TabViewModels");

                if (TabViewModels == null || TabViewModels.Count == 0) return;

                _selectedIndex = 0;
                SetTabViewModel(_selectedIndex);
            }
        } 
        #endregion

        #region SelectedTabViewModel

        private ITabViewModel _selectedTabViewModel;
        public ITabViewModel SelectedTabViewModel
        {
            get { return _selectedTabViewModel; }
            set
            {
                if (Equals(value, _selectedTabViewModel)) return;
                _selectedTabViewModel = value;
                RaisePropertyChanged("SelectedTabViewModel");

                UpdateCanExecuteCommands();
            }
        } 
        #endregion

        #endregion

        #region Commands

        #region Back

        private RelayCommand _backCommand;

        public ICommand BackCommand
        {
            get
            {
                return _backCommand
                       ?? (_backCommand = new RelayCommand(Back, CanExecuteBack));
            }
        }

        protected virtual void Back()
        {
            _selectedIndex--;
            SetTabViewModel(_selectedIndex);
        }

        protected virtual bool CanExecuteBack()
        {
            return _isEnableBack;
        }

        #endregion

        #region Forward

        private RelayCommand _forwardCommand;

        public ICommand ForwardCommand
        {
            get
            {
                return _forwardCommand
                       ?? (_forwardCommand = new RelayCommand(Forward, CanExecuteForward));
            }
        }

        protected virtual void Forward()
        {
            _selectedIndex++;
            SetTabViewModel(_selectedIndex);
        }

        protected virtual bool CanExecuteForward()
        {
            return _isEnableForward;
        }

        #endregion

        #region Ok

        private RelayCommand _okCommand;

        public ICommand OkCommand
        {
            get
            {
                return _okCommand
                       ?? (_okCommand = new RelayCommand(Ok, CanExecuteOk));
            }
        }

        protected virtual void Ok()
        {
            DialogResult = true;
        }

        protected virtual bool CanExecuteOk()
        {
            return _isEnableOk;
        }
        #endregion

        #region Cancel

        private RelayCommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand
                       ?? (_cancelCommand = new RelayCommand(Cancel));
            }
        }

        protected virtual void Cancel()
        {
            DialogResult = false;
        }

        #endregion 

        #endregion

        #region Private methods

        private void SetTabViewModel(int index)
        {
            SelectedTabViewModel = TabViewModels[index];
        }

        private void UpdateCanExecuteCommands()
        {
            var index = TabViewModels.IndexOf(SelectedTabViewModel);
            _isEnableBack = index > 0;
            _isEnableForward = index < TabViewModels.Count - 1;
            _isEnableOk = index == TabViewModels.Count - 1;
        } 
        #endregion
    }
}