using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSamples40.View.Async.TaskvsBW
{
    public partial class TaskvsBwView : Window
    {
        private BackgroundWorker _bgw;
        private CancellationTokenSource _cts;

        public TaskvsBwView()
        {
            InitializeComponent();
        }

        private void UseBwButton_OnClick(object sender, RoutedEventArgs e)
        {
            _bgw = new BackgroundWorker();
            var bgw = _bgw;
            bgw.WorkerSupportsCancellation = true;
            bgw.WorkerReportsProgress = true;

            bgw.DoWork += (_, args) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    if (bgw.CancellationPending)
                    {
                        args.Cancel = true;
                        return;
                    }
                    bgw.ReportProgress(i);
                    Thread.Sleep(100);
                }
            };

            bgw.ProgressChanged += (_, args) =>
            {
                BwProgressTextBox.Text = args.ProgressPercentage.ToString();
            };

            bgw.RunWorkerCompleted += (_, args) =>
            {
                if (args.Cancelled)
                {
                    MessageBox.Show("Canceled.");
                }
                else
                {
                    MessageBox.Show("Completed.");                    
                }
            };

            bgw.RunWorkerAsync();
        }

        private void CancelBwButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_bgw != null) 
                _bgw.CancelAsync();
        }

        private async void UseTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            IProgress<int> progressHandler = new Progress<int>(value =>
            {
                TasksProgressTextBox.Text = value.ToString();
            });

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        token.ThrowIfCancellationRequested();
                        progressHandler.Report(i);
                        Thread.Sleep(100);
                    }
                    MessageBox.Show("Completed.");                    
                }, token);

            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Canceled.");                    
            }
        }

        private void CancelTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }
    }
}