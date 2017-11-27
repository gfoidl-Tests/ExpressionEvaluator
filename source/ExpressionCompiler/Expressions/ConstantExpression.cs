using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

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
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}