using System.ComponentModel;
using NUnit.Framework;
using Test40.Annotations;

namespace Test40.Dp
{
    [TestFixture]
    public class DpTests
    {
        public class A : INotifyPropertyChanged
        {
            private ValueContainerA _valueA;

            public ValueContainerA ValueA
            {
                get { return _valueA; }
                set
                {
                    if (value.Equals(_valueA)) return;
                    _valueA = value;
                    OnPropertyChanged("ValueA");
                }
            }

            #region INPC

            public event PropertyChangedEventHandler PropertyChanged;

		    [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }
 
	        #endregion  
        }

        public class B : INotifyPropertyChanged
        {
            private ValueContainerB _valueB;

            public ValueContainerB ValueB
            {
                get { return _valueB; }
                set
                {
                    if (value.Equals(_valueB)) return;
                    _valueB = value;
                    OnPropertyChanged("ValueB");
                }
            }

            #region INPC

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }

        public class ValueContainerA
        {
            public double Value { get; set; }
        }

        public class ValueContainerB
        {
            public double Val { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void Test1()
        {
            var a = new A() {ValueA = new ValueContainerA() { Value = 22}};
            var b = new B() {ValueB = new ValueContainerB() { Val = a.ValueA.Value}};

            b.ValueB.Val = 1;
            Assert.AreEqual(1, a.ValueA.Value);
        }
    }
}