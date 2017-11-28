namespace TinyStackMachine.Instructions
{
    internal class Print : Instruction
    {
        public Print(string command, int lineNo, string line) : base(command, lineNo, line)
        { }
        //---------------------------------------------------------------------
        public override void Execute(Cpu cpu)
        {
            double res = cpu.Stack.Pop();

            cpu.Bios.PrintLine($"Execution result: {res}");
        }
    }
}