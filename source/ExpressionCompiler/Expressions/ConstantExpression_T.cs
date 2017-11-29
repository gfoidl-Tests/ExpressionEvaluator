using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal abstract class ConstantExpression<TValue> : Expression where TValue : struct
    {
        public TValue Value { get; }
        //---------------------------------------------------------------------
        protected ConstantExpression(ValueToken token, TValue value)
            : base(token)
            => this.Value = value;
        //---------------------------------------------------------------------
        protected ConstantExpression(TValue value)
            : base(new GeneratedToken())
            => this.Value = value;
    }
}