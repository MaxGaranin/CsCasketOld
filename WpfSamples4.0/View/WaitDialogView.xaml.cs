using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfSamples40.Annotations;
using WpfSamples40.Properties;

namespace WpfSamples40.View
{
    /// <summary>
    /// Interaction logic for WaitDialogView.xaml
    /// </summary>
    public partial class WaitDialogView : Window, INotifyPropertyChanged
    {
        public event EventHandler Cancel;

        private Bitmap _bitmap;
        private BitmapSource _source;
        
        public WaitDialogView()
        {
            InitializeComponent();

            LayoutRoot.DataContext = this;
            UseTelerikIndicator = false;

//            Loaded += WaitDialogView_Loaded;
        }

        #region Gif Animation

//        private void WaitDialogView_Loaded(object sender, RoutedEventArgs e)
//        {
//            _source = GetSource();
//            WaitImage.Source = _source;
//            ImageAnimator.Animate(_bitmap, OnFrameChanged);
//        }
//
//        private BitmapSource GetSource()
//        {
//            if (_bitmap == null)
//            {
//                _bitmap = new Bitmap(@".\Resources\Images\loading3.gif");
//            }
//            IntPtr handle = IntPtr.Zero;
//            handle = _bitmap.GetHbitmap();
//            return Imaging.CreateBitmapSourceFromHBitmap(
//                handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
//        }
//
//        private void OnFrameChanged(object sender, EventArgs e)
//        {
//            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
//                                    new Action(FrameUpdatedCallback));
//        }
//
//        private void FrameUpdatedCallback()
//        {
//            ImageAnimator.UpdateFrames();
//            if (_source != null)
//                _source.Freeze();
//            _source = GetSource();
//            WaitImage.Source = _source;
//            InvalidateVisual();
//        }

        #endregion

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Cancel != null)
                Cancel(this, EventArgs.Empty);

            this.Close();
        }

        private bool _useTelerikIndicator;
        public bool UseTelerikIndicator
        {
            get { return _useTelerikIndicator; }
            set
            {
                if (value.Equals(_useTelerikIndicator)) return;
                _useTelerikIndicator = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
 
	    #endregion    
    }
}
