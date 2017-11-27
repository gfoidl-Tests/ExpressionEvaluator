using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    public sealed class LeftParanthesis : Paranthesis
    {
        internal LeftParanthesis(int position) : base("Left (", position) { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}