using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Subtraction : Operation
    {
        public Subtraction(Position position) : base("Subtract", 1, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new SubtractExpression(this, left, right);
    }
}