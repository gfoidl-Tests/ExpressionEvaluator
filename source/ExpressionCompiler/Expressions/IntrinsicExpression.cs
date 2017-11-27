using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal abstract class IntrinsicExpression : Expression
    {
        public Expression Argument { get; }
        //---------------------------------------------------------------------
        public IntrinsicExpression(Intrinsic token, Expression argument)
            : base(token)
            => this.Argument = argument;
    }
}