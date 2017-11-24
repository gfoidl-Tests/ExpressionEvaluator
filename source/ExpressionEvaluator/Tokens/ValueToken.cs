namespace ExpressionEvaluator.Tokens
{
    internal sealed class ValueToken : Token
    {
        public double Value { get; }
        //---------------------------------------------------------------------
        public ValueToken(double value) : base("Value") => this.Value = value;
        public ValueToken(string value) : this(double.Parse(value)) { }
    }
}