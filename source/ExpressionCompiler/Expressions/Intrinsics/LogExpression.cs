using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class LogExpression : IntrinsicExpression
    {
        public LogExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}