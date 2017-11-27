using ExpressionCompiler.Expressions;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal class Subtraction : Operation
    {
        public Subtraction(int position) : base("Subtract", 1, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new SubtractExpression(this, left, right);
    }
}