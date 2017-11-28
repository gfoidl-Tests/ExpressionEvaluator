using System.Diagnostics;

namespace ExpressionCompiler.Tokens
{
    [DebuggerDisplay("{Name}")]
    public abstract class Token
    {
        public string Name       { get; }
        public Position Position { get; }
        //---------------------------------------------------------------------
        protected Token(string name, Position position)
        {
            this.Name     = name;
            this.Position = position;
        }
        //---------------------------------------------------------------------
        internal abstract void Accept(ITokenVisitor visitor);
        //---------------------------------------------------------------------
        [DebuggerStepThrough]
        public override string ToString() => this.Name;
    }
}