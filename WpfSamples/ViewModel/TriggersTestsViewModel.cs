using GalaSoft.MvvmLight;

namespace WpfSamples.ViewModel
{
    public class TriggersTestsViewModel : ViewModelBase
    {
        private bool _isSelected;
        private string _name;

        public TriggersTestsViewModel()
        {
            Name = "Vasya";
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }
    }
}