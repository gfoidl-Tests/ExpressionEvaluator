using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Multiplication : Operation
    {
        public Multiplication(Position position) : base("Multiply", 2, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new MultiplyExpression(this, left, right);
    }
}