using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
#if DEBUG
            var tokens = lexer.ReadTokens().ToArray();
#else
            var token = lexer.ReadTokens();
#endif
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

            for (int i = 0; i < 20; ++i)
            {
#if DEBUG
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Optimization pass # {i + 1}");
                Console.ResetColor();
#endif
                var optimizer = new Optimizer.Optimizer(tree);
                tree          = optimizer.Optimize();

                if (optimizationLevel == OptimizationLevel.Simple
                    || !optimizer.DidAnyOptimization)
                    break;
            }

            return tree;
        }
    }
}