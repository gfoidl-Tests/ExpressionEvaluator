using System;
using ExpressionCompiler.Emitter.LambdaExpression;

namespace ExpressionCompiler
{
    public class LambdaCompiler : Compiler
    {
        public Func<double[], double> Delegate { get; private set; }
        //---------------------------------------------------------------------
        public override bool Compile(string expression)
        {
            var parsingResult = this.Parse(expression);

            if (parsingResult == null) return false;

            var emitter = new LambdaExpressionEmitter(parsingResult);

            emitter.Emit();

            var linqExpression = emitter.Result;
            this.Delegate      = linqExpression.Compile();

            return true;
        }
    }
}