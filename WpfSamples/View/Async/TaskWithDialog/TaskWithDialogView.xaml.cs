using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

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
            new SomeDialogView().ShowDialog();

            IsInWork = true;
            Console.WriteLine("Further execution...");
            await Task.Delay(1000);

            IsInWork = false;
            MessageBox.Show("Done!");

            Console.WriteLine("Done.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}