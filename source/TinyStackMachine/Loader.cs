using System.Collections.Generic;
using System.IO;
using TinyStackMachine.Instructions;

namespace TinyStackMachine
{
    internal class Loader
    {
        private readonly string _tsmFile;
        //---------------------------------------------------------------------
        public Loader(string tsmFile) => _tsmFile = tsmFile;
        //---------------------------------------------------------------------
        public IEnumerable<Instruction> LoadFormula()
        {
            int lineNo = 0;

            using (StreamReader sr = File.OpenText(_tsmFile))
                while (!sr.EndOfStream)
                {
                    lineNo++;
                    string line   = sr.ReadLine();
                    string[] cmds = line.Split(' ');

                    yield return Instruction.Create(cmds[0], lineNo, line);
                }
        }
    }
}