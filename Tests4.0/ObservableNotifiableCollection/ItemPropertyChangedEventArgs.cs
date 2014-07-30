using System.ComponentModel;

namespace Test40.ObservableNotifiableCollection
{
    public delegate void ItemPropertyChangedEventHandler(object sender, ItemPropertyChangedEventArgs args);

    public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        private object _item;

        public ItemPropertyChangedEventArgs(object item, string propertyName)
            : base(propertyName)
        {
            _item = item;
        }

        public object Item
        {
            get { return _item; }
        }
    }
}