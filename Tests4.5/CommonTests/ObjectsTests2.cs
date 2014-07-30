using System.Collections.Generic;
using NUnit.Framework;
using Omu.ValueInjecter;
using Tests45.CommonTests.Data;

namespace Tests45.CommonTests
{
    [TestFixture]
    public class ObjectsTests2
    {
        [Test]
        public void Test()
        {
            Model model1 = new Model()
                {
                    Name = "Model1",
                    Data = 1
                };

            Well well = new Well()
                {
                    Name = "Well1",
                    Model = model1
                };

            Model model2 = new Model()
                {
                    Name = "Model2",
                    Data = 2
                };

            model1.InjectFrom(model2);
            Assert.AreEqual("Model2", well.Model.Name);
        }

        [Test]
        public void Test2()
        {
            var model1 = new Model()
            {
                Name = "Model1",
                Data = 1
            };

            var group1 = new Group()
                {
                    Name = "Group1",
                    Models = new List<Model>() { model1 }
                };

            var well = new Well()
            {
                Name = "Well1",
                Model = model1
            };

            var model2 = new Model()
            {
                Name = "Model2",
                Data = 2  
            };

            model1 = model2;
            model2 = null;

             Assert.AreEqual("Model2", well.Model.Name);
        }

        [Test]
        public void Test3()
        {
            var group = new Group()
                {
                    Name = "MyGroup",
                    Models = new List<Model>()
                        {
                            new Model() {Name = "Первая"},
                            new Model() {Name = "Вторая"},
                            new Model() {Name = "Третья"}
                        }
                };

            var modelRef = group.Models[1];

            group.Models.RemoveAt(1);
            Assert.AreNotEqual(null, modelRef);
        }

    }

}