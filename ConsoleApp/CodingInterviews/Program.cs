using System;

namespace ConsoleApp.CodingInterviews
{
    public class Program
    {
        private class Test
        {
            public void Print()
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception)
                {
                    Console.Write("9");
                    throw new Exception();
                }
                finally
                {
                    Console.Write("2");
                }
            }
        }

        public static void Main(string[] args)
        {
            var test = new Test();
            try
            {
                test.Print();
            }
            catch (Exception)
            {
                Console.Write("5");
            }
            finally
            {
                Console.Write("4");
            }

            Console.ReadLine();
        }
    }
}