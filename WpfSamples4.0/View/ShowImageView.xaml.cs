using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using WpfSamples40.Properties;

namespace WpfSamples40.View
{
    public partial class ShowImageView : Window, INotifyPropertyChanged
    {
        public ShowImageView()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region FileName

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (Equals(value, _fileName)) return;
                _fileName = value;
                RaisePropertyChanged("FileName");

                ShowImage(value);
            }
        } 
        #endregion

        #region IsImage

        private bool _isImage;
        public bool IsImage
        {
            get { return _isImage; }
            set
            {
                if (Equals(value, _isImage)) return;
                _isImage = value;
                RaisePropertyChanged("IsImage");
            }
        } 
        #endregion

        #region MyImageSource

        private ImageSource _myImageSource;
        public ImageSource MyImageSource
        {
            get { return _myImageSource; }
            set
            {
                if (Equals(value, _myImageSource)) return;
                _myImageSource = value;
                RaisePropertyChanged("MyImageSource");
            }
        } 
        #endregion


        #region SelectFile

        private RelayCommand _selectFileCommand;

        public ICommand SelectFileCommand
        {
            get
            {
                return _selectFileCommand
                       ?? (_selectFileCommand = new RelayCommand(SelectFile));
            }
        }

        private void SelectFile()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All(*.*)|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                FileName = fileDialog.FileName;
            }
        }

        #endregion


        #region Helpers

        private void ShowImage(string fileName)
        {
            IsImage = CheckIsImage(fileName);
            if (!IsImage)
            {
                MyImageSource = null;
                return;
            }

            MyImageSource = new BitmapImage(new Uri(fileName));
        }

        private bool CheckIsImage(string fileName)
        {
            try
            {
                var image = System.Drawing.Image.FromFile(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}