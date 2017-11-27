using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal class AddExpression : BinaryExpression
    {
        public AddExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}