using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    public abstract class Expression
    {
        internal Token Token { get; }
        //---------------------------------------------------------------------
        protected Expression(Token token) => this.Token = token;
        //---------------------------------------------------------------------
        internal abstract void Accept(IExpressionVisitor visitor);
    }
}