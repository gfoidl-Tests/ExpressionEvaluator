using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class CosExpression : IntrinsicExpression
    {
        public CosExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}