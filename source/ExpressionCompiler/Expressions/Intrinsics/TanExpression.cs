using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal class TanExpression : IntrinsicExpression
    {
        public TanExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}