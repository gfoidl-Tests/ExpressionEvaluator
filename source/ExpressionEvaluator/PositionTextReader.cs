using System.Diagnostics;
using System.IO;

namespace ExpressionEvaluator
{
    [DebuggerNonUserCode]
    public class PositionTextReader
    {
        private readonly TextReader _tr;
        private int _position;
        //---------------------------------------------------------------------
        public int Position => _position;
        //---------------------------------------------------------------------
        public PositionTextReader(TextReader tr) => _tr = tr;
        //---------------------------------------------------------------------
        // int -> char can't have -1 => therfore int
        public int Peek() => _tr.Peek();
        //---------------------------------------------------------------------
        public int Read()
        {
            _position++;
            return (char)_tr.Read();
        }
    }
}