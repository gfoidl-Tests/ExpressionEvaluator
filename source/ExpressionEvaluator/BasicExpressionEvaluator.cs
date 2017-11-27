using ExpressionCompiler;

namespace ExpressionEvaluator
{
    public sealed class BasicExpressionEvaluator : ExpressionEvaluator
    {
        public override Result Parse(string expression)
        {
            var compiler = new LambdaCompiler();
            bool success = compiler.Compile(expression);

            if (!success) return null;

            return new Result
            {
                Delegate   = compiler.Delegate,
                Parameters = compiler.Parameters
            };
        }
    }
}