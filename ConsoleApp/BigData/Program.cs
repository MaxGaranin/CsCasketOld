using System;
using System.Runtime.InteropServices;

namespace ConsoleApp.BigData
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Size of GUID: {0}", Marshal.SizeOf(typeof (Guid)));
            Console.WriteLine("Size of int: {0}", sizeof (int));

            int count = 80000000;
            var array = new MyDataItem[count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new MyDataItem();
            }
            GC.Collect();

            var msg = String.Format("Memory: {0} bytes", GC.GetTotalMemory(true).ToString("0,0"));
            Console.WriteLine(msg);
            Console.ReadKey();
        }

        // Занимает намного меньше места
        public struct MyDataItem
        // public class MyDataItem
        {
            public Guid Id { get; set; }
            public int Value1 { get; set; }
            public int Value2 { get; set; }
        }
    }
}