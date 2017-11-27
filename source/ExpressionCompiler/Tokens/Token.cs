using System.Diagnostics;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    [DebuggerDisplay("{Name}")]
    internal abstract class Token
    {
        public string Name  { get; }
        public int Position { get; }
        //---------------------------------------------------------------------
        protected Token(string name, int position)
        {
            this.Name     = name;
            this.Position = position;
        }
        //---------------------------------------------------------------------
        public abstract void Accept(ITokenVisitor visitor);
        //---------------------------------------------------------------------
        [DebuggerStepThrough]
        public override string ToString() => this.Name;
    }
}