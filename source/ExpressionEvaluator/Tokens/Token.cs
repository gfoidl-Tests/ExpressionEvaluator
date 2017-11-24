using System.Diagnostics;

namespace ExpressionEvaluator.Tokens
{
    [DebuggerDisplay("{Name}")]
    internal class Token
    {
        public string Name { get; }
        //---------------------------------------------------------------------
        public Token(string name) => this.Name = name;
    }
}