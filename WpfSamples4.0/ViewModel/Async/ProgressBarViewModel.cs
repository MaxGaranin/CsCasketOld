using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WpfSamples40.ViewModel.Async
{
    public class ProgressBarViewModel : ViewModelBase
    {
        public ProgressBarViewModel()
        {
            Title = "Выполнение...";
            Minimum = 0;
            Maximum = 100;
            Value = 73;
        }

        #region Title

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (Equals(value, _title)) return;
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        #endregion

        #region Minimum

        private double _minimum;

        public double Minimum
        {
            get { return _minimum; }
            set
            {
                if (Equals(value, _minimum)) return;
                _minimum = value;
                RaisePropertyChanged("Minimum");
            }
        }

        #endregion

        #region Maximum

        private double _maximum;

        public double Maximum
        {
            get { return _maximum; }
            set
            {
                if (Equals(value, _maximum)) return;
                _maximum = value;
                RaisePropertyChanged("Maximum");
            }
        }

        #endregion

        #region Value

        private double _value;

        public double Value
        {
            get { return _value; }
            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        #endregion

        #region Cancel

        public event EventHandler Cancelled;

        private RelayCommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand
                       ?? (_cancelCommand = new RelayCommand(Cancel));
            }
        }

        private void Cancel()
        {
            if (Cancelled != null) Cancelled(this, EventArgs.Empty);
        }

        #endregion
    }
}