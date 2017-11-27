using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ParameterExpression : Expression
    {
        public string Parameter { get; }
        //---------------------------------------------------------------------
        public ParameterExpression(ParameterToken token)
            : base(token)
            => this.Parameter = token.Parameter;
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}