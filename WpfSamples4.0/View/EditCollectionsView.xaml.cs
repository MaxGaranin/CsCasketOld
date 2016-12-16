using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using AutoMapper;
using Omu.ValueInjecter;
using WpfSamples40.Properties;

namespace WpfSamples40.View
{
    /// <summary>
    /// Interaction logic for EditCollectionsView.xaml
    /// </summary>
    public partial class EditCollectionsView : Window, INotifyPropertyChanged
    {
        private ObservableCollection<MyObject> _observableCollection;
        private MyObject _currentObject;
        private MyObject _selectedItem;

        private int _counter = 1;

        #region MyObject class

        [Serializable]
        [DataContract]
		public class MyObject : INotifyPropertyChanged
        {
            private string _name;
		    private ObservableCollection<OtherObject> _otherObjects = new ObservableCollection<OtherObject>();

            [DataMember]
		    public string Name
            {
                get { return _name; }
                set { _name = value; OnPropertyChanged("Name"); }
            }

            [DataMember]
		    public ObservableCollection<OtherObject> OtherObjects
		    {
		        get { return _otherObjects; }
		        set
		        {
		            if (Equals(value, _otherObjects)) return;
		            _otherObjects = value;
		            OnPropertyChanged();
		        }
		    }

            public override string ToString()
            {
                return Name;
            }

            [field: NonSerialized]
		    public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Serializable]
        [DataContract]
        public class OtherObject
        {
            [DataMember]
            public object A1 { get; set; }
        }
 
	    #endregion

        public EditCollectionsView()
        {
            InitializeComponent();

            DataContext = this;

            Mapper.CreateMap<MyObject, MyObject>();

            MyObjects = new ObservableCollection<MyObject>()
                {
                    new MyObject() { Name = "Первый"},
                    new MyObject() { Name = "Второй"},
                    new MyObject() { Name = "Третий"}
                };

            CurrentObject = new MyObject() { Name = "test" };
            MyObjects.Insert(0, CurrentObject);

            SelectedItem = MyObjects.FirstOrDefault();
        }

        public ObservableCollection<MyObject> MyObjects
        {
            get { return _observableCollection; }
            set
            {
                _observableCollection = value; 
                OnPropertyChanged("MyObjects");
            }
        }

        public MyObject CurrentObject
        {
            get { return _currentObject; }
            set
            {
                _currentObject = value; OnPropertyChanged("CurrentObject");
            }
        }

        public MyObject SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value; OnPropertyChanged("SelectedItem");
            }
        }

        private void TestButtonClick(object sender, RoutedEventArgs e)
        {
            CurrentObject = null;
        }

        private void RemoveItemButtonClick(object sender, RoutedEventArgs e)
        {
            MyObjects.Remove(SelectedItem);
            SelectedItem = _observableCollection.FirstOrDefault();
        }

        private void AddItemButtonClick(object sender, RoutedEventArgs e)
        {
            var newObject = new MyObject() {Name = "New object"};
            MyObjects.Add(newObject);
            SelectedItem = newObject;
        }

        private void EditItemButtonClick(object sender, RoutedEventArgs e)
        {
            var objectCopy = Mapper.Map<MyObject>(SelectedItem);
            objectCopy.Name = "Edited object " + _counter++;

            var index = MyObjects.IndexOf(SelectedItem);
            MyObjects[index].InjectFrom(objectCopy);
            SelectedItem = MyObjects[index];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
