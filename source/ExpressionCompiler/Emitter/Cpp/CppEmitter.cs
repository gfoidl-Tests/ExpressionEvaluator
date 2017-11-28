using System.Collections.Generic;
using System.IO;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Emitter.Cpp
{
    internal class CppEmitter : Emitter<bool>
    {
        private readonly TextWriter                  _writer;
        private readonly IReadOnlyCollection<string> _parameters;
        private bool                                 _isFirstBinary = true;
        //---------------------------------------------------------------------
        public CppEmitter(Expression tree, IReadOnlyCollection<string> parameters, TextWriter writer)
            : base(tree)
        {
            _writer     = writer;
            _parameters = parameters;
        }
        //---------------------------------------------------------------------
        public override void Emit()
        {
            _writer.WriteLine("#include <math.h>");
            _writer.WriteLine("#include <iostream>");
            _writer.WriteLine();
            _writer.WriteLine("using namespace std;");
            _writer.WriteLine();
            this.EmitArgs();
            _writer.WriteLine("int main()");
            _writer.WriteLine("{");
            _writer.Write("\tdouble res = ");

            this.Tree.Accept(this);

            _writer.WriteLine(";");
            _writer.WriteLine();
            _writer.WriteLine("\tcout << res << endl;");
            _writer.WriteLine("}");
        }
        //---------------------------------------------------------------------
        private void EmitArgs()
        {
            if ((_parameters?.Count ?? 0) == 0) return;

            _writer.WriteLine($"double args[{_parameters.Count}]; // supply arguments");
            _writer.WriteLine();
        }
        //---------------------------------------------------------------------
        public override bool Visit(ConstantExpression constant)
        {
            _writer.Write(constant.Value);
            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ArrayIndexExpression arrayIndexExpression)
        {
            _writer.Write("args[");

            arrayIndexExpression.Right.Accept(this);

            _writer.Write("]");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(AddExpression addExpression)                     => this.VisitBinaryCore(addExpression, "+");
        public override bool Visit(SubtractExpression subtractExpression)           => this.VisitBinaryCore(subtractExpression, "-");
        public override bool Visit(MultiplyExpression multiplyExpression)           => this.VisitBinaryCore(multiplyExpression, "*");
        public override bool Visit(DivideExpression divideExpression)               => this.VisitBinaryCore(divideExpression, "/");
        public override bool Visit(ExponentationExpression exponentationExpression) => this.VisitBinaryCore(exponentationExpression, "pow");
        public override bool Visit(ModuloExpression moduloExpression)               => this.VisitBinaryCore(moduloExpression, "%");
        public override bool Visit(SinExpression sinExpression)                     => this.VisitInstrinsicsCore(sinExpression, "sin");
        public override bool Visit(CosExpression cosExpression)                     => this.VisitInstrinsicsCore(cosExpression, "cos");
        public override bool Visit(TanExpression tanExpression)                     => this.VisitInstrinsicsCore(tanExpression, "tan");
        public override bool Visit(LogExpression logExpression)                     => this.VisitInstrinsicsCore(logExpression, "log");
        public override bool Visit(SqrtExpression sqrtExpression)                   => this.VisitInstrinsicsCore(sqrtExpression, "sqrt");
        //---------------------------------------------------------------------
        private bool VisitBinaryCore(BinaryExpression binaryExpression, string cmd)
        {
            bool isFirstBinary = _isFirstBinary;
            _isFirstBinary = false;

            if (!isFirstBinary)
                _writer.Write("(");

            binaryExpression.Left.Accept(this);
            _writer.Write($" {cmd} ");
            binaryExpression.Right.Accept(this);

            if (!isFirstBinary)
                _writer.Write(")");

            return true;
        }
        //---------------------------------------------------------------------
        private bool VisitInstrinsicsCore(IntrinsicExpression intrinsic, string cmd)
        {
            _writer.Write(cmd);
            _writer.Write("(");

            intrinsic.Argument.Accept(this);

            _writer.Write(")");

            return true;
        }
    }
}