using ExpressionCompiler.Expressions;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal class Addition : Operation
    {
        public Addition(int position) : base("Add", 1, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new AddExpression(this, left, right);
    }
}