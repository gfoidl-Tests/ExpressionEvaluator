using System.Diagnostics;

namespace ExpressionEvaluator
{
    [DebuggerDisplay("{Name}")]
    internal class Token
    {
        public string Name { get; }
        //---------------------------------------------------------------------
        public Token(string name) => this.Name = name;
    }
}