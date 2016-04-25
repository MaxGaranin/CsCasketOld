using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp.MethodCalls
{
    public class MethodCallsHelper
    {
        public const int IterationCount = 1000000;

        static MethodCallsHelper()
        {
            Container.Register(new SampleGeneric<DateTime>());
        }

        public static void TestDirectCall(DateTime arg)
        {
            var instance = Container.Get<DateTime>();
            long summ = 0;
            for (long i = 0; i < IterationCount; i++)
            {
                summ += instance.Process(arg);
            }
        }

        public static void TestReflectionCall(object arg)
        {
            var instance = Container.Get(arg.GetType());
            var method = instance.GetType().GetMethod("Process");
            long summ = 0;
            for (long i = 0; i < IterationCount; i++)
            {
                summ += (long) method.Invoke(instance, new object[] {arg});
            }
        }

        public static void TestDelegateCall(object arg)
        {
            var instance = Container.Get(arg.GetType());
            var hook = CreateDelegate(instance, instance.GetType().GetMethod("Process"));
            long summ = 0;
            for (long i = 0; i < IterationCount; i++)
            {
                summ += (long) hook.DynamicInvoke(arg);
            }
        }

        private static Delegate CreateDelegate(object target, MethodInfo method)
        {
            var methodParameters = method.GetParameters();
            var arguments = methodParameters.Select(d => Expression.Parameter(d.ParameterType, d.Name)).ToArray();
            var instance = target == null ? null : Expression.Constant(target);
            var methodCall = Expression.Call(instance, method, arguments);
            return Expression.Lambda(methodCall, arguments).Compile();
        }

        public static void TestDynamicCall(dynamic arg)
        {
            var instance = Container.Get(arg.GetType());
            dynamic hook = CreateDelegate(instance, instance.GetType().GetMethod("Process"));
            long summ = 0;
            for (long i = 0; i < IterationCount; i++)
            {
                summ += hook(arg);
            }
        }

        public static void TestExpressionCall(object arg)
        {
            var instance = Container.Get(arg.GetType());
            var hook = CreateExpression(instance.GetType().GetMethod("Process"));
            long summ = 0;
            for (long i = 0; i < IterationCount; i++)
            {
                summ += (long) hook(instance, arg);
            }
            //return
        }

        private delegate object Invoker(object target, params object[] args);

        private static Invoker CreateExpression(MethodInfo method)
        {
            var targetArg = Expression.Parameter(typeof (object));
            var argsArg = Expression.Parameter(typeof (object[]));

            Expression body = Expression.Call(
                method.IsStatic ? null : Expression.Convert(targetArg, method.DeclaringType),
                method,
                method.GetParameters()
                    .Select((p, i) => Expression.Convert(Expression.ArrayIndex(argsArg, Expression.Constant(i)), p.ParameterType)));

            if (body.Type == typeof (void))
                body = Expression.Block(body, Expression.Constant(null));
            else if (body.Type.IsValueType)
                body = Expression.Convert(body, typeof (object));

            return Expression.Lambda<Invoker>(body, targetArg, argsArg).Compile();
        }
    }
}