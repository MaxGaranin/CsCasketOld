using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Async.TupikocZhenyaTask
{
    public class Program
    {
        private const int OpCount = 100;

        public static void Main(string[] args)
        {
            var account = new Account();
            account.Deposit(1000);
            Console.WriteLine($"Initial account amount: {account.Amount}");

            var task1 = Task.Run(() =>
            {
                var deposites = 0m;
                var random = RandomProvider.GetThreadRandom();
                for (var i = 0; i < OpCount; i++)
                {
                    decimal value = random.Next(100);
                    account.Deposit(value);
                    deposites += value;
                    Thread.Sleep(1);
                }

                return deposites;
            });

            var task2 = Task.Run(() =>
            {
                var withdrowals = 0m;
                var random = RandomProvider.GetThreadRandom();
                for (var i = 0; i < OpCount; i++)
                {
                    decimal value = random.Next(100);
                    account.Withdraw(value);
                    withdrowals += value;
                    Thread.Sleep(2);
                }

                return withdrowals;
            });

            Task.WaitAll(task1, task2);

            Console.WriteLine("Operations finished.");
            Console.WriteLine($"Account amount: {account.Amount}");
            Console.WriteLine($"--------------------------------");
            Console.WriteLine();

            Console.WriteLine($"Deposits amount: {task1.Result}");
            Console.WriteLine($"Withdrowals amount: {task2.Result}");
            Console.WriteLine($"Deposites - Withdrowals: {task1.Result - task2.Result}");
            Console.WriteLine($"--------------------------------");
            Console.WriteLine();

            foreach (var op in account.Operations)
            {
                Console.WriteLine($"Op: {op.Date.ToLongTimeString()}, {op.OperationType}, {op.Value}");
            }

            Console.ReadKey();
        }
    }

    internal class Account
    {
        private readonly List<AccoutOperation> _operations;
        private readonly object _lock = new object();

        public Account()
        {
            _operations = new List<AccoutOperation>();
        }

        public decimal Amount { get; private set; }

        public ImmutableList<AccoutOperation> Operations
        {
            get
            {
                lock (_lock)
                {
                    return ImmutableList<AccoutOperation>.Empty.AddRange(_operations);
                }
            }
        }

        public void Deposit(decimal value)
        {
            lock (_lock)
            {
                Amount += value;

                _operations.Add(new AccoutOperation
                {
                    Date = DateTime.Now,
                    OperationType = OperationType.Deposit,
                    Value = value
                });
            }
        }

        public void Withdraw(decimal value)
        {
            lock (_lock)
            {
                Amount -= value;

                _operations.Add(new AccoutOperation
                {
                    Date = DateTime.Now,
                    OperationType = OperationType.Withdraw,
                    Value = value
                });
            }
        }
    }

    internal class AccoutOperation
    {
        public DateTime Date { get; set; }

        public OperationType OperationType { get; set; }

        public decimal Value { get; set; }
    }

    internal enum OperationType
    {
        Deposit,
        Withdraw,
    }
}