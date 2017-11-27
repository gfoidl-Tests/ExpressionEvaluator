using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class SinExpression : IntrinsicExpression
    {
        public SinExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}