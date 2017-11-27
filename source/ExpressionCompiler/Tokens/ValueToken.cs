﻿namespace ExpressionCompiler.Tokens
{
    internal class ValueToken : Token
    {
        public double Value { get; }
        //---------------------------------------------------------------------
        public ValueToken(int position, double value) : base(value.ToString(), position) => this.Value = value;
        public ValueToken(int position, string value) : this(position, double.Parse(value)) { }
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}