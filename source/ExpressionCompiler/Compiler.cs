using System.IO;

namespace ExpressionCompiler
{
    public class Compiler
    {
        public ParsingResult Compile(string expression)
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