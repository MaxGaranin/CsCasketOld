using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Tests45.Common
{
    public class ObservableNotifiableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ItemPropertyChangedEventHandler ItemPropertyChanged;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);

            if (args.NewItems != null)
                foreach (INotifyPropertyChanged item in args.NewItems)
                    item.PropertyChanged += OnItemPropertyChanged;

            if (args.OldItems != null)
                foreach (INotifyPropertyChanged item in args.OldItems)
                    item.PropertyChanged -= OnItemPropertyChanged;
        }

        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (ItemPropertyChanged != null)
                ItemPropertyChanged(this, new ItemPropertyChangedEventArgs(sender, args.PropertyName));
        }
    }
}