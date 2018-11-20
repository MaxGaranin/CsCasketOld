using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp.HackerRanks.ArrayManipulation
{
    public class Program
    {
        private class Part
        {
            public Part(int start, int end, long value)
            {
                Start = start;
                End = end;
                Value = value;
            }

            public int Start { get; private set; }
            public int End { get; private set; }
            public long Value { get; private set; }
        }

        private static long ArrayManipulation(int n, int[][] queries) {

            var parts = new List<Part>();

            var query = queries[0];
            var a = query[0];
            var b = query[1];
            var k = query[2];

            var initPart = new Part(a, b, k);
            parts.Add(initPart);

            for (int i = 1; i < queries.Length; i++)
            {
                query = queries[i];
                a = query[0];
                b = query[1];
                k = query[2];

                var tempParts = new List<Part>();

                foreach (var part in parts)
                {
                    var minEnd = Math.Min(b, part.End);
                    var maxStart = Math.Max(a, part.Start);
                    if (minEnd - maxStart > 0)
                    {
                        tempParts.Add(new Part(a, b, k + part.Value));

//                        if (a > part.Start)
//                        {
//                            tempParts.Add(new Part(part.Start, a, k));
//                        }
//                        else
//                        {
//                            tempParts.Add(new Part(a, part.Start, k));
//                        }
//
//                        if (b > part.End)
//                        {
//                            tempParts.Add(new Part(part.End, b, k));
//                        }
//                        else
//                        {
//                            tempParts.Add(new Part(b, part.End, k));
//                        }
                    }
                    else
                    {
                        // нет пересечения
//                        tempParts.Add(new Part(a, b, k));
                    }
                }

                parts.AddRange(tempParts);
            }

            var max = parts.Max(p => p.Value);

            return max;
        }

        public static void Main()
        {
            var sr = new StreamReader(@".\..\..\HackerRanks\ArrayManipulation\input07.txt");
            var s = sr.ReadLine();
            var tokens = s.Split(' ');

            var n = Convert.ToInt32(tokens[0]);
            var m = Convert.ToInt32(tokens[1]);

            var queries = new int[m][];

            for (int i = 0; i < m; i++)
            {
                var line = sr.ReadLine();
                if (string.IsNullOrEmpty(line)) break;

                queries[i] = Array.ConvertAll(line.Split(' '), Convert.ToInt32);
            }
            sr.Close();

            long result = ArrayManipulation(n, queries);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static void Main2()
        {
            var n = 5;
            var m = 3;
            var s = "1 2 100\n" +
                    "2 5 100\n" +
                    "3 4 100\n";

            var queries = new int[m][];

            var strQueries = s.Split('\n').Where(x => !string.IsNullOrEmpty(x));
            var i = 0;
            foreach (var strQuery in strQueries)
            {
                queries[i] = Array.ConvertAll(strQuery.Split(' '), Convert.ToInt32);
                i++;
            }

            var result = ArrayManipulation(n, queries);

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}