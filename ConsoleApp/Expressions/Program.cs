using System;
using System.Linq.Expressions;

namespace ConsoleApp.Expressions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pDict = new ParameterDict {Id = 1, Name = "Давление"};
            var p = new Parameter
            {
                Id = 1,
                ParameterDict = pDict,
                Value = 10
            };

            // p.ParameterDict.Id == 1 && p.Value > 5

            //            var paramExpr = Expression.Parameter(typeof (Parameter), "item");
            //            var idExpr = Expression.Property(paramExpr, "Id");
            //            var equalExpr = Expression.Equal(idExpr, Expression.Constant(1));
            //            var compiled = Expression.Lambda<Func<Parameter, bool>>(equalExpr).Compile();

            var itemParameter = Expression.Parameter(typeof(Parameter), "item");
            var whereExpression = Expression.Lambda<Func<Parameter, bool>>
                (
                Expression.Equal(
                    Expression.Property(
                        itemParameter,
                        "Id"
                        ),
                    Expression.Constant(1)
                    ),
                new[] { itemParameter }
                );

            Console.WriteLine(whereExpression.Compile()(p));

            Console.WriteLine(whereExpression.ToString());
            Console.ReadKey();
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