using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Tangens : Intrinsic
    {
        public Tangens(int position) : base("Tangens", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new TanExpression(this, arg);
    }
}