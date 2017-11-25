using System;
using System.Globalization;
using System.Linq;

namespace ExpressionEvaluator
{
    public abstract class ExpressionEvaluator
    {
        static ExpressionEvaluator()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression, params double[] arguments)
        {
            ParsingResult result = this.Parse(expression);

            if (result == null) return double.NaN;
#if DEBUG
            Console.WriteLine("\nExpression Tree");
            Console.WriteLine(result.Tree);
            Console.WriteLine($"No of params: {result.Parameters.Count}");
#endif
            this.CheckParameterCount(result, arguments);

            return result.Delegate(arguments);
        }
        //---------------------------------------------------------------------
        public abstract ParsingResult Parse(string expression);
        //---------------------------------------------------------------------
        protected void CheckParameterCount(ParsingResult result, double[] arguments)
        {
            if (result.Parameters.Count == arguments.Length) return;

            var paramsWithouArgs = result
                .Parameters
                .Skip(arguments.Length);

            string msg =
                $"Expression contains {result.Parameters.Count} parameters, but {arguments.Length} are given.\n" +
                $"Parameters without args are: {string.Join(", ", paramsWithouArgs)}";

            throw new ArgumentException(msg, nameof(arguments));
        }
    }
}