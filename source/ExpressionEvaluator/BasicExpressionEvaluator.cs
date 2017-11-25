using System;
using System.Globalization;
using System.IO;
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
        public double Evaluate(string expression, params double[] arguments)
        {
            if (string.IsNullOrWhiteSpace(expression)) return 0;

            var reader         = new StringReader(expression);
            var lexer          = new Lexer(reader);
            var tokens         = lexer.ReadTokens();
            var arrayParameter = Expression.Parameter(typeof(double[]), "args");
            var parser         = new Parser();
            var result         = parser.Parse(tokens, arrayParameter);

            this.CheckParameterCount(result, arguments);
#if DEBUG
            Console.WriteLine("\nExpression Tree");
            Console.WriteLine(result.Tree);
            Console.WriteLine($"No of params: {result.Parameters.Count}");
#endif
            var compiled = Expression.Lambda<Func<double[], double>>(result.Tree, arrayParameter).Compile();
            return compiled(arguments);
        }
        //---------------------------------------------------------------------
        private void CheckParameterCount(ParsingResult result, double[] arguments)
        {
            if (result.Parameters.Count == arguments.Length) return;

            var paramsWithouArgs = result
                .Parameters
                .Skip(arguments.Length);

            string msg =
                $"Expression contains {result.Parameters.Count} parameters, only {arguments.Length} are given.\n" +
                $"Parameters without args are: {string.Join(", ", paramsWithouArgs)}";

            throw new ArgumentException(msg, nameof(arguments));
        }
    }
}