using System.Diagnostics;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    [DebuggerDisplay("{Name}")]
    public abstract class Token
    {
        public string Name { get; }
        //---------------------------------------------------------------------
        protected Token(string name) => this.Name = name;
        //---------------------------------------------------------------------
        public abstract void Accept(IVisitor visitor);
        //---------------------------------------------------------------------
        [DebuggerStepThrough]
        public override string ToString() => this.Name;
    }
}