namespace ExpressionCompiler.Tokens
{
    internal class GeneratedToken : Token
    {
        public GeneratedToken() : base("CompilerGenerated", -1)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) { }
    }
}