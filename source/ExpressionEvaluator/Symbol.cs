using System.Diagnostics;

namespace ExpressionEvaluator
{
    [DebuggerDisplay("{Name}")]
    internal class Symbol
    {
        public string Name { get; }
        //---------------------------------------------------------------------
        public Symbol(string name) => this.Name = name;
    }
}