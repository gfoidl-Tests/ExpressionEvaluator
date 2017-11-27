using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ArrayIndexExpression : BinaryExpression
    {
        public ArrayIndexExpression(ParameterToken token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}