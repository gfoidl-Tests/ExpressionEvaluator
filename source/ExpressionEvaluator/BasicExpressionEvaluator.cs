using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionEvaluator
{
    public class BasicExpressionEvaluator
    {
        static BasicExpressionEvaluator()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression)
        {
            if (expression.Contains("+"))
            {
                var parts = expression.Split('+');

                Expression res = Expression.Constant(0d);
                res = parts.Aggregate(res, (current, next) => Expression.Add(current, Expression.Constant(double.Parse(next))));

                return this.CompileExpression(res);
            }

            if (expression.Contains("-"))
            {
                var parts = expression.Split('-');

                Expression res = Expression.Constant(double.Parse(parts[0]));
                res = parts.Skip(1).Aggregate(res, (current, next) => Expression.Subtract(current, Expression.Constant(double.Parse(next))));

                return this.CompileExpression(res);
            }

            double value = 0;
            double.TryParse(expression, out value);

            return value;
        }
        //---------------------------------------------------------------------
        private double CompileExpression(Expression expression)
        {
            Expression<Func<double>> lambda = Expression.Lambda<Func<double>>(expression);
            Func<double> compiled = lambda.Compile();
            return compiled();
        }
    }
}