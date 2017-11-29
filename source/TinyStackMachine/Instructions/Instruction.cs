using System;
using System.Collections.Generic;

namespace TinyStackMachine.Instructions
{
    internal abstract class Instruction
    {
        private static readonly Dictionary<string, Func<int, string, Instruction>> _instructions;
        //---------------------------------------------------------------------
        public string Command { get; }
        public int LineNo     { get; }
        public string Line    { get; }
        //---------------------------------------------------------------------
        public static readonly Func<int, string, Instruction> Start      = (n, s) => new Start(".formula", n, s);
        public static readonly Func<int, string, Instruction> End        = (n, s) => new End(".end", n, s);
        public static readonly Func<int, string, Instruction> Push       = (n, s) => new Push("push", n, s);
        public static readonly Func<int, string, Instruction> ArrayIndex = (n, s) => new ArrayIndex("arr_index", n, s);
        public static readonly Func<int, string, Instruction> Add        = (n, s) => new BinaryInstruction("add", n, s, (a, b) => a + b);
        public static readonly Func<int, string, Instruction> Sub        = (n, s) => new BinaryInstruction("sub", n, s, (a, b) => a - b);
        public static readonly Func<int, string, Instruction> Mul        = (n, s) => new BinaryInstruction("mul", n, s, (a, b) => a * b);
        public static readonly Func<int, string, Instruction> Div        = (n, s) => new BinaryInstruction("div", n, s, (a, b) => a / b);
        public static readonly Func<int, string, Instruction> Exp        = (n, s) => new BinaryInstruction("exp", n, s, Math.Pow);
        public static readonly Func<int, string, Instruction> Mod        = (n, s) => new BinaryInstruction("mod", n, s, (a, b) => a % b);
        public static readonly Func<int, string, Instruction> Sin        = (n, s) => new IntrinsicInstruction("sin", n, s, Math.Sin);
        public static readonly Func<int, string, Instruction> Cos        = (n, s) => new IntrinsicInstruction("cos", n, s, Math.Cos);
        public static readonly Func<int, string, Instruction> Tan        = (n, s) => new IntrinsicInstruction("tan", n, s, Math.Tan);
        public static readonly Func<int, string, Instruction> Log        = (n, s) => new IntrinsicInstruction("log", n, s, Math.Log);
        public static readonly Func<int, string, Instruction> Sqrt       = (n, s) => new IntrinsicInstruction("sqrt", n, s, Math.Sqrt);
        public static readonly Func<int, string, Instruction> Print      = (n, s) => new Print("print", n, s);
        //---------------------------------------------------------------------
        static Instruction()
        {
            _instructions = new Dictionary<string, Func<int, string, Instruction>>()
            {
                [".formula"]  = Start,
                [".end"]      = End,
                ["push"]      = Push,
                ["arr_index"] = ArrayIndex,
                ["add"]       = Add,
                ["sub"]       = Sub,
                ["mul"]       = Mul,
                ["div"]       = Div,
                ["exp"]       = Exp,
                ["mod"]       = Mod,
                ["sin"]       = Sin,
                ["cos"]       = Cos,
                ["tan"]       = Tan,
                ["log"]       = Log,
                ["sqrt"]      = Sqrt,
                ["print"]     = Print
            };
        }
        //---------------------------------------------------------------------
        protected Instruction(string command, int lineNo, string line)
        {
            this.Command = command;
            this.LineNo  = lineNo;
            this.Line    = line;
        }
        //---------------------------------------------------------------------
        public static Instruction Create(string cmd, int lineNo, string line)
        {
            if (_instructions.TryGetValue(cmd, out Func<int, string, Instruction> instruction))
                return instruction(lineNo, line);

            throw new InvalidOperationException($"No instruction defined for {cmd}");
        }
        //---------------------------------------------------------------------
        public abstract void Execute(Cpu cpu);
        //---------------------------------------------------------------------
        protected void CheckForProgramRunning(Cpu cpu)
        {
            if (!cpu.IsProgramRunning)
                throw new InvalidOperationException("Program not started correctly");
        }
    }
}