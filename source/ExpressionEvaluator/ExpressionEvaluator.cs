using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpressionEvaluator
{
    public abstract class ExpressionEvaluator
    {
        public double Evaluate(string expression, object arguments)
        {
            Result result = this.Parse(expression);

            if (result == null) return double.NaN;

            Dictionary<string, double> args = GetArgs();

            this.CheckParameterCount(result, args);

            double[] values = result.Parameters.Select(p => args[p]).ToArray();

            return result.Delegate(values);
            //-----------------------------------------------------------------
            Dictionary<string, double> GetArgs()
            {
                Type argsType  = arguments.GetType();
                var properties = argsType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.CanRead && p.PropertyType.IsNumeric());

                return properties.ToDictionary(
                    p => p.Name,
                    p => Convert.ToDouble(p.GetValue(arguments))
                );
            }
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression, params double[] arguments)
        {
            Result result = this.Parse(expression);

            if (result == null) return double.NaN;

            this.CheckParameterCount(result, arguments);

            return result.Delegate(arguments);
        }
        //---------------------------------------------------------------------
        public abstract Result Parse(string expression);
        //---------------------------------------------------------------------
        private void CheckParameterCount(Result result, double[] arguments)
        {
            if (result.Parameters.Count == arguments.Length) return;

            var paramsWithoutArgs = result
                .Parameters
                .Skip(arguments.Length);

            throw ParameterCountDoesntMatch(result.Parameters.Count, arguments.Length, paramsWithoutArgs);
        }
        //---------------------------------------------------------------------
        private void CheckParameterCount(Result result, Dictionary<string, double> arguments)
        {
            if (result.Parameters.Count == arguments.Count) return;

            var paramsWithoutArgs = result
                .Parameters
                .Where(p => !arguments.ContainsKey(p));

            throw ParameterCountDoesntMatch(result.Parameters.Count, arguments.Count, paramsWithoutArgs);
        }
        //---------------------------------------------------------------------
        private static ArgumentException ParameterCountDoesntMatch(
            int parameters,
            int given,
            IEnumerable<string> paramsWithoutArgs)
        {
            string msg =
                $"Expression contains {parameters} parameters, but {given} are given.\n" +
                $"Parameters without args are: {string.Join(", ", paramsWithoutArgs)}";

            return new ArgumentException(msg, "arguments");
        }
    }
}