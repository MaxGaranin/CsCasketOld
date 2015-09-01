using System.Collections.ObjectModel;

namespace WpfSamples40.ViewModel
{
    public class EditableComboBoxViewModel
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
    }

    public class Student
    {
        public string StudentName { get; set; }
    }
}