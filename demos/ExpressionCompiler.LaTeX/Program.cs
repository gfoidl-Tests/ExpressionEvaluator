using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExpressionCompiler.LaTeX
{
    static class Program
    {
        static void Main()
        {
            Directory.CreateDirectory("latex");
            Console.WriteLine();

            Run("(2+3)*(5-1)", "simple");
            Run("sin(x)(2+3)*(5/log(a))", "vars");
        }
        //---------------------------------------------------------------------
        private static void Run(string expression, string latexName)
        {
            latexName = Path.ChangeExtension(Path.Combine("latex", latexName), "gif");
            var sb    = new StringBuilder();

            var compiler = new LaTeXCompiler(sb);
            compiler.Compile(expression);

            string latex = sb.ToString();

            Console.WriteLine();
            Console.WriteLine(latex);
            Console.WriteLine();

            Download(latex, latexName);
        }
        //---------------------------------------------------------------------
        private static void Download(string latex, string latexName)
        {
            const string urlTemplate = "https://latex.codecogs.com/gif.download?{0}";
            string url               = string.Format(urlTemplate, latex);

            using (var wc = new WebClient())
                wc.DownloadFile(url, latexName);
        }
    }
}