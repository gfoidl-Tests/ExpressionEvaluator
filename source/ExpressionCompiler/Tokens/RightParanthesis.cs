using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal sealed class RightParanthesis : Paranthesis
    {
        internal RightParanthesis(int position) : base("Right )", position) { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}