using System.Collections.Generic;

namespace ExpressionEvaluator
{
    public sealed class CachedExpressionEvaluator : ExpressionEvaluator
    {
        private readonly Dictionary<string, ParsingResult> _cache;
        private readonly ExpressionEvaluator               _expressionEvaluator;
        //---------------------------------------------------------------------
        public CachedExpressionEvaluator()
        {
            _cache               = new Dictionary<string, ParsingResult>();
            _expressionEvaluator = new BasicExpressionEvaluator();
        }
        //---------------------------------------------------------------------
        public override ParsingResult Parse(string expression)
        {
            if (_cache.TryGetValue(expression, out ParsingResult result))
                return result;

            result = _expressionEvaluator.Parse(expression);
            _cache.Add(expression, result);

            return result;
        }
    }
}