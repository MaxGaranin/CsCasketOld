namespace ConsoleApp.Async
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