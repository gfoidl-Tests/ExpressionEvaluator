using System;
using System.IO;

namespace ExpressionCompiler.TinyStackMachine
{
    static class Program
    {
        static void Main()
        {
            Directory.CreateDirectory("tsm");
            Console.WriteLine();

            Run("(2+3)*5", "simple");
            Run("sin(x)(2+3)*5+log(a)", "vars");
        }
        //---------------------------------------------------------------------
        private static void Run(string expression, string tsmName)
        {
            tsmName = Path.ChangeExtension(Path.Combine("tsm", tsmName), "tsm");

            using (StreamWriter sw = File.CreateText(tsmName))
            using (StreamWriter dw = File.CreateText(Path.ChangeExtension(tsmName, "dbg")))
            {
                var compiler = new TinyStackMachineCompiler(sw, dw);
                compiler.Compile(expression);
            }

            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(tsmName));
            Console.WriteLine();
        }
    }
}