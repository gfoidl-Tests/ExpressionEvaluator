using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class DivideExpression : BinaryExpression
    {
        public DivideExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}