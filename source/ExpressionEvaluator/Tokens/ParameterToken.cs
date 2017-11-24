namespace ExpressionEvaluator.Tokens
{
    internal sealed class ParameterToken : Token
    {
        public string Parameter { get; }
        //---------------------------------------------------------------------
        public ParameterToken(string parameter) : base("Parameter") => this.Parameter = parameter;
    }
}