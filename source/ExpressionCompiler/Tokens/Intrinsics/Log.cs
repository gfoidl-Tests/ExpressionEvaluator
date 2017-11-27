using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Log : Intrinsic
    {
        public Log(int position) : base("Log", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new CosExpression(this, arg);
    }
}