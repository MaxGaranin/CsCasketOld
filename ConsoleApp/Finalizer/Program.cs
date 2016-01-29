using System;
using System.Diagnostics;

namespace ConsoleApp.Finalizer
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Start Application");

            MySingleton.Instanse.Run();

            Console.ReadLine();
            Console.WriteLine("End Application");
        }
    }

    public class MySingleton
    {
        private static MySingleton _instanse;

        private MySingleton()
        {
            Console.WriteLine("MySingleton Constructor");
        }

        public static MySingleton Instanse
        {
            get { return _instanse ?? (_instanse = new MySingleton()); }
        }

        public void Run()
        {
            Console.WriteLine("MySingleton Run");
        }

        ~MySingleton()
        {
            Console.WriteLine("MySingleton Finalizer");
        }
    }
}