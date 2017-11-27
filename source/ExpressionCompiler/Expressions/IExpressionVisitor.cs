namespace ExpressionCompiler.Expressions
{
    internal interface IExpressionVisitor
    {
        void Visit(ConstantExpression      constant);
        void Visit(ParameterExpression     parameterExpression);
        void Visit(ArrayIndexExpression    arrayIndexExpression);
        void Visit(AddExpression           addExpression);
        void Visit(SubtractExpression      subtractExpression);
        void Visit(MultiplyExpression      multiplyExpression);
        void Visit(DivideExpression        divideExpression);
        void Visit(ExponentationExpression exponentationExpression);
        void Visit(ModuloExpression        moduloExpression);
        void Visit(SinExpression           sinExpression);
        void Visit(CosExpression           cosExpression);
        void Visit(TanExpression           tanExpression);
        void Visit(LogExpression           logExpression);
    }
}