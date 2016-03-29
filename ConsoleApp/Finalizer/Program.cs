using System;
using System.Diagnostics;

namespace ConsoleApp.Finalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            MyClassB b = new MyClassB();
            MyClassA a = b;
            a.abc();
            Console.ReadLine();
        }
    }

    class MyClassA
    {
        public MyClassA()
        {
            Console.WriteLine("constructor A");
        }

        public void abc()
        {
            Console.WriteLine("A");
        }
    }

    class MyClassB : MyClassA
    {
        public MyClassB()
        {
            Console.WriteLine("constructor B");
        }

        public void abc()
        {
            Console.WriteLine("B");
        }
    }
}