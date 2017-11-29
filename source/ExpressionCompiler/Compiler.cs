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
        public virtual bool Compile(string expression, OptimizationLevel optimizationLevel = OptimizationLevel.None)
        {
            Expression tree = this.Parse(expression);

            if (tree == null) return false;

            tree = this.Optimize(tree, optimizationLevel);
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
        //---------------------------------------------------------------------
        private Expression Optimize(Expression tree, OptimizationLevel optimizationLevel)
        {
            if (optimizationLevel == OptimizationLevel.None) return tree;

            for (int i = 0; i < 10; ++i)
            {
                var optimizer = new Optimizer.Optimizer(tree);
                tree          = optimizer.Optimize();

                if (optimizationLevel == OptimizationLevel.Simple)
                    break;
            }

            return tree;
        }
    }
}