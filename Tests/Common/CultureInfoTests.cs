using System;
using System.Threading;
using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class CultureInfoTests
    {
        [Test]
        public void Test()
        {
            var culture = Thread.CurrentThread.CurrentCulture;    // Формат из панели управления
            var uiCulture = Thread.CurrentThread.CurrentUICulture;

            Console.WriteLine("Culture: {0}", culture);
            Console.WriteLine("UI Culture: {0}", uiCulture);
        }
    }
}