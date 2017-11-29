using System.IO;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Emitter.TinyStackMachine
{
    internal class TinyStackMachineEmitter : Emitter<bool>
    {
        private readonly TextWriter _writer;
        private readonly TextWriter _debugInfoWriter;
        private int _emittedLine;
        //---------------------------------------------------------------------
        public TinyStackMachineEmitter(Expression tree, TextWriter writer, TextWriter debugInfoWriter)
            : base(tree)
        {
            _writer          = writer;
            _debugInfoWriter = debugInfoWriter;
        }
        //---------------------------------------------------------------------
        public override void Emit()
        {
            _writer.WriteLine(".formula");
            _emittedLine++;

            this.Tree.Accept(this);

            _writer.WriteLine("print");
            _emittedLine++;

            _writer.WriteLine(".end");
            _emittedLine++;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ConstantExpression constant)     => this.VisitConstant(constant);
        public override bool Visit(IndexExpression indexExpression) => this.VisitConstant(indexExpression);
        //---------------------------------------------------------------------
        private bool VisitConstant<T>(ConstantExpression<T> constant) where T : struct
        {
            _writer.WriteLine($"push {constant.Value}");
            _emittedLine++;

            this.EmitDebugInfo(constant);

            return true;
        }
        //---------------------------------------------------------------------
        public override bool Visit(ArrayIndexExpression arrayIndexExpression)
        {
            arrayIndexExpression.Right.Accept(this);
            _writer.WriteLine("arr_index");
            _emittedLine++;

            this.EmitDebugInfo(arrayIndexExpression);

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
        public override bool Visit(SqrtExpression          sqrtExpression)          => this.VisitInstrinsicsCore(sqrtExpression, "sqrt");
        //---------------------------------------------------------------------
        private bool VisitBinaryCore(BinaryExpression binaryExpression, string cmd)
        {
            binaryExpression.Left.Accept(this);
            binaryExpression.Right.Accept(this);
            _writer.WriteLine(cmd);
            _emittedLine++;

            this.EmitDebugInfo(binaryExpression);

            return true;
        }
        //---------------------------------------------------------------------
        private bool VisitInstrinsicsCore(IntrinsicExpression intrinsic, string cmd)
        {
            intrinsic.Argument.Accept(this);
            _writer.WriteLine(cmd);
            _emittedLine++;

            this.EmitDebugInfo(intrinsic);

            return true;
        }
        //---------------------------------------------------------------------
        private void EmitDebugInfo(Expression expression)
        {
            if (_debugInfoWriter == null) return;

            Position position = expression.Token.Position;

            _debugInfoWriter.WriteLine($"{_emittedLine} {position.Start} {position.End}");
        }
    }
}