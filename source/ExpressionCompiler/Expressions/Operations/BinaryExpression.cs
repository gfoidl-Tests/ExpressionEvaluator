using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal abstract class BinaryExpression : Expression
    {
        public Expression Left  { get; }
        public Expression Right { get; }
        //---------------------------------------------------------------------
        public BinaryExpression(Token token, Expression left, Expression right) : base(token)
        {
            this.Left  = left;
            this.Right = right;
        }
    }
}