using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.HackerRanks.BitManipulations.LonelyInteger
{
    public class Program
    {
        private static int Lonelyinteger(int[] a)
        {
            var hash = new HashSet<int>();
            for (var i = 0; i < a.Length; i++)
            {
                if (hash.Contains(a[i]))
                {
                    hash.Remove(a[i]);
                }
                else
                {
                    hash.Add(a[i]);
                }
            }

            return hash.Single();
        }

        public static void Main()
        {
            var a = new int[] {4, 9, 95, 93, 57, 4, 57, 93, 9};
            var result = Lonelyinteger(a);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}