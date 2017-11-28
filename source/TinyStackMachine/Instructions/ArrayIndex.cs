namespace TinyStackMachine.Instructions
{
    internal class ArrayIndex : Instruction
    {
        public ArrayIndex(string command, int lineNo, string line) : base(command, lineNo, line)
        { }
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            this.CheckForProgramRunning(cpu);

            int idx    = (int)cpu.Stack.Pop();
            double val = cpu.Args[idx];

            cpu.Stack.Push(val);
        }
    }
}