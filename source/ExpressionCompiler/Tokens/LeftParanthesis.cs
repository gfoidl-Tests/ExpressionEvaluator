using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal sealed class LeftParanthesis : Paranthesis
    {
        internal LeftParanthesis(int position) : base("Left (", position) { }
        //---------------------------------------------------------------------
        public override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}