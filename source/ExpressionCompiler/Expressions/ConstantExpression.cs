using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ConstantExpression : ConstantExpression<double>
    {
        public ConstantExpression(ValueToken token) : base(token, token.Value) { }
        public ConstantExpression(double value)     : base(value) { }
        //--------------------------------------------------------------------- 
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}