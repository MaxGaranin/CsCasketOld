using System;
using System.ComponentModel;
using NUnit.Framework;

namespace Test40.CallVirtualMember
{
    [TestFixture]
    public class CallVirtualMemberTests
    {
        [Test]
        public void Test()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                var derived = new SomeDerivedClass();
            });
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