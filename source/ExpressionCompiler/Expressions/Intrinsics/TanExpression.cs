using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class TanExpression : IntrinsicExpression
    {
        public TanExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}