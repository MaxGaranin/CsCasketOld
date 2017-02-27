using System;
using System.ComponentModel;
using System.Threading;
using GalaSoft.MvvmLight.Threading;
using WpfSamples40.View.Async;
using WpfSamples40.View.Async.ProgressBarSample;
using WpfSamples40.ViewModel.Async;

namespace WpfSamples40.Services
{
    public class ProgressBarService
    {
        private ProgressBarViewModel _progressViewModel;
        private ProgressBarView _progressView;

        public void Show(BackgroundWorker bgw)
        {
            DispatcherHelper.UIDispatcher.BeginInvoke(new Action(() =>
            {
                _progressViewModel = new ProgressBarViewModel();
                _progressViewModel.Cancelled += (s, e) => { bgw.CancelAsync(); };

                _progressView = new ProgressBarView { DataContext = _progressViewModel };
                DispatcherHelper.UIDispatcher.BeginInvoke(new Action(() => { _progressView.Show(); }));
            }));
        }

        public void Show(CancellationTokenSource cts)
        {
            DispatcherHelper.UIDispatcher.BeginInvoke(new Action(() =>
            {
                _progressViewModel = new ProgressBarViewModel();
                _progressViewModel.Cancelled += (s, e) => { cts.Cancel(); };

                _progressView = new ProgressBarView {DataContext = _progressViewModel};
                DispatcherHelper.UIDispatcher.BeginInvoke(new Action(() => { _progressView.Show(); }));
            }));
        }

        public void Close()
        {
            if (_progressViewModel == null || _progressView == null) return;
            _progressView.Close();
        }

        public void Update(int value)
        {
            if (_progressViewModel == null || _progressView == null) return;

            _progressViewModel.Value = value;
        }
    }
}