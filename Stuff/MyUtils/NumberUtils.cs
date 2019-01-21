using System;

namespace MyUtils
{
    public class NumberUtils
    {
        public static int Factorial(int n)
        {
            if (n < 0) throw new ArgumentException("Число должно быть неотрицательным!");

            if (n == 0) return 1;

            var result = 1;
            for (var i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }

    public class DataAllocator
    {
        public MyDataItem[] MyArray { get; set; }

        public void InitArray(long size)
        {
            MyArray = new MyDataItem[size];
            for (int i = 0; i < size; i++)
            {
                MyArray[i] = new MyDataItem { Value1 = i};
            }
        }
    }

    public class MyDataItem
    {
        public Guid Id { get; set; }
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }
}