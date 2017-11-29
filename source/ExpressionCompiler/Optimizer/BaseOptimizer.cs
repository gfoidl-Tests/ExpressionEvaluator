using System;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Optimizer
{
    internal abstract class BaseOptimizer : IExpressionVisitor<Expression>
    {
        public Expression Tree { get; }
        //---------------------------------------------------------------------
        protected BaseOptimizer(Expression tree) => this.Tree = tree;
        //---------------------------------------------------------------------
        public Expression Optimize() => this.Tree.Accept(this);
        //---------------------------------------------------------------------
        public virtual Expression Visit(ConstantExpression      constant)                => constant;
        public virtual Expression Visit(IndexExpression         indexExpression)         => indexExpression;
        public virtual Expression Visit(ParameterExpression     parameterExpression)     => parameterExpression;
        public virtual Expression Visit(ArrayIndexExpression    arrayIndexExpression)    => arrayIndexExpression;
        public virtual Expression Visit(AddExpression           addExpression)           => this.VisitBinaryCore(addExpression          , (t, l, r) => new AddExpression(t, l, r));
        public virtual Expression Visit(SubtractExpression      subtractExpression)      => this.VisitBinaryCore(subtractExpression     , (t, l, r) => new SubtractExpression(t, l, r));
        public virtual Expression Visit(MultiplyExpression      multiplyExpression)      => this.VisitBinaryCore(multiplyExpression     , (t, l, r) => new MultiplyExpression(t, l, r));
        public virtual Expression Visit(DivideExpression        divideExpression)        => this.VisitBinaryCore(divideExpression       , (t, l, r) => new DivideExpression(t, l, r));
        public virtual Expression Visit(ExponentationExpression exponentationExpression) => this.VisitBinaryCore(exponentationExpression, (t, l, r) => new ExponentationExpression(t, l, r));
        public virtual Expression Visit(ModuloExpression        moduloExpression)        => this.VisitBinaryCore(moduloExpression       , (t, l, r) => new ModuloExpression(t, l, r));
        public virtual Expression Visit(SinExpression           sinExpression)           => this.VisitIntrinsicsCore(sinExpression      , (t, e) => new SinExpression(t, e));
        public virtual Expression Visit(CosExpression           cosExpression)           => this.VisitIntrinsicsCore(cosExpression      , (t, e) => new CosExpression(t, e));
        public virtual Expression Visit(TanExpression           tanExpression)           => this.VisitIntrinsicsCore(tanExpression      , (t, e) => new TanExpression(t, e));
        public virtual Expression Visit(LogExpression           logExpression)           => this.VisitIntrinsicsCore(logExpression      , (t, e) => new LogExpression(t, e));
        public virtual Expression Visit(SqrtExpression          sqrtExpression)          => this.VisitIntrinsicsCore(sqrtExpression     , (t, e) => new SqrtExpression(t, e));
        //---------------------------------------------------------------------
        protected Expression VisitBinaryCore(BinaryExpression binaryExpression, Func<Operation, Expression, Expression, Expression> func)
        {
            Expression left  = binaryExpression.Left.Accept(this);
            Expression right = binaryExpression.Right.Accept(this);

            return func(binaryExpression.Token as Operation, left, right);
        }
        //---------------------------------------------------------------------
        protected Expression VisitIntrinsicsCore(IntrinsicExpression intrinsics, Func<Intrinsic, Expression, Expression> func)
        {
            Expression arg = intrinsics.Argument.Accept(this);

            return func(intrinsics.Token as Intrinsic, arg);
        }
    }
}