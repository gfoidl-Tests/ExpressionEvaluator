using System.IO;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Emitter.TinyStackMachine
{
    internal class TinyStackMachineEmitter : Emitter<bool>
    {
        private readonly TextWriter _writer;
        //---------------------------------------------------------------------
        public TinyStackMachineEmitter(Expression tree, TextWriter writer)
            : base(tree)
            => _writer = writer;
        //---------------------------------------------------------------------
        public override void Emit()
        {
            _writer.WriteLine(".formula");
            this.Tree.Accept(this);
            _writer.WriteLine("print");
            _writer.WriteLine(".end");
        }
        //---------------------------------------------------------------------
        public override bool Visit(ConstantExpression constant)
        {
            _writer.WriteLine($"push {constant.Value}");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ArrayIndexExpression arrayIndexExpression)
        {
            arrayIndexExpression.Right.Accept(this);
            _writer.WriteLine("arr_index");

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(AddExpression           addExpression)           => this.VisitBinaryCore(addExpression          , "add");
        public override bool Visit(SubtractExpression      subtractExpression)      => this.VisitBinaryCore(subtractExpression     , "sub");
        public override bool Visit(MultiplyExpression      multiplyExpression)      => this.VisitBinaryCore(multiplyExpression     , "mul");
        public override bool Visit(DivideExpression        divideExpression)        => this.VisitBinaryCore(divideExpression       , "div");
        public override bool Visit(ExponentationExpression exponentationExpression) => this.VisitBinaryCore(exponentationExpression, "exp");
        public override bool Visit(ModuloExpression        moduloExpression)        => this.VisitBinaryCore(moduloExpression       , "mod");
        public override bool Visit(SinExpression           sinExpression)           => this.VisitInstrinsicsCore(sinExpression, "sin");
        public override bool Visit(CosExpression           cosExpression)           => this.VisitInstrinsicsCore(cosExpression, "cos");
        public override bool Visit(TanExpression           tanExpression)           => this.VisitInstrinsicsCore(tanExpression, "tan");
        public override bool Visit(LogExpression           logExpression)           => this.VisitInstrinsicsCore(logExpression, "log");
        //---------------------------------------------------------------------
        private bool VisitBinaryCore(BinaryExpression binaryExpression, string cmd)
        {
            binaryExpression.Left.Accept(this);
            binaryExpression.Right.Accept(this);
            _writer.WriteLine(cmd);

            return true;
        }
        //---------------------------------------------------------------------
        private bool VisitInstrinsicsCore(IntrinsicExpression intrinsic, string cmd)
        {
            intrinsic.Argument.Accept(this);
            _writer.WriteLine(cmd);

            return true;
        }
    }
}