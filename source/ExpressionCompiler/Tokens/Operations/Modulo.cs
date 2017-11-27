using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Modulo : Operation
    {
        public Modulo(int position) : base("Modulo", 3, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new ModuloExpression(this, left, right);
    }
}