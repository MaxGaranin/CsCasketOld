using System;
using System.Linq.Expressions;

namespace ConsoleApp.Expressions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pDict = new ParameterDict {Id = 2, Name = "Давление"};
            var p = new Parameter
            {
                Id = 1,
                ParameterDict = pDict,
                Value = 10
            };

            // p.ParameterDict.Id == 1 && p.Value > 5

            var itemParameter = Expression.Parameter(typeof (Parameter), "item");
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
                    new[] {itemParameter}
                ).Compile();

            Console.WriteLine(whereExpression(p));
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