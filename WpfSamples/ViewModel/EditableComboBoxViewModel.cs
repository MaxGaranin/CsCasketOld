using System.Collections;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace WpfSamples.ViewModel
{
    public class EditableComboBoxViewModel : ObservableObject
    {
        public EditableComboBoxViewModel()
        {
            Students = new ObservableCollection<Student>
            {
                new Student {StudentName = "Amir"},
                new Student {StudentName = "Asif"},
                new Student {StudentName = "Catherine"},
                new Student {StudentName = "Cindrella"},
                new Student {StudentName = "David"},
                new Student {StudentName = "Ellis"},
                new Student {StudentName = "Farooq"},
                new Student {StudentName = "Muhammad"},
                new Student {StudentName = "Saleem"},
                new Student {StudentName = "Usman"}
            };
        }

        public ObservableCollection<Student> Students { get; set; }

        #region Add items to combobox

        private string _selectedItem;

        private ObservableCollection<string> _items = new ObservableCollection<string>()
        {
            "One",
            "Two",
            "Three",
            "Four",
            "Five",
        };

        public IEnumerable Items
        {
            get { return _items; }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public string NewItem
        {
            set
            {
                if (SelectedItem != null) return;
                
                if (!string.IsNullOrEmpty(value))
                {
                    _items.Add(value);
                    SelectedItem = value;
                }
            }
        }

        #endregion
    }

    public class Student
    {
        public string StudentName { get; set; }
    }
}