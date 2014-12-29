using System.Dynamic;
using NUnit.Framework;
using Test40.ClassesUnderTest;

namespace Test40.Dynamics
{
    [TestFixture]
    public class DynamicCollectionTests
    {
        [Test]
        public void Test()
        {
            var collection = new DynamicCollection();
            collection.AddColumn("Id");
            collection.AddColumn("Name");
            collection.AddColumn("Value");

            dynamic expando = new ExpandoObject();
            expando.Id = 1;
            expando.Name = "Vasya";
            expando.Value = 2.5;
            collection.AddNewItem(expando);

            expando = new ExpandoObject();
            expando.Id = 2;
            expando.Name = "Serega";
            expando.Value = 5.666;
            collection.AddNewItem(expando);

            expando = new ExpandoObject();
            expando.Id = 3;
            expando.Name = "Vova";
            expando.Value = -15.1;
            collection.AddNewItem(expando);

            var table = collection.ToDataTable();

            Assert.AreEqual(3, table.Columns.Count);
            Assert.AreEqual(3, table.Rows.Count);
        }
    }
}