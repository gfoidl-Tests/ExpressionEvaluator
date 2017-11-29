namespace ExpressionCompiler.Expressions
{
    internal interface IExpressionVisitor<T>
    {
        T Visit(ConstantExpression      constant);
        T Visit(IndexExpression         indexExpression);
        T Visit(ParameterExpression     parameterExpression);
        T Visit(ArrayIndexExpression    arrayIndexExpression);
        T Visit(AddExpression           addExpression);
        T Visit(SubtractExpression      subtractExpression);
        T Visit(MultiplyExpression      multiplyExpression);
        T Visit(DivideExpression        divideExpression);
        T Visit(ExponentationExpression exponentationExpression);
        T Visit(ModuloExpression        moduloExpression);
        T Visit(SinExpression           sinExpression);
        T Visit(CosExpression           cosExpression);
        T Visit(TanExpression           tanExpression);
        T Visit(LogExpression           logExpression);
        T Visit(SqrtExpression          sqrtExpression);
    }
}