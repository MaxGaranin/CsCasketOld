using System;
using System.ComponentModel;
using System.Diagnostics;
using NUnit.Framework;

namespace Test40.Speed.PropertiesVsFields
{
    [TestFixture]
    public class PropertiesVsFieldsTests
    {
        [Test]
        public void Test()
        {
            var time1 = new TestTime1();
            var time2 = new TestTime2();
            var watch1 = new Stopwatch();
            var watch2 = new Stopwatch();

            watch2.Start();
            for (var i = 0; i < 10000000; i++)
            {
                time2.Id = i;
                i = time2.Id;
            }
            watch2.Stop();

            watch1.Start();
            for (var i = 0; i < 10000000; i++)
            {
                time1.Id = i;
                i = time1.Id;
            }
            watch1.Stop();

            Console.WriteLine("Time for 1 : {0}", watch1.ElapsedMilliseconds);
            Console.WriteLine("Time for 2 : {0}", watch2.ElapsedMilliseconds);
        }
    }

    internal class TestTime1
    {
        public int Id = 0;
    }

    internal class TestTime2
    {
        [DefaultValue(0)]
        public int Id { get; set; }
    }
}