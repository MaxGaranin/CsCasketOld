using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Test40.ObservableNotifiableCollection
{
    [Serializable]
    [DataContract]
    public class Bank
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Group> Groups { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Group
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Model> Models { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Model
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Data { get; set; }

        [DataMember]
        public Correlation Correlation { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Correlation : INotifyPropertyChanged
    {
        private int _value;
        [DataMember]
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (PropertyChanged != null) 
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        [DataMember] 
        public Dictionary<string, int> StringToInt;

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
    [DataContract]
    public class Well
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Model Model { get; set; }
    }
}