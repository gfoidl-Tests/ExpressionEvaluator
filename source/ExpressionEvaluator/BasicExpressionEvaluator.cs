using System;
using System.Globalization;
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

                Expression sum = Expression.Constant(double.Parse(parts[0]));

                for (int i = 1; i < parts.Length; ++i)
                    sum = Expression.Add(sum, Expression.Constant(double.Parse(parts[i])));

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