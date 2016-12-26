using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfSamples40.Utils
{
    public static class ObjectUtils
    {
        public static void Copy(this object source, object destination)
        {
            PropertyInfo[] propListSrc = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            List<PropertyInfo> propListDest =
                new List<PropertyInfo>(destination.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public));
            PropertyInfo propDest;
            foreach (PropertyInfo propSrc in propListSrc)
            {
                if ((propDest = propListDest.Find(
                    new Predicate<PropertyInfo>(
                        delegate(PropertyInfo prop)
                        { return prop.Name.Equals(propSrc.Name, StringComparison.CurrentCultureIgnoreCase); }))) !=
                    null)
                    propDest.SetValue(destination, propSrc.GetValue(source, null), null);
            }
        }

        public static T DeepClone<T>(this T obj)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "obj");
            }

            // Не сериализуем пустой объект, просто возвращаем значение по умолчанию
            if (Object.ReferenceEquals(obj, null))
            {
                return default(T);
            }

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }

}