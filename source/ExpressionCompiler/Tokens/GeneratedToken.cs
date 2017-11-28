namespace ExpressionCompiler.Tokens
{
    internal class GeneratedToken : Token
    {
        public GeneratedToken() : base("CompilerGenerated", Position.NotDefined)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) { }
    }
}