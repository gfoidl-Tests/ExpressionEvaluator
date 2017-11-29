using System;
using System.IO;

namespace ExpressionCompiler.Optimizer
{
    static class Program
    {
        static void Main()
        {
            Directory.CreateDirectory("dot");
            Console.WriteLine();

            Run("(3+4)*(7-2)", "0");
            //Run("2a", "1");
            //Run("a^2", "2");
            //Run("sqrt(a^2 + b^2)", "pythagoras");
            //Run("1-2a", "foo");
            //Run("sin(3+x)(a-b)/log(a+1)*(3+x)-2a + e^((x-3)/4)", "bar");
        }
        //---------------------------------------------------------------------
        private static void Run(string expression, string dotName)
        {
            dotName = Path.ChangeExtension(Path.Combine("dot", dotName), "dot");

            using (StreamWriter sw = File.CreateText(dotName))
            {
                var compiler = new DotCompiler(sw);
                compiler.Compile(expression, true);
            }

            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(dotName));
            Console.WriteLine();
        }
    }
}