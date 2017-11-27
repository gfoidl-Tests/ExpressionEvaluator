using System.IO;
using ExpressionCompiler.Emitter.TinyStackMachine;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class TinyStackMachineCompiler : Compiler
    {
        private readonly TextWriter _writer;
        //---------------------------------------------------------------------
        public TinyStackMachineCompiler(TextWriter writer) => _writer = writer;
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new TinyStackMachineEmitter(tree, _writer);
            emitter.Emit();

            return true;
        }
    }
}