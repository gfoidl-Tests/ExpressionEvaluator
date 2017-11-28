using System;
using System.Collections.Generic;
using System.IO;
using ExpressionCompiler.Tokens;
using TinyStackMachine.Instructions;

namespace TinyStackMachine
{
    public class Debugger
    {
        private readonly VirtualMachine   _vm;
        private bool                      _autoHalt;
        private string                    _expression;
        private Dictionary<int, Position> _symbolMap;
        //---------------------------------------------------------------------
        public Debugger(VirtualMachine vm) => _vm = vm;
        //---------------------------------------------------------------------
        public void Debug(string tsmFile, bool autoHalt = true)
        {
            this.ReadDebugInfos(tsmFile);

            _autoHalt = autoHalt;

            _vm.Cpu.InstructionProcessed += this.Cpu_InstructionProcessed;
            _vm.Execute(tsmFile);
            _vm.Cpu.InstructionProcessed -= this.Cpu_InstructionProcessed;
        }
        //---------------------------------------------------------------------
        private void Cpu_InstructionProcessed(Instruction instruction)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Instruction: {instruction.Command}");
            Console.WriteLine();

            if (_symbolMap.TryGetValue(instruction.LineNo, out Position tokenPos) && tokenPos != Position.NotDefined)
            {
                for (int i = 0; i < tokenPos.Start - 1; ++i)
                    Console.Write(_expression[i]);

                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = tokenPos.Start; i <= tokenPos.End; ++i)
                    Console.Write(_expression[i - 1]);
                Console.ResetColor();

                for (int i = tokenPos.End; i < _expression.Length; ++i)
                    Console.Write(_expression[i]);

                Console.WriteLine();
                Console.WriteLine();

                this.PrintCpuStack();
            }

            if (_autoHalt)
            {
                Console.WriteLine("\nHalt...any key to continue...");
                Console.ReadKey();
            }
        }
        //---------------------------------------------------------------------
        private void PrintCpuStack()
        {
            Console.WriteLine("Stack:");

            foreach (var item in _vm.Cpu.Stack)
                Console.WriteLine(item);
        }
        //---------------------------------------------------------------------
        private void ReadDebugInfos(string tsmFile)
        {
            string dbgFile = Path.ChangeExtension(tsmFile, "dbg");

            using (StreamReader sr = File.OpenText(dbgFile))
            {
                _expression = sr.ReadLine();
                _symbolMap  = new Dictionary<int, Position>();

                while (!sr.EndOfStream)
                {
                    string[] cols = sr.ReadLine().Split(' ');
                    int line      = int.Parse(cols[0]);

                    Position tokenPos = (int.Parse(cols[1]), int.Parse(cols[2]));

                    _symbolMap.Add(line, tokenPos);
                }
            }
        }
    }
}