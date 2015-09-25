using ConsoleApp.Async;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            var worker = new CancelAsyncWorker();
            worker.Start();
        }
    }
}