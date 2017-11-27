using System;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Emitter
{
    internal abstract class Emitter<T> : IExpressionVisitor<T>
    {
        public Expression Tree { get; }
        //---------------------------------------------------------------------
        protected Emitter(Expression tree) => this.Tree = tree;
        //---------------------------------------------------------------------
        public abstract void Emit();
        //---------------------------------------------------------------------
        public abstract T Visit(ConstantExpression      constant);
        public abstract T Visit(ArrayIndexExpression    arrayIndexExpression);
        public abstract T Visit(AddExpression           addExpression);
        public abstract T Visit(SubtractExpression      subtractExpression);
        public abstract T Visit(MultiplyExpression      multiplyExpression);
        public abstract T Visit(DivideExpression        divideExpression);
        public abstract T Visit(ExponentationExpression exponentationExpression);
        public abstract T Visit(ModuloExpression        moduloExpression);
        public abstract T Visit(SinExpression           sinExpression);
        public abstract T Visit(CosExpression           cosExpression);
        public abstract T Visit(TanExpression           tanExpression);
        public abstract T Visit(LogExpression           logExpression);
        //---------------------------------------------------------------------
        public T Visit(ParameterExpression parameterExpression)
            => throw new InvalidOperationException("Should not be called internally");
    }
}