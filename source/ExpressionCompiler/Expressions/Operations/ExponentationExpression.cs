using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ExponentationExpression : BinaryExpression
    {
        public ExponentationExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}