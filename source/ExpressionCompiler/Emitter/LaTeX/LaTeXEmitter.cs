using System.Text;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Emitter.LaTeX
{
    internal class LaTeXEmitter : Emitter<bool>
    {
        private readonly PrettyPrintRules _prettyPrintRules = new PrettyPrintRules();
        private StringBuilder             _sb;
        //---------------------------------------------------------------------
        public LaTeXEmitter(Expression tree, StringBuilder sb)
            : base(tree)
            => _sb = sb;
        //---------------------------------------------------------------------
        public override void Emit() => this.Tree.Accept(this);
        //---------------------------------------------------------------------
        public override bool Visit(ConstantExpression constant)
        {
            if (constant.Token is Constant c)
                if (c.Name == "pi")
                    _sb.Append(@" \pi ");
                else
                    _sb.Append(" ").Append(constant.Token.Name).Append(" ");
            else
                _sb.Append(" ").Append(constant.Value).Append(" ");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ArrayIndexExpression arrayIndexExpression)
        {
            var parameterToken = arrayIndexExpression.Token as ParameterToken;

            _sb.Append(" ").Append(parameterToken.Parameter).Append(" ");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(AddExpression addExpression)           => this.VisitBinaryCore(addExpression, "+");
        public override bool Visit(SubtractExpression subtractExpression) => this.VisitBinaryCore(subtractExpression, "-");
        public override bool Visit(MultiplyExpression multiplyExpression) => this.VisitBinaryCore(multiplyExpression, @"\cdot");
        public override bool Visit(ModuloExpression moduloExpression)     => this.VisitBinaryCore(moduloExpression, @"\mod");
        //---------------------------------------------------------------------
        public override bool Visit(DivideExpression divideExpression)
        {
            _sb.Append(@"\frac{");
            divideExpression.Left.Accept(this);
            _sb.Append("}{");
            divideExpression.Right.Accept(this);
            _sb.Append("}");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ExponentationExpression exponentationExpression)
        {
            exponentationExpression.Left.Accept(this);
            _sb.Append("^{");
            exponentationExpression.Right.Accept(this);
            _sb.Append("}");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(SinExpression sinExpression) => this.VisitInstrinsicsCore(sinExpression, @"\sin");
        public override bool Visit(CosExpression cosExpression) => this.VisitInstrinsicsCore(cosExpression, @"\cos");
        public override bool Visit(TanExpression tanExpression) => this.VisitInstrinsicsCore(tanExpression, @"\tan");
        public override bool Visit(LogExpression logExpression) => this.VisitInstrinsicsCore(logExpression, @"\log");
        //---------------------------------------------------------------------
        private bool VisitBinaryCore(BinaryExpression binaryExpression, string cmd)
        {
            this.VisitBinarySide(binaryExpression, binaryExpression.Left);

            if (_prettyPrintRules.PrintOperator(binaryExpression))
                _sb.Append(cmd);

            this.VisitBinarySide(binaryExpression, binaryExpression.Right);

            return true;
        }
        //---------------------------------------------------------------------
        private bool VisitInstrinsicsCore(IntrinsicExpression intrinsic, string cmd)
        {
            _sb.Append(" ").Append(cmd).Append(@" \left(");

            intrinsic.Argument.Accept(this);

            _sb.Append(@"\right)");

            return true;
        }
        //---------------------------------------------------------------------
        private void VisitBinarySide(BinaryExpression binaryExpression, Expression expression)
        {
            if (_prettyPrintRules.NeedParanthesis(expression, binaryExpression))
            {
                StringBuilder globalBuilder = _sb;
                _sb = new StringBuilder();

                expression.Accept(this);

                globalBuilder
                    .Append(@"\left(")
                    .Append(_sb.ToString())
                    .Append(@"\right)");

                _sb = globalBuilder;
            }
            else
                expression.Accept(this);
        }
    }
}