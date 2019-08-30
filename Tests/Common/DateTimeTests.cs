using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class DateTimeTests
    {
        [Test]
        public void Test()
        {
            var utcDate = DateTime.UtcNow;
            Assert.AreEqual(DateTimeKind.Utc, utcDate.Kind);
            Debug.WriteLine("u: {0:u}", utcDate);
            Debug.WriteLine("G: {0:G}", utcDate);

            var date = DateTime.Now;
            Assert.AreEqual(DateTimeKind.Local, date.Kind);
            Debug.WriteLine("u: {0:u}", date);
            Debug.WriteLine("G: {0:G}", date);

//            TimeSpan ts = date - utcDate;
//            Assert.AreEqual(4, ts.Hours);

            var udt = ToUtc(date);
            var dt = FromUtc(udt);

            dt = FromUtc(utcDate);
            udt = ToUtc(dt);

            var today = DateTime.Today;
            Assert.AreEqual(0d, today.Minute);

            var date2 = new DateTime(2019, 8, 2);
            Assert.AreEqual(DateTimeKind.Unspecified, date2.Kind);
            Assert.AreEqual(0d, date2.Minute);

            DateTime a = DateTime.Now;
            DateTime? b = a;
        }

        private DateTime FromUtc(DateTime val)
        {
            return new DateTime(val.ToUniversalTime().Ticks, DateTimeKind.Local);
        }

        private DateTime ToUtc(DateTime val)
        {
            return new DateTime(val.Ticks, DateTimeKind.Utc);
        }
    }
}