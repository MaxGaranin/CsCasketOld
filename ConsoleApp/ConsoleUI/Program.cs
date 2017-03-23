using System;
using System.Threading;

namespace ConsoleApp.ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i <= 100; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(10, 10);

                Console.WriteLine("Прогресс выполнения {0}", i);                
                Thread.Sleep(100);
            }
            
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}