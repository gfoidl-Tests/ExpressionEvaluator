namespace ExpressionCompiler.Tokens
{
    internal sealed class RightParanthesis : Paranthesis
    {
        internal RightParanthesis(int position) : base("Right )", position) { }
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}