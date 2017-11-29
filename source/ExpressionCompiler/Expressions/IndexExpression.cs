namespace ExpressionCompiler.Expressions
{
    internal class IndexExpression : ConstantExpression<int>
    {
        public IndexExpression(int index) : base(index) { }
        //---------------------------------------------------------------------
        internal override T Accept<T>(IExpressionVisitor<T> visitor) => visitor.Visit(this);
    }
}