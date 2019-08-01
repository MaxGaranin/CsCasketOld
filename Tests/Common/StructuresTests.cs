using System;
using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class StructuresTests
    {
        [Test]
        public void Test()
        {
            double d = 0;
            Assert.AreEqual(0, d);

            MyStruct myStruct = new MyStruct();
            Assert.AreEqual(null, myStruct.Name);
            Assert.AreEqual(0d, myStruct.Value, 0.1);
            Assert.AreEqual(new DateTime(), myStruct.Date);

            MyStruct a;

            DerivedStruct ds;
            DerivedStruct ds2 = new DerivedStruct();
            DerivedStruct ds3 = new DerivedStruct(1, 2);
            ds3.SomeClass.Values[100000 - 1] = 6.7;
        }
    }

    internal struct MyStruct
    {
        public MyStruct(string name, double value, DateTime date)
        {
            Name = name;    // Попробуй закомментировать эту строчку (все поля д.б. инициализированы)
            Value = value;
            Date = date;
        }

        public string Name; // На месте нельзя инциализировать
        public double Value;
        public DateTime Date;
    }

    internal interface IStruct
    {
        string GetName();
    }

    internal struct DerivedStruct : IStruct
    {
        private static DerivedStruct _nestedStruct;
        private int _b;

        static DerivedStruct()
        {
            _nestedStruct = new DerivedStruct();
        }

        public DerivedStruct(int a, int b)
        {
            A = a;
            _b = b;
            SomeClass = new SomeClass();
        }

        public int A { get; }
        public SomeClass SomeClass { get; set; }

        public string GetName()
        {
            var str = new MyStruct();
            return str.Name;
        }

        public static DateTime Now()
        {
            return DateTime.Now;
        }
    }

    internal class SomeClass
    {
        public SomeClass()
        {
            Values = new double[100000];
        }

        public double[] Values { get; set; }
    }
}