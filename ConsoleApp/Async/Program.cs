using System;

namespace ConsoleApp.Async
{
    internal class Program
    {
        private static void Main()
        {
            var worker = new CancelTaskWorker();
            worker.StartWithTasks();

//            var worker2 = new CancelAsyncWorker();
//            worker2.Work();
            
            Console.ReadLine();
        }
    }
}