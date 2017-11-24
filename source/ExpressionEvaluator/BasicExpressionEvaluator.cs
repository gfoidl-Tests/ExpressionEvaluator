using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionEvaluator
{
    public class BasicExpressionEvaluator
    {
        private static Dictionary<char, Func<Expression, Expression, Expression>> _operations;
        //---------------------------------------------------------------------
        static BasicExpressionEvaluator()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            _operations = new Dictionary<char, Func<Expression, Expression, Expression>>
            {
                ['+'] = Expression.Add,
                ['-'] = Expression.Subtract,
                ['*'] = Expression.Multiply,
                ['/'] = Expression.Divide
            };
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression)
        {
            foreach (var operation in _operations)
            {
                if (expression.Contains(operation.Key))
                {
                    var parts      = expression.Split(operation.Key);
                    Expression res = Expression.Constant(this.Evaluate(parts[0]));
                    res            = parts.Skip(1).Aggregate(res, (current, next) => operation.Value(current, Expression.Constant(this.Evaluate(next))));

                    return CompileExpression(res);
                }
            }

            double value = 0;
            double.TryParse(expression, out value);

            return value;
        }
        //---------------------------------------------------------------------
        private static double CompileExpression(Expression expression)
        {
            Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(expression);
            Func<double> compiled           = lambda.Compile();
            return compiled();
        }
    }
}