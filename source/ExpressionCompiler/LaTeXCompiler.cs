using System.Text;
using ExpressionCompiler.Emitter.LaTeX;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class LaTeXCompiler : Compiler
    {
        private readonly StringBuilder _sb;
        //---------------------------------------------------------------------
        public LaTeXCompiler(StringBuilder sb) => _sb = sb;
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new LaTeXEmitter(tree, _sb);
            emitter.Emit();

            return true;
        }
    }
}