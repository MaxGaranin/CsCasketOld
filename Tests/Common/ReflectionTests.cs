using System;
using System.Reflection;
using NUnit.Framework;

namespace Tests45.Common
{
    [TestFixture]
    public class ReflectionTests
    {
        [Test]
        public void TestGetProperty()
        {
            var propInfo = typeof (MyClass).GetProperty("MyProperty");
            Assert.IsNotNull(propInfo);

            var propInfo2 = typeof(MyClass).GetProperty("MyProperty.Name");
            Assert.IsNull(propInfo2);

            var myClass = new MyClass
            {
                MyProperty = new MyProperty
                {
                    Name = "Name"
                }
            };

            var value = myClass.GetPropValue("MyProperty.Name");
            Assert.AreEqual("Name", value);

            propInfo = typeof(MyClass).GetProp("MyProperty.Name");
            Assert.IsNotNull(propInfo);
        }
    }

    public class MyClass
    {
        public MyProperty MyProperty { get; set; }
    }

    public class MyProperty
    {
        public string Name { get; set; }
    }

    public static class ReflectionUtils
    {
        public static PropertyInfo GetProp(this Type type, string name)
        {
            PropertyInfo info = null;

            foreach (var part in name.Split('.'))
            {
                if (type == null) return null;
                
                info = type.GetProperty(part);
                if (info == null) return null;

                type = info.PropertyType;
            }

            return info;
        }

        public static object GetPropValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) return null;

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) return null;

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            object retval = GetPropValue(obj, name);
            if (retval == null) return default(T);
            
            // throws InvalidCastException if types are incompatible
            return (T) retval;
        }
    }
}