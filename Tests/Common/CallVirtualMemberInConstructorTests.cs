using System;
using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class CallVirtualMemberInConstructorTests
    {
        [Test]
        public void Test()
        {
            Assert.Throws<NullReferenceException>(() => { var derived = new SomeDerivedClass(); });
        }
    }

    public class SomeBaseClass
    {
        public SomeBaseClass()
        {
            DoWork();
        }

        public virtual void DoWork()
        {
        }
    }

    public class SomeDerivedClass : SomeBaseClass
    {
        private string _myString;

        public SomeDerivedClass() : base()
        {
            _myString = "LaLa";
        }

        public override void DoWork()
        {
            base.DoWork();
            Console.WriteLine(_myString.ToLower());
        }
    }
}