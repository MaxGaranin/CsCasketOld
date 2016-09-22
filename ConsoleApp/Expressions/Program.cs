using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp.Expressions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.WorkEstimates();
        }

        public void WorkEstimates()
        {
            // estimate.Name.Contains("My");
            var estimate = new Estimate {Name = "MyEstimate"};
            
            var itemEstimate = Expression.Parameter(typeof(Estimate), "item");
            var prop = Expression.Property(itemEstimate, "Name");

            MethodInfo method = typeof(string).GetMethod("Contains");
            List<Type> args = new List<Type>(method.GetParameters().Select(p => p.ParameterType));
            var delegateType = Expression.GetFuncType(args.ToArray());
            // dynamic func = Delegate.CreateDelegate(delegateType, prop, method);

            //var whereExpr = Expression.Lambda<Func<Estimate, bool>>(delegateType, new[] {itemEstimate});

        }

        private static Delegate GetDelegate<T>(object target, string methodName)
        {
            MethodInfo method = typeof(T).GetMethod(methodName);
            List<Type> args = new List<Type>(
                method.GetParameters().Select(p => p.ParameterType));
            Type delegateType;
            if (method.ReturnType == typeof(void))
            {
                delegateType = Expression.GetActionType(args.ToArray());
            }
            else
            {
                args.Add(method.ReturnType);
                delegateType = Expression.GetFuncType(args.ToArray());
            }
            return Delegate.CreateDelegate(delegateType, target, method);
        }

        public void WorkWithParameters()
        {
            var pDict = new ParameterDict { Id = 2, Name = "Давление" };
            var p = new Parameter
            {
                Id = 1,
                ParameterDict = pDict,
                Value = 10
            };

            // p.ParameterDict.Id == 1 && p.Value > 5

            var itemParameter = Expression.Parameter(typeof(Parameter), "item");
            var whereExpression = Expression.Lambda<Func<Parameter, bool>>
                (
                    Expression.AndAlso(
                        Expression.Equal(
                            Expression.Property(Expression.Property(itemParameter, "ParameterDict"), "Id"),
                            Expression.Constant(1)
                            ),
                        Expression.GreaterThan(
                            Expression.Property(itemParameter, "Value"),
                            Expression.Constant(5.0, typeof(double))
                            )
                        ),
                    new[] { itemParameter }
                ).Compile();

            Console.WriteLine(whereExpression(p));
            Console.WriteLine(whereExpression.ToString());
            Console.ReadKey();
        }

        private class Estimate
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class Parameter
        {
            public int Id { get; set; }
            public ParameterDict ParameterDict { get; set; }
            public double Value { get; set; }
        }

        private class ParameterDict
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}