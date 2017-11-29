using System.Collections.Generic;
using System.IO;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Parser;

namespace ExpressionCompiler
{
    public abstract class Compiler : ICompiler
    {
        public IReadOnlyCollection<string> Parameters { get; private set; }
        //---------------------------------------------------------------------
        public virtual bool Compile(string expression, bool optimize = false)
        {
            Expression tree = this.Parse(expression);

            if (tree == null) return false;

            if (optimize)
            {
                var optimizer = new Optimizer.Optimizer(tree);
                tree = optimizer.Optimize();
            }

            return this.Emit(tree);
        }
        //---------------------------------------------------------------------
        protected virtual Expression Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return null;

            var reader      = new StringReader(expression);
            var lexer       = new Lexer(new PositionTextReader(reader));
            var parser      = new Parser.Parser();
            var tokens      = lexer.ReadTokens();
            var result      = parser.Parse(tokens);
            this.Parameters = result.Parameters;

            return result.Tree;
        }
        //---------------------------------------------------------------------
        protected abstract bool Emit(Expression tree);
    }
}