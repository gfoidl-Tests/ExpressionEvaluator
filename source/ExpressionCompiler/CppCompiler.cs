using System.IO;
using ExpressionCompiler.Emitter.Cpp;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class CppCompiler : Compiler
    {
        private readonly TextWriter _writer;
        //---------------------------------------------------------------------
        public CppCompiler(TextWriter writer) => _writer = writer;
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new CppEmitter(tree, this.Parameters, _writer);
            emitter.Emit();

            return true;
        }
    }
}