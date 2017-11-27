using System;
using ExpressionCompiler.Expressions;
using LE = System.Linq.Expressions;

namespace ExpressionCompiler.Emitter.LambdaExpression
{
    internal class LambdaExpressionEmitter : Emitter<LE.Expression>
    {
        private readonly LE.ParameterExpression _arrayParameter = LE.Expression.Parameter(typeof(double[]), "args");
        //---------------------------------------------------------------------
        public LE.Expression<Func<double[], double>> Result { get; private set; }
        //---------------------------------------------------------------------
        public LambdaExpressionEmitter(Expression tree) : base(tree) { }
        //---------------------------------------------------------------------
        public override void Emit()
        {
            LE.Expression expression = this.Tree.Accept(this);

            this.Result = LE.Expression.Lambda<Func<double[], double>>(expression, _arrayParameter);
        }
        //---------------------------------------------------------------------
        public override LE.Expression Visit(ConstantExpression      constant)                => LE.Expression.Constant(constant.Value);
        public override LE.Expression Visit(ArrayIndexExpression    arrayIndexExpression)    => LE.Expression.ArrayIndex(_arrayParameter, arrayIndexExpression.Right.Accept(this));
        public override LE.Expression Visit(AddExpression           addExpression)           => this.VisitBinaryCore(addExpression          , LE.Expression.Add);
        public override LE.Expression Visit(SubtractExpression      subtractExpression)      => this.VisitBinaryCore(subtractExpression     , LE.Expression.Subtract);
        public override LE.Expression Visit(MultiplyExpression      multiplyExpression)      => this.VisitBinaryCore(multiplyExpression     , LE.Expression.Multiply);
        public override LE.Expression Visit(DivideExpression        divideExpression)        => this.VisitBinaryCore(divideExpression       , LE.Expression.Divide);
        public override LE.Expression Visit(ExponentationExpression exponentationExpression) => this.VisitBinaryCore(exponentationExpression, LE.Expression.Power);
        public override LE.Expression Visit(ModuloExpression        moduloExpression)        => this.VisitBinaryCore(moduloExpression       , LE.Expression.Modulo);
        public override LE.Expression Visit(SinExpression           sinExpression)           => this.VisitIntrinsicsCore(sinExpression, Intrinsics.Sin);
        public override LE.Expression Visit(CosExpression           cosExpression)           => this.VisitIntrinsicsCore(cosExpression, Intrinsics.Cos);
        public override LE.Expression Visit(TanExpression           tanExpression)           => this.VisitIntrinsicsCore(tanExpression, Intrinsics.Tan);
        public override LE.Expression Visit(LogExpression           logExpression)           => this.VisitIntrinsicsCore(logExpression, Intrinsics.Log);
        //---------------------------------------------------------------------
        private LE.Expression VisitBinaryCore(
            BinaryExpression binaryExpression,
            Func<LE.Expression, LE.Expression, LE.Expression> factory)
        {
            LE.Expression left  = binaryExpression.Left.Accept(this);
            LE.Expression right = binaryExpression.Right.Accept(this);

            return factory(left, right);
        }
        //---------------------------------------------------------------------
        private LE.Expression VisitIntrinsicsCore(
            IntrinsicExpression intrinsicExpression,
            Func<LE.Expression, LE.Expression> factory)
        {
            LE.Expression arg = intrinsicExpression.Argument.Accept(this);

            return factory(arg);
        }
    }
}