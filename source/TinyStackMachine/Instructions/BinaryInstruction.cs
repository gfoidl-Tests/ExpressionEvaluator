using System;

namespace TinyStackMachine.Instructions
{
    internal class BinaryInstruction : Instruction
    {
        private readonly Func<double, double, double> _func;
        //---------------------------------------------------------------------
        public BinaryInstruction(string command, int lineNo, string line, Func<double, double, double> func)
            : base(command, lineNo, line)
            => _func = func;
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            this.CheckForProgramRunning(cpu);

            double right = cpu.Stack.Pop();
            double left  = cpu.Stack.Pop();
            double res   = _func(left, right);

            cpu.Stack.Push(res);
        }
    }
}