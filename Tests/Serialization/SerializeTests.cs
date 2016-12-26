using System.IO;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Tests45.Serialization
{
    [TestFixture]
    public class SerializeTests
    {
        #region Auxiliary classes

        [DataContract]
        public class A
        {
            [DataMember]
            public B Bref { get; set; }

            //        [DataMember]
            //        public C Cref { get; set; }
        }

        [DataContract]
        public class B
        {
            [DataMember]
            public string Bfield { get; set; }
        }

        //    [DataContract]
        //    public class C
        //    {
        //        [DataMember]
        //        public int Cfield { get; set; }
        //    } 

        #endregion

        [Test]
        public void Serialize_comlpex_object_to_file()
        {
            var a = new A()
            {
                Bref = new B() {Bfield = "B"},
//                Cref = new C() {Cfield = 123}
            };

            var serializer = new DataContractSerializer(typeof(A));
            using (var stream = new FileStream(@"D:\1.xml", FileMode.Create))
            {
                serializer.WriteObject(stream, a);
            }
        }

        [Test]
        public void Deserialize_complex_object_from_file_after_changing_structure()
        {
            var serializer = new DataContractSerializer(typeof(A));
            using (var stream = new FileStream(@"D:\1.xml", FileMode.Open))
            {
                A a = (A) serializer.ReadObject(stream);

                Assert.AreEqual(a.Bref.Bfield, "B");
            }
        }
    }
}