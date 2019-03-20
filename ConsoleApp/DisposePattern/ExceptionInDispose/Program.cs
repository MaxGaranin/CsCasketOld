using System;

namespace ConsoleApp.DisposePattern.ExceptionInDispose
{
    public class Disposable : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Освобождение управляемых ресурсов
                Console.WriteLine("Disposable.Dispose");
                throw new OperationCanceledException("Вызов исключения в Dispose");
            }
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }

    // Проверка вызова Exception в методе Dispose
    public class Program
    {
        public static void Main()
        {
            // Используем using
            try
            {
                using (var disposable = new Disposable())
                {
                    Console.WriteLine("Working...");
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }

            // Используем финализатор (от этого блока программа упадет)
//            try
//            {
//                var disposable2 = new Disposable();
//            }
//            catch (OperationCanceledException e)
//            {
//                Console.WriteLine(e.Message);                
//            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}