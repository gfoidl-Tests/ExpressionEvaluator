using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Exponentation : Operation
    {
        public Exponentation(Position position) : base("Exponentation", 2, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new ExponentationExpression(this, left, right);
    }
}