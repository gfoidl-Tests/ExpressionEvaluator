using System;
using System.Globalization;
using System.IO;
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
        public double Evaluate(string expression, params double[] arguments)
        {
            if (string.IsNullOrWhiteSpace(expression)) return 0;

            var reader         = new StringReader(expression);
            var lexer          = new Lexer(reader);
            var tokens         = lexer.ReadTokens();
            var arrayParameter = Expression.Parameter(typeof(double[]), "args");
            var parser         = new Parser();
            var tree           = parser.Parse(tokens, arrayParameter);
            var compiled       = Expression.Lambda<Func<double[], double>>(tree, arrayParameter).Compile();
            return compiled(arguments);
        }
    }
}