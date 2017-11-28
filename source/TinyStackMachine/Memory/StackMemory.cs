using System;
using System.Collections;
using System.Collections.Generic;

namespace TinyStackMachine
{
    internal sealed class StackMemory : Memory, IEnumerable<double>
    {
        private readonly Stack<double> _stack = new Stack<double>();
        private readonly int           _stackSize;
        //---------------------------------------------------------------------
        public StackMemory(int stackSize = 1024) => _stackSize = stackSize;
        //---------------------------------------------------------------------
        public void Push(double value)
        {
            if (_stack.Count > _stackSize - 1) throw new StackOverflowException($"Stack-space of {_stackSize} is used");

            _stack.Push(value);
        }
        //---------------------------------------------------------------------
        public double Pop() => _stack.Pop();
        public int Count    => _stack.Count;
        //---------------------------------------------------------------------
        public IEnumerator<double> GetEnumerator() => _stack.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()    => _stack.GetEnumerator();
    }
}