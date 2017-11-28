using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Sqrt : Intrinsic
    {
        public Sqrt(Position position) : base("Sqrt", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new SqrtExpression(this, arg);
    }
}