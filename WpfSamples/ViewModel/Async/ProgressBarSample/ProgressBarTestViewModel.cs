using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProgressBarView = WpfSamples.View.Async.ProgressBarSample.ProgressBarView;

namespace WpfSamples.ViewModel.Async.ProgressBarSample
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

            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += (sender, args) =>
            {
                pbViewModel.Value = args.ProgressPercentage;
            };
            worker.RunWorkerCompleted += (sender, args) =>
            {
                if (args.Cancelled)
                {
                    MessageBox.Show("Расчет прерван");
                }
                pbView.Close();
            };

            pbViewModel.Cancelled += (sender, args) =>
            {
                worker.CancelAsync();
            };

            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            for (int i = 0; i <= 100; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                worker.ReportProgress(i);
                Thread.Sleep(100);
            }            
        }

        #endregion
    }
}