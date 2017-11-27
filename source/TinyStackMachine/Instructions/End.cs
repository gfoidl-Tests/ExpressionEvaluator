using System;

namespace TinyStackMachine.Instructions
{
    internal class End : Instruction
    {
        public End(string command, int lineNo, string line) : base(command, lineNo, line)
        { }
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            cpu.IsProgramRunning = false;

            if (cpu.Stack.Count > 0)
                throw new InvalidOperationException("No all instructions processed");
        }
    }
}