using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ReplaceBackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new MyAsyncAwaitTest();
            test.Run();

            Console.WriteLine("А программа выполняется...");
            Console.ReadLine();
        }
    }
}
