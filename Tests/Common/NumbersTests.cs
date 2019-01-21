using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class NumbersTests
    {
        [Test]
        public void TestEps()
        {
            var eps = 1E6;
            Assert.AreEqual(1000000, eps);

            eps = 1E-6;
            Assert.AreEqual(0.000001, eps);
        }

        [Test]
        public void TestObjectEquals()
        {
            object obj1 = 1;
            object obj2 = 1;

            Assert.False(obj1 == obj2);
            Assert.True(obj1.Equals(obj2));
        }
    }
}