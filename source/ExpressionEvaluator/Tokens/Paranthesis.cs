using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    internal sealed class Paranthesis : Token
    {
        public static readonly Paranthesis Left  = new Paranthesis("Left (");
        public static readonly Paranthesis Right = new Paranthesis("Right )");
        //---------------------------------------------------------------------
        private Paranthesis(string name) : base(name) { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}