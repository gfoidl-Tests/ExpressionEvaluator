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

                var sum = parts.Aggregate(Expression.Constant(0d) as Expression,
                    (current, next) => Expression.Add(current, Expression.Constant(double.Parse(next))));

                var lambda = Expression.Lambda<Func<double>>(sum);
                var compiled = lambda.Compile();
                return compiled();
            }

            double value = 0;
            double.TryParse(expression, out value);

            return value;
        }
    }
}