using System.IO;
using ExpressionCompiler.Parser;

namespace ExpressionCompiler
{
    public abstract class Compiler
    {
        public abstract void Compile(string expression);
        //---------------------------------------------------------------------
        protected ParsingResult Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            var reader = new StringReader(expression);
            var lexer  = new Lexer(new PositionTextReader(reader));
            var parser = new Parser.Parser();
            var tokens = lexer.ReadTokens();
            var result = parser.Parse(tokens);

            return result;
        }
    }
}