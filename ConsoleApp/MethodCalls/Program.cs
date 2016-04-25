using System;
using System.Diagnostics;

namespace ConsoleApp.MethodCalls
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            MethodCallsHelper.TestDirectCall(DateTime.Now);
            Console.WriteLine("Direct call: {0}", sw.Elapsed);
            sw.Stop();

            sw.Restart();
            MethodCallsHelper.TestReflectionCall(DateTime.Now);
            Console.WriteLine("Reflection call: {0}", sw.Elapsed);
            sw.Stop();

            sw.Restart();
            MethodCallsHelper.TestDelegateCall(DateTime.Now);
            Console.WriteLine("Delegate call: {0}", sw.Elapsed);
            sw.Stop();

            sw.Restart();
            MethodCallsHelper.TestDynamicCall(DateTime.Now);
            Console.WriteLine("Dynamic call: {0}", sw.Elapsed);
            sw.Stop();

            sw.Restart();
            MethodCallsHelper.TestExpressionCall(DateTime.Now);
            Console.WriteLine("Expression call: {0}", sw.Elapsed);
            sw.Stop();

            Console.ReadLine();
        }
    }
}