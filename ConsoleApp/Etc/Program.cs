using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.Etc
{
    public class Program
    {
        public static void Main()
        {
            long n = 7329864172865285;
            n = 732;

            var result = Sum(n);
            Console.WriteLine(result);

            result = Sum2(n);
            Console.WriteLine(result);

            Console.ReadKey();
        }

        private static long Sum(long n)
        {
            long result = 0;

            var s = n.ToString();

            var reminder = n % 10;
            result += reminder;

            for (int i = 2; i < s.Length; i++)
            {
                reminder = (n - Pow(10, i)) % 10;
                result += reminder;
            }

            return result;
        }

        private static long Sum2(long n)
        {
            long result = 0;

            var s = n.ToString();

            foreach (var ch in s)
            {
                var k = int.Parse(ch.ToString());
                result += k;
            }

            return result;
        }

        private static long Pow(int a, int n)
        {
            if (n == 0) return 1L;

            long result = a;

            for (long i = 2; i <= n; i++)
            {
                result *= a;
            }

            return result;
        }
    }
}