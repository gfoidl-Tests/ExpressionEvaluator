using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public sealed class LeftParanthesis : Paranthesis
    {
        internal LeftParanthesis() : base("Left (") { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}