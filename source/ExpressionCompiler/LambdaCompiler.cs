using System;
using ExpressionCompiler.Emitter.LambdaExpression;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    public class LambdaCompiler : Compiler
    {
        public Func<double[], double> Delegate { get; private set; }
        //---------------------------------------------------------------------
        protected override bool Emit(Expression tree)
        {
            var emitter = new LambdaExpressionEmitter(tree);
            emitter.Emit();

            var linqExpression = emitter.Result;
            this.Delegate      = linqExpression.Compile();

            return true;
        }
    }
}