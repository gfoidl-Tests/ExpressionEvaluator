namespace ExpressionCompiler.Tokens
{
    internal sealed class ParameterToken : Token
    {
        public string Parameter { get; }
        //---------------------------------------------------------------------
        public ParameterToken(Position position, string parameter)
            : base(parameter, position)
            => this.Parameter = parameter;
        //---------------------------------------------------------------------
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}