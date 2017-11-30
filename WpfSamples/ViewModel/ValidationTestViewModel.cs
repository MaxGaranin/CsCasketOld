using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace WpfSamples40.ViewModel
{
    public class ValidationTestViewModel : ViewModelBase
    {
        public ValidationTestViewModel()
        {
            WaterCut = 0.6;
            MyDoubleValue = 11.234;
            MyLength = 3;

            MyList = new ObservableCollection<SomeType>
            {
                new SomeType
                {
                    DateValue = new DateTime(2016, 1, 22),
                    DoubleValue = 4.55,
                    StringValue = "aaaa"
                },
                new SomeType
                {
                    DateValue = new DateTime(2016, 1, 22),
                    DoubleValue = 4.55,
                    StringValue = "aaaa"
                },
                new SomeType
                {
                    DateValue = new DateTime(2016, 1, 22),
                    DoubleValue = 4.55,
                    StringValue = "aaaa"
                },
            };
        }

        private double _waterCut;

        public double WaterCut
        {
            get { return _waterCut; }
            set
            {
                _waterCut = value;
                RaisePropertyChanged("WaterCut");
            }
        }

        private double _myDoubleValue;

        public double MyDoubleValue
        {
            get { return _myDoubleValue; }
            set
            {
                _myDoubleValue = value;
                RaisePropertyChanged("MyDoubleValue");
            }
        }

        private double _myLength;

        public double MyLength
        {
            get { return _myLength; }
            set
            {
                if (Equals(value, _myLength)) return;
                _myLength = value;
                RaisePropertyChanged("MyLength");
            }
        }

        private ObservableCollection<SomeType> _myList;

        public ObservableCollection<SomeType> MyList
        {
            get { return _myList; }
            set
            {
                if (Equals(value, _myList)) return;
                _myList = value;
                RaisePropertyChanged("MyList");
            }
        }
    }

    public class SomeType : ObservableObject
    {
        private double _doubleValue;

        public double DoubleValue
        {
            get { return _doubleValue; }
            set { Set("DoubleValue", ref _doubleValue, value); }
        }

        private DateTime _dateValue;

        public DateTime DateValue
        {
            get { return _dateValue; }
            set { Set("DateValue", ref _dateValue, value); }
        }

        private string _stringValue;

        public string StringValue
        {
            get { return _stringValue; }
            set { Set("StringValue", ref _stringValue, value); }
        }
    }
}