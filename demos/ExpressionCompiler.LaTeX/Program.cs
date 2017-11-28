using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCompiler.LaTeX
{
    static class Program
    {
        private static List<Task> _dowloadTasks = new List<Task>();
        //---------------------------------------------------------------------
        static async Task Main()
        {
            Directory.CreateDirectory("latex");
            Console.WriteLine();

            Run("a*b*c", "vars");
            Run("2+3+4", "foo");
            Run("2*3*5", "dummy");
            Run("(2+3)*(5-1)", "simple");
            Run("sin(x)(2+3)*(5/log(a))", "vars");
            Run("sin(3+x)(a-b)/log(a+1)*(3+x)-5a + e^((x-3)/4)", "bar");

            try
            {
                Console.WriteLine("\nWaiting for downloads to complete...");
                await Task.WhenAll(_dowloadTasks);
            }
            catch
            {
                foreach(var task in _dowloadTasks)
                {
                    try
                    {
                        await task;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                }
            }
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

            _dowloadTasks.Add(Download(latex, latexName));
        }
        //---------------------------------------------------------------------
        private static async Task Download(string latex, string latexName)
        {
            const string urlTemplate = "https://latex.codecogs.com/gif.download?{0}";
            string url               = string.Format(urlTemplate, latex);

            using (var wc = new WebClient())
                await wc.DownloadFileTaskAsync(url, latexName);
        }
    }
}