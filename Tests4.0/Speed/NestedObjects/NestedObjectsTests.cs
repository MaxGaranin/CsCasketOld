using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Test40.Speed.NestedObjects
{
    [TestFixture]
    public class NestedObjectsTests
    {
        [Test]
        public void Test()
        {
            var count = 10E+7;

            // Simple
            var watch = Stopwatch.StartNew();

            var simpleObject = new SimpleObject();
            for (int i = 0; i < count; i++)
            {
                simpleObject.Value = i + 2*8;
            }

            watch.Stop();
            Console.WriteLine("Simple object: {0}", watch.ElapsedMilliseconds);

            // Complex
            watch.Reset();
            watch.Start();

            var complexObject = new ComplexObject();
            for (int i = 0; i < count; i++)
            {
                complexObject.A1.Value = i + 2*8;
            }

            watch.Stop();
            Console.WriteLine("Complex object: {0}", watch.ElapsedMilliseconds);
        }
    }
}