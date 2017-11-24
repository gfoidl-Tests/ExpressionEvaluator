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

            var reader   = new StringReader(expression);
            var lexer    = new Lexer(reader);
            var tokens   = lexer.ReadTokens();
            var parser   = new Parser();
            var tree     = parser.Parse(tokens);
            var compiled = Expression.Lambda<Func<double[], double>>(tree.Tree, tree.Parameter).Compile();
            return compiled(arguments);
        }
    }
}