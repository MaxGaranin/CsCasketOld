using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Test40.Common
{
    [TestFixture]
    public class GenericInheritanceTests
    {
        [Test]
        public void Should_inherits_generic_interface()
        {
            ISourseable<string> sourseable = new Sourseable<string>();

            var result = sourseable.GetType().InheritsOrImplements(typeof(ISourseable<>));
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Should_not_inherits_generic_interface()
        {
            IList<string> list = new List<string>();
            
            var result = list.GetType().InheritsOrImplements(typeof(ISourseable<>));
            Assert.AreEqual(false, result);
        }
    }

    public interface ISourseable<T>
    {
        IList<T> Source { get; set; }
    }

    public class Sourseable<T> : ISourseable<T>
    {
        public IList<T> Source { get; set; }
    }

    public static class GenericExtentions
    {
        public static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                                   ? child.GetGenericTypeDefinition()
                                   : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.BaseType != null
                               && currentChild.BaseType.IsGenericType
                                   ? currentChild.BaseType.GetGenericTypeDefinition()
                                   : currentChild.BaseType;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = true;
            if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
                shouldUseGenericType = false;

            if (parent.IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();

            return parent;
        }
    }
}