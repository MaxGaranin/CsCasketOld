using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using Tests45.CommonTests.Data;

namespace Tests45.CommonTests
{
    [TestFixture]
    public class AutoMapperTests
    {
        [Test]
        public void Test2()
        {
            var model1 = new Model() { Name = "Model1"};
            var model2 = new Model() { Name = "Model2"};
            var group = new Group() {Name = "Group", Models = new List<Model>() {model2}};

            Mapper.CreateMap<Model, Model>();
            Mapper.Map(model1, model2);

            Assert.AreEqual("Model1", group.Models[0].Name);
        }

        [Test]
        public void Test()
        {
            var bank = new Bank()
                {
                    Name = "Bank",
                    Groups = new List<Group>()
                        {
                            new Group()
                                {
                                    Name = "GroupA",
                                    Models = new List<Model>()
                                        {
                                            new Model() { Name = "ModelA1"},
                                            new Model() { Name = "ModelA2"},
                                        }
                                },
                            new Group()
                                {
                                    Name = "GroupB",
                                    Models = new List<Model>()
                                        {
                                            new Model() { Name = "ModelB1"},
                                            new Model() { Name = "ModelB2"},
                                            new Model() { Name = "ModelB3"},
                                        }
                                }
                        }
                };

            bank.Groups[0].Models[0].Correlation = new Correlation()
                {
                    Value = 5,
                    StringToInt = new Dictionary<string, int>()
                        {
                            {"a", 1},
                            {"b", 2},
                            {"c", 3},
                        }
                };

            Mapper.CreateMap<Bank, Bank>();
            Mapper.CreateMap<Group, Group>();
            Mapper.CreateMap<Model, Model>();
            Mapper.CreateMap<Correlation, Correlation>();
            var bankCopy = Mapper.Map<Bank>(bank);

            bank.Groups[0].Models[0].Name = "LALALA";
            bank.Groups[0].Models[0].Correlation.StringToInt["b"] = 34;
            Assert.AreEqual("LALALA", bankCopy.Groups[0].Models[0].Name);
            Assert.AreEqual(34, bankCopy.Groups[0].Models[0].Correlation.StringToInt["b"]);
        }
    }
}