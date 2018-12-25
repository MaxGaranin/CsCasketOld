using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfSamples.View.Async.TaskWithDialog
{
    public partial class SomeDialogView : Window, INotifyPropertyChanged
    {
        public SomeDialogView()
        {
            InitializeComponent();

            this.LayoutRoot.DataContext = this;
        }

        private bool _isPermissionGranted;

        public bool IsPermissionGranted
        {
            get { return _isPermissionGranted; }
            set
            {
                if (Equals(value, _isPermissionGranted)) return;
                _isPermissionGranted = value;
                RaisePropertyChanged("IsPermissionGranted");
            }
        }

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
