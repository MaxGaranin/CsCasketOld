using System;
using NUnit.Framework;

namespace Tests45.Common
{
    /// <summary>
    /// <remarks>Разбор статьи The art of Generics https://habrahabr.ru/post/201996/</remarks>
    /// </summary>
    [TestFixture]
    public class GenericCovarianceTests
    {
        [Test]
        public void Test()
        {
            var reader = new DefaultReader<short>();

            var arr = new short[] {128, 256};

            if (reader.Supports<short>())
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine("Reader result: {0}", reader.Read(arr, i));
                }
            }
        }
    }

    public interface IReader<T>
    {
        T Read(T[] arr, int index);

        bool Supports<TType>();
    }

    public class SignedIntegersReader : IReader<int>, IReader<short>, IReader<long>
    {
        public int Read(int[] arr, int index)
        {
            return arr[index];
        }

        public short Read(short[] arr, int index)
        {
            return arr[index];
        }

        public long Read(long[] arr, int index)
        {
            return arr[index];
        }

        public bool Supports<TType>()
        {
            return this as IReader<TType> != null;
        }
    }

    public class DefaultReader<T> : IReader<T>
    {
        private IReader<T> _reader = new SignedIntegersReader() as IReader<T>;

        public T Read(T[] arr, int index)
        {
            if (_reader != null)
            {
                return _reader.Read(arr, index);
            }
            return default(T);
        }

        public bool Supports<TType>()
        {
            return _reader.Supports<TType>();
        }
    }

    public static class ReaderExtensions
    {
        public static T Read<TReader, T>(this TReader reader, T[] arr, int index) where TReader : IReader<T>
        {
            return reader.Read(arr, index);
        }
    }


}