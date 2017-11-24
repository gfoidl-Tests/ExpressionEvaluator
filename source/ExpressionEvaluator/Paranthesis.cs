namespace ExpressionEvaluator
{
    internal sealed class Paranthesis : Symbol
    {
        public static readonly Paranthesis Left  = new Paranthesis("Left (");
        public static readonly Paranthesis Right = new Paranthesis("Right )");
        //---------------------------------------------------------------------
        private Paranthesis(string name) : base(name) { }
    }
}