using System;
using System.Collections.Generic;
using System.Text;
using TinyStackMachine.Instructions;

namespace TinyStackMachine
{
    internal class Cpu
    {
        public IBios Bios            { get; }
        public StackMemory Stack     { get; }
        public double[] Args         { get; }
        public bool IsProgramRunning { get; set; }
        //---------------------------------------------------------------------
        public Cpu(StackMemory stackMemory, IBios bios, double[] args)
        {
            this.Stack = stackMemory;
            this.Bios  = bios;
            this.Args  = args;
        }
        //---------------------------------------------------------------------
        public void Process(IEnumerable<Instruction> instructions)
        {
            foreach (Instruction instruction in instructions)
                instruction.Execute(this);
        }
    }
}