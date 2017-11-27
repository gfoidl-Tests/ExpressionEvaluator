using System.Collections.Generic;
using ExpressionCompiler.Parser;

namespace ExpressionEvaluator
{
    public sealed class CachedExpressionEvaluator : ExpressionEvaluator
    {
        private readonly Dictionary<string, Result> _cache;
        private readonly ExpressionEvaluator        _expressionEvaluator;
        //---------------------------------------------------------------------
        public CachedExpressionEvaluator()
        {
            _cache               = new Dictionary<string, Result>();
            _expressionEvaluator = new BasicExpressionEvaluator();
        }
        //---------------------------------------------------------------------
        public override Result Parse(string expression)
        {
            if (_cache.TryGetValue(expression, out Result result))
                return result;

            result = _expressionEvaluator.Parse(expression);
            _cache.Add(expression, result);

            return result;
        }
    }
}