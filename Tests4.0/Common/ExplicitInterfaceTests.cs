using NUnit.Framework;

namespace Test40.Common
{
    [TestFixture]
    public class ExplicitInterfaceTests
    {
        [Test]
        public void Test()
        {
            var dbClass = new DbClass();
            (dbClass as IAdapter).ObjectClass = new ObjectClass();
        }
    }

    public class ObjectClass
    {

    }

    public interface IAdapter
    {
        ObjectClass ObjectClass { get; set; }
    }

    public class DbClass : IAdapter
    {
        ObjectClass IAdapter.ObjectClass { get; set; }
    }
}