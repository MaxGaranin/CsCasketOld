using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;
using Telerik.Windows.Controls;
using WpfSamples40.View;
using WpfSamples40.View.Tables;
using WpfSamples40.ViewModel.Tables;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace WpfSamples40.ViewModel
{

    #region Class TestViewModelLauncher

    public class TestViewModelLauncher
    {
        public static void Run()
        {
            var view = new TestView();

            // 1 способ
            var viewModel = new TestViewModel();
            viewModel.StringValue = "QQQQQ";
            viewModel.MyComplexValue = new ComplexValue() {Name = "Vasya"};
            viewModel.MyComplexValues = new ObservableCollection<ComplexValue>()
            {
                new ComplexValue() {Name = "Orig"}
            };
            view.DataContext = viewModel;

            // 2 способ
            //            var viewModel = view.DataContext as TestViewModel;
            //            viewModel.StringValue = "EEEEE";

            view.Show();
        }
    }

    #endregion

    #region Class TestViewModel

    public class TestViewModel : ViewModelBase
    {
        #region Properties

        private string _stringValue;

        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                if (_stringValue == value) return;
                _stringValue = value;
                RaisePropertyChanged("StringValue");
            }
        }

        private ComplexValue _myComplexValue;

        public ComplexValue MyComplexValue
        {
            get { return _myComplexValue; }
            set
            {
                if (Equals(_myComplexValue, value)) return;
                _myComplexValue = value;
                RaisePropertyChanged("MyComplexValue");
            }
        }

        private double _doubleValue;

        public double DoubleValue
        {
            get { return _doubleValue; }
            set
            {
                if (Equals(value, _doubleValue)) return;
                _doubleValue = value;
                RaisePropertyChanged("DoubleValue");
            }
        }

        private ObservableCollection<ComplexValue> _myComplexValues;

        public ObservableCollection<ComplexValue> MyComplexValues
        {
            get { return _myComplexValues; }
            set
            {
                if (Equals(_myComplexValues, value)) return;
                _myComplexValues = value;
                RaisePropertyChanged("MyComplexValues");
            }
        }

        #endregion

        #region UpdateCommand

        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand
                       ?? (_updateCommand = new RelayCommand(Update));
            }
        }

        public void Update()
        {
            MyComplexValue.Name = "New Name";
            MyComplexValues[0].Name = "New QQQ";
        }

        #endregion

        #region OpenViewCommand

        private RelayCommand _openViewCommand;

        public RelayCommand OpenViewCommand
        {
            get
            {
                return _openViewCommand ??
                       (_openViewCommand = new RelayCommand(OpenView));
            }
        }

        private void OpenView()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var view = new RadGridViewTestView();
            var viewModel = new RadGridViewTestViewModel();
            view.DataContext = viewModel;
            view.Show();

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;

            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds/10);
            Console.WriteLine("Open GridView " + elapsedTime);
        }

        #endregion

        #region ShowMessageCommand

        private RelayCommand _showMessageBoxCommand;

        public RelayCommand ShowMessageBoxCommand
        {
            get
            {
                return _showMessageBoxCommand
                       ?? (_showMessageBoxCommand = new RelayCommand(ShowMessageBox));
            }
        }

        private void ShowMessageBox()
        {
            RadWindow.Alert("Привет");
        }

        #endregion
    }

    #endregion

    #region Class ComplexValue

    public class ComplexValue : INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    #endregion
}