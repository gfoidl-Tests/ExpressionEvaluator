using System.Diagnostics;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    [DebuggerDisplay("{Name}")]
    public abstract class Token
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
        public abstract void Accept(IVisitor visitor);
        //---------------------------------------------------------------------
        [DebuggerStepThrough]
        public bool IsOfType<T>() where T : Token => this is T;
        //---------------------------------------------------------------------
        [DebuggerStepThrough]
        public override string ToString() => this.Name;
    }
}