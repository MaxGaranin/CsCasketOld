using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleApp.Async.Metanit
{
    public class MutexAppRunOnce
    {
        public static void Main(string[] args)
        {
            // получаем GIUD приложения
            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

            bool isExists;
            var mutexObj = new Mutex(true, guid, out isExists);

            if (isExists)
            {
                Console.WriteLine("Приложение работает");
            }
            else
            {
                Console.WriteLine("Приложение уже было запущено. И сейчас оно будет закрыто.");
                Thread.Sleep(3000);
                return;
            }

            Console.ReadLine();
        }
    }
}