namespace ExpressionEvaluator.Tokens
{
    public abstract class Paranthesis : Token
    {
        public static readonly LeftParanthesis Left   = new LeftParanthesis();
        public static readonly RightParanthesis Right = new RightParanthesis();
        //---------------------------------------------------------------------
        protected Paranthesis(string name) : base(name) { }
    }
}