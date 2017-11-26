using System.IO;

namespace ExpressionEvaluator
{
    public sealed class BasicExpressionEvaluator : ExpressionEvaluator
    {
        public override ParsingResult Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            var reader = new StringReader(expression);
            var lexer  = new Lexer(new PositionTextReader(reader));
            var parser = new Parser();
            var tokens = lexer.ReadTokens();
            var result = parser.Parse(tokens);

            return result;
        }
    }
}