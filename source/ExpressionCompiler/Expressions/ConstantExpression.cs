using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ConstantExpression : Expression
    {
        public object Value { get; }
        //---------------------------------------------------------------------
        public ConstantExpression(ValueToken token)
            : base(token)
            => this.Value = token.Value;
        //---------------------------------------------------------------------
        public ConstantExpression(object value)
            : base(new GeneratedToken())
            => this.Value = value;
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}