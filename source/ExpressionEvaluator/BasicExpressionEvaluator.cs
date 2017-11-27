using ExpressionCompiler;

namespace ExpressionEvaluator
{
    public sealed class BasicExpressionEvaluator : ExpressionEvaluator
    {
        public override ParsingResult Parse(string expression)
        {
            var compiler = new Compiler();
            return compiler.Compile(expression);
        }
    }
}