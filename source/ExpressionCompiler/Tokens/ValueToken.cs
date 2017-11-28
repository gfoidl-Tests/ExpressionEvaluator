using System.Globalization;

namespace ExpressionCompiler.Tokens
{
    internal class ValueToken : Token
    {
        public double Value { get; }
        //---------------------------------------------------------------------
        public ValueToken(Position position, double value) : base(value.ToString(), position) => this.Value = value;
        public ValueToken(Position position, string value) : this(position, double.Parse(value, CultureInfo.InvariantCulture)) { }
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}