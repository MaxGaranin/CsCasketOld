using System;

namespace ConsoleApp.OOP.Virtual02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var checker = new CompatibilityChecker();

            // Ошибка компиляции
            // Console.WriteLine(checker.Foo(new C()));

            Console.ReadKey();
        }
    }

    public interface IA
    {
        void Print();
    }

    public interface IB
    {
        void Print();
    }

    public class C : IA, IB
    {
        void IA.Print()
        {
            Console.WriteLine("A");
        }

        void IB.Print()
        {
            Console.WriteLine("B");
        }
    }

    public class CompatibilityChecker
    {
        public string Foo(IA a)
        {
            return "A";
        }

        public string Foo(IB b)
        {
            return "B";
        }
    }
}