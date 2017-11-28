using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Log : Intrinsic
    {
        public Log(Position position) : base("Log", position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression arg) => new LogExpression(this, arg);
    }
}