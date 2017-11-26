using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public class ValueToken : Token
    {
        public double Value { get; }
        //---------------------------------------------------------------------
        public ValueToken(int position, double value) : base(value.ToString(), position) => this.Value = value;
        public ValueToken(int position, string value) : this(position, double.Parse(value)) { }
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}