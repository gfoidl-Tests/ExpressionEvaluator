using System.IO;
using ExpressionCompiler.Emitter.Dot;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class DotCompiler : Compiler
    {
        private readonly TextWriter _writer;
        //---------------------------------------------------------------------
        public DotCompiler(TextWriter writer) => _writer = writer;
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new DotEmitter(tree, _writer);
            emitter.Emit();

            return true;
        }
    }
}