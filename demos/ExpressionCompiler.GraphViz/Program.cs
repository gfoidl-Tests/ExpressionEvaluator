using System;
using System.IO;

namespace ExpressionCompiler.GraphViz
{
    static class Program
    {
        static void Main()
        {
            Directory.CreateDirectory("dot");
            Console.WriteLine();

            Run("2+3+4", "add1");
            Run("2*3*5", "mul");
            Run("a*b*c", "mul-var");
            Run("(2+3)*(5-1)", "simple");
            Run("sin(x)(2+3)*(5/log(a))", "vars");
            Run("sin(3+x)(a-b)/log(a+1)*(3+x)-5a + e^((x-3)/4)", "bar");
        }
        //---------------------------------------------------------------------
        private static void Run(string expression, string dotName)
        {
            dotName = Path.ChangeExtension(Path.Combine("dot", dotName), "dot");

            using (StreamWriter sw = File.CreateText(dotName))
            {
                var compiler = new DotCompiler(sw);
                compiler.Compile(expression);
            }

            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(dotName));
            Console.WriteLine();
        }
    }
}