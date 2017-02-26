using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace WpfSamples40.View.Async.TaskWithDialog
{
    public partial class TaskWithDialogView : Window, INotifyPropertyChanged
    {
        public TaskWithDialogView()
        {
            InitializeComponent();

            this.LayoutRoot.DataContext = this;
            IsInWork = false;
        }

        #region IsInWork

        private bool _isInWork;

        public bool IsInWork
        {
            get { return _isInWork; }
            set
            {
                if (Equals(value, _isInWork)) return;
                _isInWork = value;
                RaisePropertyChanged("IsInWork");
            }
        }

        #endregion

        private void OpenDialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            Run();
        }

        private async void Run()
        {
            IsInWork = true;
            await Task.Delay(2000);

            IsInWork = false;
            await DispatcherHelper.UIDispatcher.BeginInvoke(
                new Action(() => { new SomeDialogView().ShowDialog(); }));

            IsInWork = true;
            Console.WriteLine("Further execution...");
            await Task.Delay(1000);

            IsInWork = false;
            await DispatcherHelper.UIDispatcher.BeginInvoke(
                new Action(() => { MessageBox.Show("Done!"); }));

            Console.WriteLine("Done.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}