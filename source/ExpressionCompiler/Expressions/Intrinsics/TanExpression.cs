using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class TanExpression : IntrinsicExpression
    {
        public TanExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}