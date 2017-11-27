using System;

namespace TinyStackMachine
{
    internal class ConsoleBios : IBios
    {
        public void Print    (string message) => Console.Write(message);
        public void PrintLine(string message) => Console.WriteLine(message);
    }
}