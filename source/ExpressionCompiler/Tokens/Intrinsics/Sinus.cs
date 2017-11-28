using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Sinus : Intrinsic
    {
        public Sinus(Position position) : base("Sinus", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new SinExpression(this, arg);
    }
}