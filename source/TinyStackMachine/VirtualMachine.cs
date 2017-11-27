namespace TinyStackMachine
{
    public class VirtualMachine
    {
        internal Cpu Cpu { get; }
        //---------------------------------------------------------------------
        public VirtualMachine(IBios bios = null, int stackSize = 1024, params double[] args)
        {
            var stack = new StackMemory(stackSize);
            this.Cpu  = new Cpu(stack, bios ?? new ConsoleBios(), args);
        }
        //---------------------------------------------------------------------
        public void Execute(string tsmFile)
        {
            var loader       = new Loader(tsmFile);
            var instructions = loader.LoadFormula();

            this.Cpu.Process(instructions);
        }
    }
}