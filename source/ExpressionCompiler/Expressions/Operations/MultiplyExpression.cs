using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class MultiplyExpression : BinaryExpression
    {
        public MultiplyExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}