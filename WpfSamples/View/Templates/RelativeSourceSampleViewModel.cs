using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace WpfSamples.View.Templates
{
    public class RelativeSourceSampleViewModel : ViewModelBase
    {
        public RelativeSourceSampleViewModel()
        {
            Name = "Zorro";
            MyItems = new ObservableCollection<IMyItem>
            {
                new MyIntItem { Value = 5},
                new MyStringItem { Value = "Hello"},
                new MyOutStringItem(),
            };
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }

        private ObservableCollection<IMyItem> _myItems;

        public ObservableCollection<IMyItem> MyItems
        {
            get { return _myItems; }
            set { Set("MyItems", ref _myItems, value); }
        }
    }

    public interface IMyItem
    {
        Type Type { get; }
    }

    public abstract class MyItem : IMyItem
    {
        public virtual Type Type => typeof(object);

        public virtual object ObjectValue { get; set; }
    }

    public class MyItem<T> : MyItem
    {
        public override Type Type => typeof(T);

        public T Value { get; set; }

        public override object ObjectValue
        {
            get => Value;
            set => Value = (T) value;
        }
    }

    public class MyIntItem : MyItem<int>
    {
    }

    public class MyStringItem : MyItem<string>
    {
    }

    public class MyOutStringItem : MyItem<string>
    {
    }
}