using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ArrayIndexExpression : BinaryExpression
    {
        public ArrayIndexExpression(ParameterToken token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}