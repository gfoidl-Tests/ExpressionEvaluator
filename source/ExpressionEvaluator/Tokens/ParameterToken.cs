using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public sealed class ParameterToken : Token
    {
        public string Parameter { get; }
        //---------------------------------------------------------------------
        public ParameterToken(int position, string parameter) : base(parameter, position) =>
            this.Parameter = parameter;
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}