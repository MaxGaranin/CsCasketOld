using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class ShallowCopyTests
    {
        public class A
        {
            public string Str { get; set; }
            public B B { get; set; }

            public A ShallowCopy()
            {
                return (A) this.MemberwiseClone();
            }
        }

        public class B
        {
            public string AnotherStr { get; set; }
        }
         
        [Test]
        public void Shallow_copy_does_one_layer_deep_copy()
        {
            A a = new A() { Str = "aaa", B = new B() { AnotherStr = "ququ" } };
            A copyA = a.ShallowCopy();
            Assert.AreEqual(a.Str, copyA.Str);

            a.Str = "bbb";
            Assert.AreNotEqual(a.Str, copyA.Str);

            a.B.AnotherStr = "wewe";
            Assert.AreEqual(a.B.AnotherStr, copyA.B.AnotherStr);
        }
    }
}