using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Test40.Common
{
    [TestFixture]
    public class LinqTests
    {
        [Test]
        public void Test()
        {
            IEnumerable<string> collection = new List<string>();
            Assert.IsTrue(collection.All(string.IsNullOrEmpty));
            Assert.IsFalse(collection.Any());
        } 
    }
}