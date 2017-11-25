using System.Diagnostics;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    [DebuggerDisplay("{Name}")]
    internal abstract class Token
    {
        public string Name { get; }
        //---------------------------------------------------------------------
        protected Token(string name) => this.Name = name;
        //---------------------------------------------------------------------
        public abstract void Accept(IVisitor visitor);
    }
}