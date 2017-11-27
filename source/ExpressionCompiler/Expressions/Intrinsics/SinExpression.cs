using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal class SinExpression : IntrinsicExpression
    {
        public SinExpression(Intrinsic token, Expression argument)
            : base(token, argument)
        { }
        //---------------------------------------------------------------------
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}