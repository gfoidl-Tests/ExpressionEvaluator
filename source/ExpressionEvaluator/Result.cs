using System;
using System.Collections.Generic;

namespace ExpressionEvaluator
{
    public class Result
    {
        public Func<double[], double>      Delegate   { get; set; }
        public IReadOnlyCollection<string> Parameters { get; set; }
    }
}