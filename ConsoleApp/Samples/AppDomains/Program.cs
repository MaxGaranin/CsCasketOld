using System;
using System.Reflection;

namespace ConsoleApp.Samples.AppDomains
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factorialDomain = AppDomain.CreateDomain("Factorial Domain");
            factorialDomain.AssemblyLoad += FactorialDomain_OnAssemblyLoad;
            factorialDomain.DomainUnload += FactorialDomain_OnDomainUnload;

            var assembly = factorialDomain.Load("MyUtils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var type = assembly.GetType("MyUtils.NumberUtils");
            var mi = type.GetMethod("Factorial", BindingFlags.Static | BindingFlags.Public);
            var n = 10;
            var result = (int) mi.Invoke(null, new object[] {n});
            Console.WriteLine($"Факториал {n} равен {result}");

            var type2 = assembly.GetType("MyUtils.DataAllocator");
            var instance2 = Activator.CreateInstance(type2);
            var mi2 = type2.GetMethod("InitArray", BindingFlags.Instance | BindingFlags.Public);
            var k = 10000000L;
            mi2.Invoke(instance2, new object[] {k});

            AppDomain.Unload(factorialDomain);

            Console.ReadKey();
        }

        private static void FactorialDomain_OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine($"Сборка {args.LoadedAssembly.FullName} загружена");
        }

        private static void FactorialDomain_OnDomainUnload(object sender, EventArgs e)
        {
            Console.WriteLine("Домен Factorial Domain выгружен из процесса");
        }
    }
}