using System;

namespace ConsoleApp.Hardware
{
    internal class Program
    {
        private static void Main()
        {
            var memorySize = HardwareInfoHelper.GetMemorySize();
            var memorySizeInMb = memorySize / (1024 * 1024);
            Console.WriteLine($"Memory size, Mb: {memorySizeInMb} (first function)");

            memorySize = HardwareInfoHelper.GetMemorySizeInKb();
            memorySizeInMb = memorySize / 1024;
            Console.WriteLine($"Memory size, Mb: {memorySizeInMb} (second function)");

            Console.ReadKey();
        }
    }
}