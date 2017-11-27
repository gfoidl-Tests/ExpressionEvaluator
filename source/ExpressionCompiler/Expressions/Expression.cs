using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Expressions
{
    internal abstract class Expression
    {
        public Token Token { get; }
        //---------------------------------------------------------------------
        protected Expression(Token token) => this.Token = token;
        //---------------------------------------------------------------------
        public abstract void Accept(IExpressionVisitor visitor);
    }
}