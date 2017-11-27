using ExpressionCompiler.Expressions;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal class Multiplication : Operation
    {
        public Multiplication(int position) : base("Multiply", 2, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new MultiplyExpression(this, left, right);
    }
}