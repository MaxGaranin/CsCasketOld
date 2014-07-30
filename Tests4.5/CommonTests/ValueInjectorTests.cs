using System.Collections.Generic;
using NUnit.Framework;
using Omu.ValueInjecter;
using Tests45.CommonTests.Data;
using Tests45.Utils;

namespace Tests45.CommonTests
{
    [TestFixture]
    public class ValueInjectorTests
    {
        [Test]
         public void Test1()
        {
            var model = new Model()
                {
                    Name = "MyModel",
                    Correlation = new Correlation()
                        {
                            Value = 5,
                            StringToInt = new Dictionary<string, int>()
                                {
                                    {"a", 1},
                                    {"b", 2},
                                    {"c", 3},
                                }
                        }
                };

            var well = new Well()
                {
                    Model = model
                };

            var otherModel = new Model();
            otherModel.InjectFrom(model);
            otherModel.Correlation.Value = 28;
            Assert.AreEqual(28, model.Correlation.Value);

            var modelCopy = new Model();
            modelCopy.InjectFrom<CloneInjection>(model);

            modelCopy.Correlation.Value = 11;

            // Dictionary does not clone
            // modelCopy.Correlation.StringToInt["b"] = 10;

            Assert.AreNotEqual(11, model.Correlation.Value);

            model.InjectFrom(modelCopy);
            Assert.AreEqual(11, model.Correlation.Value);
            Assert.AreEqual(11, well.Model.Correlation.Value);
            // Assert.AreEqual(10, well.Model.Correlation.StringToInt["b"]);
        }
    }
}