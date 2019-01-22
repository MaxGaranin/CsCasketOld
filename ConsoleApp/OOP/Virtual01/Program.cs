using System;

namespace ConsoleApp.OOP.Virtual01
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            MyClassB b = new MyClassB();
            MyClassA a = b;

            // если override Abc() - выведется A
            // если new Abc() или без модификатора - выведется B
            a.Abc();
            
            Console.ReadKey();
        }
    }

    internal class MyClassA
    {
        public MyClassA()
        {
            Console.WriteLine("constructor A");
        }

        public virtual void Abc()
        {
            Console.WriteLine("A");
        }
    }

    internal class MyClassB : MyClassA
    {
        public MyClassB()
        {
            Console.WriteLine("constructor B");
        }

        // Можно попробовать new и override
        public override void Abc()
        {
            Console.WriteLine("B");
        }
    }
}