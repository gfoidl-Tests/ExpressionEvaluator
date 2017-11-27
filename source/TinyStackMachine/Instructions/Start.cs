using System;

namespace TinyStackMachine.Instructions
{
    internal class Start : Instruction
    {
        public Start(string command, int lineNo, string line) : base(command, lineNo, line)
        { }
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            if (cpu.Stack.Count > 0)
                throw new InvalidOperationException("Program not started correctly");

            cpu.IsProgramRunning = true;
        }
    }
}