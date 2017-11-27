namespace TinyStackMachine
{
    public class VirtualMachine
    {
        private readonly Cpu _cpu;
        //---------------------------------------------------------------------
        public VirtualMachine(IBios bios = null, int stackSize = 1024, params double[] args)
        {
            var stack = new StackMemory(stackSize);
            _cpu      = new Cpu(stack, bios ?? new ConsoleBios(), args);
        }
        //---------------------------------------------------------------------
        public void Execute(string tsmFile)
        {
            var loader       = new Loader(tsmFile);
            var instructions = loader.LoadFormula();

            _cpu.Process(instructions);
        }
    }
}