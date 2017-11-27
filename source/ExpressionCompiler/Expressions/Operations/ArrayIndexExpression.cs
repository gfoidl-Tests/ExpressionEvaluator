using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal class ArrayIndexExpression : BinaryExpression
    {
        public ArrayIndexExpression(ParameterToken token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}