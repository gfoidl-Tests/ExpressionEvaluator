using System;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Optimizer
{
    internal class Optimizer : BaseOptimizer
    {
        public Optimizer(Expression tree) : base(tree) { }
        //---------------------------------------------------------------------
        public override Expression Visit(AddExpression addExpression)           => this.VisitBinaryCore(addExpression     , (a, b) => a + b) ?? base.Visit(addExpression);
        public override Expression Visit(SubtractExpression subtractExpression) => this.VisitBinaryCore(subtractExpression, (a, b) => a - b) ?? base.Visit(subtractExpression);
        public override Expression Visit(DivideExpression divideExpression)     => this.VisitBinaryCore(divideExpression  , (a, b) => a / b) ?? base.Visit(divideExpression);
        //---------------------------------------------------------------------
        public override Expression Visit(MultiplyExpression multiplyExpression)
        {
            var expression = this.TryParameterMultipliedBy2(multiplyExpression);

            return expression
                ?? this.VisitBinaryCore(multiplyExpression, (a, b) => a * b)
                ?? base.Visit(multiplyExpression);
        }
        //---------------------------------------------------------------------
        public override Expression Visit(ExponentationExpression exponentationExpression)
        {
            ArrayIndexExpression left  = exponentationExpression.Left  as ArrayIndexExpression;
            ConstantExpression   right = exponentationExpression.Right as ConstantExpression;

            if (left == null || right == null || right.Value != 2) return exponentationExpression;

            return new MultiplyExpression(exponentationExpression.Token as Operation, left, left);
        }
        //---------------------------------------------------------------------
        private BinaryExpression TryParameterMultipliedBy2(MultiplyExpression multiplyExpression)
        {
            ArrayIndexExpression parameter;
            ConstantExpression   constant;

            if (multiplyExpression.Left is ArrayIndexExpression p && multiplyExpression.Right is ConstantExpression c)
            {
                parameter = p;
                constant  = c;
            }
            else if (multiplyExpression.Left is ConstantExpression c1 && multiplyExpression.Right is ArrayIndexExpression p1)
            {
                parameter = p1;
                constant  = c1;
            }
            else
                return null;

            if (constant.Value != 2) return null;

            return new AddExpression(multiplyExpression.Token as Operation, parameter, parameter);
        }
        //---------------------------------------------------------------------
        private Expression VisitBinaryCore(BinaryExpression binaryExpression, Func<double, double, double> operation)
        {
            ConstantExpression left  = binaryExpression.Left as ConstantExpression;
            ConstantExpression right = binaryExpression.Right as ConstantExpression;

            if (left == null || right == null) return null;

            double res = operation(left.Value, right.Value);

            return new ConstantExpression(res);
        }
    }
}