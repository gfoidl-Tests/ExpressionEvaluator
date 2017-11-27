using System;
using System.Collections.Generic;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal sealed class Constant : ValueToken
    {
        private static readonly Dictionary<string, Func<int, Constant>> _constants;
        //---------------------------------------------------------------------
        public static readonly Func<int, Constant> Pi = p => new Constant(p, Math.PI);
        public static readonly Func<int, Constant> E  = p => new Constant(p, Math.E);
        //---------------------------------------------------------------------
        static Constant()
        {
            _constants = new Dictionary<string, Func<int, Constant>>
            {
                ["pi"] = Pi,
                ["e"]  = E
            };
        }
        //---------------------------------------------------------------------
        private Constant(int position, double value) : base(position, value) { }
        //---------------------------------------------------------------------
        public static explicit operator Constant((string Name, int Position) constant)
        {
            if (_constants.TryGetValue(constant.Name, out Func<int, Constant> res))
                return res(constant.Position);

            throw new InvalidOperationException($"No constant defined for {constant}");
        }
        //---------------------------------------------------------------------
        public static bool IsDefined(string constant) => _constants.ContainsKey(constant);
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}