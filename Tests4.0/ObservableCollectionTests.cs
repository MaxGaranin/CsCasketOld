using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Tests.Common;
using Tests.CommonTests.Data;

namespace Tests.CommonTests
{
    [TestFixture]
    public class ObservableCollectionTests
    {
        [Test]
        public void Test1()
        {
            var list = new List<string> {"a", "b", "c"};
            var obsCol = new ObservableCollection<string>(list);

            list[1] = "QUQU";

            Assert.AreNotEqual("QUQU", obsCol[1]);
        }

        [Test]
        public void Test2()
        {
           
        }

    }
}