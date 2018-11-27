using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.HackerRanks.Stacks.BalancedBrackets
{
    public class Program
    {
        private static readonly HashSet<char> OpenBrackets =
            new HashSet<char> {'(', '[', '{'};

        private static readonly HashSet<char> CloseBrackets =
            new HashSet<char> {')', ']', '}'};

        private static readonly Dictionary<char, char> MatchingBrackets =
            new Dictionary<char, char>
            {
                {')', '('},
                {']', '['},
                {'}', '{'},
            };

        private static string IsBalanced(string s)
        {
            var stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                var ch = s[i];

                if (IsOpen(ch))
                {
                    stack.Push(ch);
                }
                else if (IsClose(ch))
                {
                    if (stack.Count == 0) return "No";
                    var openCh = stack.Pop();
                    if (!IsMatch(ch, openCh)) return "No";
                }
            }

            return stack.Count > 0 ? "No" : "Yes";
        }

        private static bool IsOpen(char ch)
        {
            return OpenBrackets.Contains(ch);
        }

        private static bool IsClose(char ch)
        {
            return CloseBrackets.Contains(ch);
        }

        private static bool IsMatch(char closeCh, char openCh)
        {
            var ch = MatchingBrackets[closeCh];
            return ch == openCh;
        }

        public static void Main()
        {
            var sr = new StreamReader(@".\..\..\HackerRanks\Stacks\BalancedBrackets\input17.txt");
            var sw = new StreamWriter(@"d:\work835\Temp\result.txt");

            var n = int.Parse(sr.ReadLine());

            for (int i = 0; i < n; i++)
            {
                var s = sr.ReadLine();
                if (string.IsNullOrEmpty(s)) break;

                var result = IsBalanced(s);                
                sw.WriteLine(result.ToUpper());
            }

            sr.Close();
            sw.Close();

            Console.ReadKey();
        }
    }
}