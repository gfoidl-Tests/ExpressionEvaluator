using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal class GeneratedToken : Token
    {
        public GeneratedToken() : base("CompilerGenerated", -1)
        { }
        //---------------------------------------------------------------------
        public override void Accept(ITokenVisitor visitor) { }
    }
}