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

                Expression res = Expression.Constant(this.Evaluate(parts[0]));
                res = parts.Skip(1).Aggregate(res, (current, next) => Expression.Add(current, Expression.Constant(this.Evaluate(next))));

                return this.CompileExpression(res);
            }

            if (expression.Contains("-"))
            {
                var parts = expression.Split('-');

                Expression res = Expression.Constant(this.Evaluate(parts[0]));
                res = parts.Skip(1).Aggregate(res, (current, next) => Expression.Subtract(current, Expression.Constant(this.Evaluate(next))));

                return this.CompileExpression(res);
            }

            if (expression.Contains("*"))
            {
                var parts = expression.Split('*');

                Expression res = Expression.Constant(this.Evaluate(parts[0]));
                res = parts.Skip(1).Aggregate(res, (current, next) => Expression.Multiply(current, Expression.Constant(this.Evaluate(next))));

                return this.CompileExpression(res);
            }

            if (expression.Contains("/"))
            {
                var parts = expression.Split('/');

                Expression res = Expression.Constant(this.Evaluate(parts[0]));
                res = parts.Skip(1).Aggregate(res, (current, next) => Expression.Divide(current, Expression.Constant(this.Evaluate(next))));

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