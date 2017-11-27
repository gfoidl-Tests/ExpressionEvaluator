using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal class CosExpression : IntrinsicExpression
    {
        public CosExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}