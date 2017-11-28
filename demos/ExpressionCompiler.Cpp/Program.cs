using System;
using System.IO;

namespace ExpressionCompiler.Cpp
{
    static class Program
    {
        static void Main()
        {
            Directory.CreateDirectory("cpp");
            Console.WriteLine();

            Run("(2+3)*5", "simple");
            Run("sin(x)(2+3)*5+log(a)", "vars");
        }
        //---------------------------------------------------------------------
        private static void Run(string expression, string cppName)
        {
            cppName = Path.ChangeExtension(Path.Combine("cpp", cppName), "cpp");

            using (StreamWriter sw = File.CreateText(cppName))
            {
                var compiler = new CppCompiler(sw);
                compiler.Compile(expression);
            }

            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(cppName));
            Console.WriteLine();
        }
    }
}