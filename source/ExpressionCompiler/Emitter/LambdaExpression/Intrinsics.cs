using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionCompiler.Emitter.LambdaExpression
{
    internal static class Intrinsics
    {
        public static Expression Sin(Expression argument) => Core(argument, typeof(Math).GetMethod(nameof(Math.Sin)));
        public static Expression Cos(Expression argument) => Core(argument, typeof(Math).GetMethod(nameof(Math.Cos)));
        public static Expression Tan(Expression argument) => Core(argument, typeof(Math).GetMethod(nameof(Math.Tan)));
        public static Expression Log(Expression argument) => Core(argument, typeof(Math).GetMethod(nameof(Math.Log), new Type[] { typeof(double) }));
        //---------------------------------------------------------------------
        private static Expression Core(Expression argument, MethodInfo method)
        {
            Func<Expression, Expression> factory = e => Expression.Call(method, e);

            return factory(argument);
        }
    }
}