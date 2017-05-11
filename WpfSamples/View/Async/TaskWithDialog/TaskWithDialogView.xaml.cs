using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
            RadBusyIndicator.IsBusy = true;
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

        #region Task with dialog

        private void OpenDialogButton_OnClick(object sender, RoutedEventArgs e)
        {
            RunWithDialog();
        }

        private async void RunWithDialog()
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

        #endregion

        #region Task with process

        private async void RunProcessButton_OnClick(object sender, RoutedEventArgs e)
        {
            await RunProcess();

            DispatcherHelper.UIDispatcher.BeginInvoke(
                new Action(() => { MessageBox.Show("Резервное копирование завершено!"); }));
        }

        private async Task RunProcess()
        {
            // Копирование
            SomeDialogView dialogView = null;
            DispatcherHelper.UIDispatcher.BeginInvoke(
                new Action(() =>
                {
                    dialogView = new SomeDialogView {InfoTextBlock = {Text = "Подготовка к резервному копированию..."}};
                    dialogView.ShowDialog();
                }));

            await CopyDirectory(@"d:\work835\Dev\Sources\From Books\", @"d:\work835\Temp\Copy\");
            await Task.Delay(1000);

            DispatcherHelper.UIDispatcher.Invoke(
                () => { if (dialogView != null) dialogView.Close(); });

            // Архивирование
            var fileName = @"""c:\Program Files\WinRAR\Rar.exe""";
            var args = @"a -ep1 ""d:\work835\Temp\Copy.rar"" ""d:\work835\Temp\Copy""";
            await RunProcessAsync(fileName, args);
        }

        public static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = fileName,
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync(process).ConfigureAwait(false);
            }
        }

        private static Task<int> RunProcessAsync(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            //process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            //process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                //you may allow for the process to be re-used (started = false) 
                //but I'm not sure about the guarantees of the Exited event in such a case
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return tcs.Task;
        }

        private static async Task CopyDirectory(string sourcePath, string destinationPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

                foreach (string newPath in Directory.EnumerateFiles(dirPath))
                {
                    await CopyFileAsync(newPath, newPath.Replace(sourcePath, destinationPath));
                }
            }

            foreach (string newPath in Directory.EnumerateFiles(sourcePath))
            {
                await CopyFileAsync(newPath, newPath.Replace(sourcePath, destinationPath));
            }
        }

        private static async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath, FileMode.Open))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}