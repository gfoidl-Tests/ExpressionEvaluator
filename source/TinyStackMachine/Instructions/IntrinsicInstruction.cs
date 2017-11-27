using System;

namespace TinyStackMachine.Instructions
{
    internal class IntrinsicInstruction : Instruction
    {
        private readonly Func<double, double> _func;
        //---------------------------------------------------------------------
        public IntrinsicInstruction(string command, int lineNo, string line, Func<double, double> func)
            : base(command, lineNo, line)
            => _func = func;
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            this.CheckForProgramRunning(cpu);

            double arg = cpu.Stack.Pop();
            double res = _func(arg);

            cpu.Stack.Push(res);
        }
    }
}