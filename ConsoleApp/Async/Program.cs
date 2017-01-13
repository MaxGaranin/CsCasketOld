using System;

namespace ConsoleApp.Async
{
    internal class Program
    {
        private static void Main()
        {
            var worker = new CancelAsyncWorker();
            worker.StartWithTasks();

            Console.ReadLine();
        }
    }
}