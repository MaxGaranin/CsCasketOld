using System.Diagnostics;
using NUnit.Framework;

namespace Test40.Common
{
    [TestFixture]
    public class ThisBaseClassTests
    {
        class Base
        {
            protected string Name;
            protected double Value;

            public Base()
            {
                Value = 45.3;
                Debug.WriteLine("Base empty constructor");
            }

            protected Base(string name)
            {
                Name = name;
                Debug.WriteLine("Base constructor with parameters");
            }

            public string GetCurrentName()
            {
                return Name + Value;
            }
        }

        class Derived : Base
        {
            public Derived() : base()
            {
                Debug.WriteLine("Derived empty constructor");
            }

            public Derived(double value) : this()
            {
                Value = value;
                Debug.WriteLine("Derived constructor with parameters");
            }
        }

        [Test]
        public void Test()
        {
           Base b = new Derived(222);
        } 
    }
}