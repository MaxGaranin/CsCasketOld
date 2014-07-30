using System.ComponentModel;

namespace Tests45.Common
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