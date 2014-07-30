using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests45.CommonTests
{
    [TestFixture]
    public class CollectionTests
    {
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