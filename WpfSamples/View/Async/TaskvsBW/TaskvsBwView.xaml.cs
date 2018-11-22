using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfSamples.Services;

namespace WpfSamples.View.Async.TaskvsBW
{
    public partial class TaskvsBwView : Window
    {
        private BackgroundWorker _bgw;
        private CancellationTokenSource _cts;

        public TaskvsBwView()
        {
            InitializeComponent();
        }

        #region BackgroundWorker

        private void UseBwButton_OnClick(object sender, RoutedEventArgs e)
        {
            _bgw = new BackgroundWorker();
            var bgw = _bgw;
            bgw.WorkerSupportsCancellation = true;
            bgw.WorkerReportsProgress = true;

            var progressService = new ProgressBarService();
            progressService.Show(_bgw);

            bgw.DoWork += (_, args) =>
            {
                for (int i = 1; i <= 100; i++)
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
                progressService.Update(args.ProgressPercentage);
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
                progressService.Close();
            };

            bgw.RunWorkerAsync();
        }

        private void CancelBwButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_bgw != null)
                _bgw.CancelAsync();
        }

        #endregion

        #region AsyncAwait

        private async void UseTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            var progressService = new ProgressBarService();
            progressService.Show(_cts);

            IProgress<int> progressHandler = new Progress<int>(value =>
            {
                TasksProgressTextBox.Text = value.ToString();
                progressService.Update(value);
            });

            try
            {
                await Task.Run(() =>
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        token.ThrowIfCancellationRequested();
                        progressHandler.Report(i);
                        Thread.Sleep(100);
                    }
                }, token);

                MessageBox.Show("Completed.");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Canceled.");
            }
            finally
            {
                progressService.Close();
            }
        }

        private void CancelTasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        #endregion
    }
}