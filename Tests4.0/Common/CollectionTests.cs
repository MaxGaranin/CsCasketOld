using System.Collections.Generic;
using NUnit.Framework;

namespace Test40.Common
{
    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void TestHashSet()
        {
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(1);

            Assert.That(set.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestDoubles()
        {
            var valuesList = new List<double> { 1, 2, 3, 4, 5 };
            var valuesArray = valuesList.ToArray();

            valuesArray[0] = 44;

            Assert.That(valuesList[0], Is.EqualTo(1));
        }

        [Test]
        public void TestStrings()
        {
            var valuesList = new List<string> { "1", "2", "3", "4", "5" };
            var valuesArray = valuesList.ToArray();

            valuesArray[0] = "44";

            Assert.That(valuesList[0], Is.EqualTo("1"));
        }

        [Test]
        public void TestObjects()
        {
            var valuesList = new List<SomeObject>
            {
                new SomeObject { Value = 1},
                new SomeObject { Value = 2},
            };
            var valuesArray = valuesList.ToArray();

            valuesArray[0].Value = 44;

            Assert.That(valuesList[0].Value, Is.EqualTo(44));
        }

        private class SomeObject
        {
            public int Value { get; set; }
        }
    }
}