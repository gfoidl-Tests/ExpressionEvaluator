using System.Collections.Generic;
using System.IO;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Emitter.Dot
{
    internal class DotEmitter : Emitter<bool>
    {
        private readonly TextWriter   _writer;
        private int                   _opCounter;
        private readonly List<string> _labels = new List<string>();
        //---------------------------------------------------------------------
        public DotEmitter(Expression tree, TextWriter writer)
            : base(tree)
            => _writer = writer;
        //---------------------------------------------------------------------
        public override void Emit()
        {
            _writer.WriteLine("digraph Formula {");

            this.Tree.Accept(this);

            _writer.WriteLine();

            foreach (string label in _labels)
                _writer.WriteLine(label);

            _writer.WriteLine("}");
        }
        //---------------------------------------------------------------------
        public override bool Visit(ConstantExpression constant)
        {
            string name;

            if (constant.Token is Constant c)
                name = c.Name;
            else
                name = constant.Value.ToString();

            string id = this.GetNextOpId(name);

            _writer.WriteLine($"{id};");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ArrayIndexExpression arrayIndexExpression)
        {
            var parameterToken = arrayIndexExpression.Token as ParameterToken;

            _writer.WriteLine($"{this.GetNextOpId(parameterToken.Parameter)};");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(AddExpression addExpression)                     => this.VisitBinaryCore(addExpression, "+");
        public override bool Visit(SubtractExpression subtractExpression)           => this.VisitBinaryCore(subtractExpression, "-");
        public override bool Visit(MultiplyExpression multiplyExpression)           => this.VisitBinaryCore(multiplyExpression, "*");
        public override bool Visit(DivideExpression divideExpression)               => this.VisitBinaryCore(divideExpression, "/");
        public override bool Visit(ExponentationExpression exponentationExpression) => this.VisitBinaryCore(exponentationExpression, "^");
        public override bool Visit(ModuloExpression moduloExpression)               => this.VisitBinaryCore(moduloExpression, "mod");
        public override bool Visit(SinExpression sinExpression)                     => this.VisitIntrinsicsCore(sinExpression, "sin");
        public override bool Visit(CosExpression cosExpression)                     => this.VisitIntrinsicsCore(cosExpression, "cos");
        public override bool Visit(TanExpression tanExpression)                     => this.VisitIntrinsicsCore(tanExpression, "tan");
        public override bool Visit(LogExpression logExpression)                     => this.VisitIntrinsicsCore(logExpression, "log");
        public override bool Visit(SqrtExpression sqrtExpression)                   => this.VisitIntrinsicsCore(sqrtExpression, "sqrt");
        //---------------------------------------------------------------------
        private bool VisitBinaryCore(BinaryExpression binaryExpression, string cmd)
        {
            string opId = this.GetNextOpId(cmd);

            this.VisitBinarySide(opId, binaryExpression.Left);
            this.VisitBinarySide(opId, binaryExpression.Right);

            return true;
        }
        //---------------------------------------------------------------------
        private bool VisitIntrinsicsCore(IntrinsicExpression intrinsicExpression, string cmd)
        {
            string opId = this.GetNextOpId(cmd);

            _writer.Write($"{opId} -> ");
            intrinsicExpression.Argument.Accept(this);

            return true;
        }
        //---------------------------------------------------------------------
        private void VisitBinarySide(string opId, Expression side)
        {
            _writer.Write($"{opId} -> ");
            side.Accept(this);
        }
        //---------------------------------------------------------------------
        private string GetNextOpId(string cmd)
        {
            string opId = $".{_opCounter++}";
            _labels.Add($"{opId} [label=\"{cmd}\"]");

            return opId;
        }
    }
}