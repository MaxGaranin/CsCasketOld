using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using WpfSamples40.Helpers;
using WpfSamples40.Properties;

namespace WpfSamples40.View.Async.BackgroundWorkerSample
{
    public partial class BackgroundWorkerView : Window, INotifyPropertyChanged
    {
        private readonly BackgroundWorker _backgroundWorker;
        private WaitDialogView _waitView;

        public BackgroundWorkerView()
        {
            InitializeComponent();

            DataContext = this;

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            MyTestObject = new MyObject()
                {
                    MyDouble = 134.34, 
                    MyString = "Hello, world",
                    IntList = new ObservableCollection<int>()
                };
        }

        #region Properties

        public MyObject MyTestObject
        {
            get { return _myTestObject; }
            set
            {
                if (Equals(value, _myTestObject)) return;
                _myTestObject = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Change property with binding in other thread

        private void TestButton_OnClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(delegate()
            {
                // Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                //    (ThreadStart)(() => MyTestObject.MyString = "Starting..."));

                // И так работает
                MyTestObject.MyString = "Starting...";

                Thread.Sleep(2000);
                MyTestObject.MyString = "Processing...";
                Thread.Sleep(2000);
                MyTestObject.MyString = "Finished.";
            });

            thread.Start();
        }
        
        #endregion

        #region BackgroundWorker Async operation

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MyWork(MyTestObject);

            if (_backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void MyWork(MyObject parameter)
        {
            Action<int> workMethod = i => AutoNumberTextBox.Text = i.ToString();

            var n = 100;
            for (int i = 0; i < n; i++)
            {
                if (_backgroundWorker.CancellationPending)
                    return;

                if (_backgroundWorker.WorkerReportsProgress)
                    _backgroundWorker.ReportProgress(i * 100 / n);

                Thread.Sleep(TimeSpan.FromSeconds(0.05));

                AutoNumberTextBox.Dispatcher.BeginInvoke(DispatcherPriority.Normal, workMethod, i);

                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart) (() =>
                    {
                        parameter.MyString = i.ToString();
                        parameter.IntList.Add(i);
                    }));
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            StatusTextBox.Text = string.Format("{0} %", e.ProgressPercentage);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                StatusTextBox.Text = "Отмена";
            }
            else if (e.Error != null)
            {
                StatusTextBox.Text = string.Format("Ошибка: {0}", e.Error.Message);
            }
            else
            {
                if (UseWaitDialog.IsChecked != null && UseWaitDialog.IsChecked.Value)
                {
                    _waitView.Close();
                }
                ProgressBar.Value = 0;
                StatusTextBox.Text = "Расчет выполнен";
            }
        }

        private void BackgroundWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (UseWaitDialog.IsChecked != null && UseWaitDialog.IsChecked.Value)
            {
                _waitView = new WaitDialogView();
                _waitView.UseTelerikIndicator = UseTelerikIndicatorCheckBox.IsChecked.Value;
                _waitView.Cancel += (o, args) => _backgroundWorker.CancelAsync();
                _waitView.Show();
            }

            _backgroundWorker.RunWorkerAsync();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.CancelAsync();
        }
        
        #endregion

        #region DoEvents Sync operation

        private void DoEventsButton_Click(object sender, RoutedEventArgs e)
        {
            _cancelFlag = false;

            if (UseWaitDialog.IsChecked.Value)
            {
                _waitView = new WaitDialogView();
                _waitView.UseTelerikIndicator = UseTelerikIndicatorCheckBox.IsChecked.Value;
                _waitView.Cancel += (o, args) => _cancelFlag = true;
                _waitView.Show();
            }

            LongOperation();

            if (UseWaitDialog.IsChecked.Value)
            {
                _waitView.Close();    
            }

            ProgressBar.Value = 0;
            StatusTextBox.Text = "";
        }

        private bool _cancelFlag;
        private MyObject _myTestObject;

        private void LongOperation()
        {
            for (int i = 0; i < 100; i++)
            {
                DispatcherHelperEx.DoEvents();

                if (_cancelFlag) return;
                Thread.Sleep(TimeSpan.FromSeconds(0.05));

                AutoNumberTextBox.Text = i.ToString();
                ProgressBar.Value = i;
                StatusTextBox.Text = string.Format("{0} %", i);
            }
        }
        
        #endregion

        #region INotifyPropertyChanged members

		public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
 
	    #endregion
    }

    #region MyObject class

    public class MyObject : INotifyPropertyChanged
    {
        private double _myDouble;
        public double MyDouble
        {
            get { return _myDouble; }
            set
            {
                const double EPSILON = 10E-6;
                if (Math.Abs(value - _myDouble) < EPSILON) return;
                _myDouble = value;
                RaisePropertyChanged();
            }
        }

        private string _myString;
        public string MyString
        {
            get { return _myString; }
            set
            {
                if (value == _myString) return;
                _myString = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<int> _intList;
        public ObservableCollection<int> IntList
        {
            get { return _intList; }
            set
            {
                if (Equals(value, _intList)) return;
                _intList = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    #endregion
}
