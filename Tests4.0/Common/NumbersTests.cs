using NUnit.Framework;

namespace Test40.Common
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
    }
}