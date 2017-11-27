using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

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
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}