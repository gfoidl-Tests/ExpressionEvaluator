using System.IO;
using ExpressionCompiler.Emitter.TinyStackMachine;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class TinyStackMachineCompiler : Compiler
    {
        private readonly TextWriter _writer;
        private readonly TextWriter _debugInfoWriter;
        //---------------------------------------------------------------------
        public TinyStackMachineCompiler(TextWriter writer, TextWriter debugInfoWriter = null)
        {
            _writer          = writer;
            _debugInfoWriter = debugInfoWriter;
        }
        //---------------------------------------------------------------------
        public override bool Compile(string expression, bool optimize = false)
        {
            _debugInfoWriter?.WriteLine(expression);

            return base.Compile(expression, optimize);
        }
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new TinyStackMachineEmitter(tree, _writer, _debugInfoWriter);
            emitter.Emit();

            return true;
        }
    }
}