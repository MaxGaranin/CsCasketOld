using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfSamples40.View;

namespace WpfSamples40.ViewModel
{
    public class ProgressBarTestViewModel : ViewModelBase
    {


        #region Process

        private RelayCommand _processCommand;

        public ICommand ProcessCommand
        {
            get
            {
                return _processCommand
                       ?? (_processCommand = new RelayCommand(Process));
            }
        }

        private void Process()
        {
            var pbViewModel = new ProgressBarViewModel();
            var pbView = new ProgressBarView { DataContext = pbViewModel };
            pbView.Show();
        }

        #endregion
    }
}