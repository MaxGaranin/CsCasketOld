using System;
using System.Collections.Generic;

namespace ConsoleApp.HackerRanks.WarmUp
{
    public class Program
    {
        static string TwoStrings(string s1, string s2)
        {
            var hashSet1 = new HashSet<char>(s1);
            var hashSet2 = new HashSet<char>(s2);
            hashSet1.IntersectWith(hashSet2);

            if (hashSet1.Count > 0)
            {
                return "YES";
            }
            else
            {
                return "NO";
            }
        }

        static void Main(string[] args)
        {
            var s1 = "Hello";
            var s2 = "world";

            var result = TwoStrings(s1, s2);
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}