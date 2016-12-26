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
        public MyStruct(string name, double value)
        {
            Name = name;  // Попробуй закомментировать эту строчку (все поля д.б. инициализированы)
            Value = value;
        }

        public string Name; // На месте нельзя инциализировать

        public double Value;
    }
}