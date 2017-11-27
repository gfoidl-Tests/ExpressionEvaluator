using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class ModuloExpression : BinaryExpression
    {
        public ModuloExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}