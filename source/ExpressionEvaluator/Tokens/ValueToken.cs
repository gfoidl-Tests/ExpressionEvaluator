using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public class ValueToken : Token
    {
        public double Value { get; }
        //---------------------------------------------------------------------
        public ValueToken(double value) : base(value.ToString()) => this.Value = value;
        public ValueToken(string value) : this(double.Parse(value)) { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}