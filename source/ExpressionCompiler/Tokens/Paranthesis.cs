using System;

namespace ExpressionCompiler.Tokens
{
    public abstract class Paranthesis : Token
    {
        public static readonly Func<int, LeftParanthesis> Left   = p => new LeftParanthesis(p);
        public static readonly Func<int, RightParanthesis> Right = p => new RightParanthesis(p);
        //---------------------------------------------------------------------
        protected Paranthesis(string name, int position) : base(name, position) { }
    }
}