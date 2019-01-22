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
            Double d = 0;
            Assert.AreEqual(0, d);

            MyStruct myStruct = new MyStruct();
            Assert.AreEqual(string.Empty, myStruct.Name);

            MyStruct a;
        }
    }

    internal struct MyStruct
    {
        public MyStruct(string name, double value, string str)
        {
            Name = name;    // Попробуй закомментировать эту строчку (все поля д.б. инициализированы)
            Value = value;
            Str = str;
        }

        public string Name; // На месте нельзя инциализировать
        public double Value;
        public string Str;
    }

    internal interface IStruct
    {
        string GetName();
    }

    internal struct DerivedStruct : IStruct
    {
        private static DerivedStruct nestedStruct;
        private int _b;

        static DerivedStruct()
        {
            nestedStruct = new DerivedStruct();
        }

        public DerivedStruct(int a, int b)
        {
            A = a;
            _b = b;
        }

        public int A { get; }

        public string GetName()
        {
            var str = new MyStruct();
            return str.Str;
        }

        public static DateTime Now()
        {
            return DateTime.Now;
        }
    }
}