using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.HackerRanks.Stacks.MaximumElement
{
    public class Program
    {
        private const int MinValue = 0;

        private static void Work(TextWriter writer, TextReader reader)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrEmpty(line)) throw new ArgumentException();

            var n = int.Parse(line);

            var stack = new Stack<int>();
            var maxStack = new Stack<int>();
            maxStack.Push(MinValue);

            for (int i = 0; i < n; i++)
            {
                line = reader.ReadLine();
                if (string.IsNullOrEmpty(line)) throw new ArgumentException();

                var tokens = line.Split(' ');
                if (tokens[0] == "1")
                {
                    var k = int.Parse(tokens[1]);
                    stack.Push(k);

                    if (k >= maxStack.Peek()) maxStack.Push(k);
                }
                else if (tokens[0] == "2")
                {
                    var k = stack.Pop();
                    if (k == maxStack.Peek()) maxStack.Pop();
                }
                else if (tokens[0] == "3")
                {
                    var maxValue = maxStack.Peek();
                    writer.WriteLine(maxValue);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public static void Main()
        {
            const bool useFileReader = true;
            var textReader = useFileReader
                ? new StreamReader(@".\..\..\HackerRanks\Stacks\MaximumElement\input06.txt")
                : Console.In;

            const bool useFileWriter = true;
            var textWriter = useFileWriter
                ? new StreamWriter(@"d:\work835\Temp\result.txt")
                : Console.Out;

            Work(textWriter, textReader);

            textReader.Close();
            textWriter.Close();

            Console.ReadKey();
        }
    }
}