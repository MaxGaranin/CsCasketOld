using System.Collections.ObjectModel;

namespace WpfSamples40.ViewModel
{
    public class Place
    {
        public string Name { get; set; }
        public string State { get; set; }
    } 

    public class MultiTriggerViewModel
    {
        public MultiTriggerViewModel()
        {
            Places = new ObservableCollection<Place>()
                {
                    new Place() {Name = "Max", State = "qwerty"},
                    new Place() {Name = "Vasya", State = "WA"},
                    new Place() {Name = "Portland", State = "OR"},
                };
        }

        public ObservableCollection<Place> Places { get; set; }
    }
}