using System.Globalization;

namespace TinyStackMachine.Instructions
{
    internal class Push : Instruction
    {
        public Push(string command, int lineNo, string line) : base(command, lineNo, line)
        { }
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            this.CheckForProgramRunning(cpu);

            double value = double.Parse(Line.Split(' ')[1], CultureInfo.InvariantCulture);

            cpu.Stack.Push(value);
        }
    }
}