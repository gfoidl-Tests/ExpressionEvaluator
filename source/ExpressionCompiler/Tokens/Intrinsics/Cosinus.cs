using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Cosinus : Intrinsic
    {
        public Cosinus(Position position) : base("Cosinus", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new CosExpression(this, arg);
    }
}