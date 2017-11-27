using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Division : Operation
    {
        public Division(int position) : base("Divide", 2, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new DivideExpression(this, left, right);
    }
}