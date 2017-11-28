using System;
using System.Collections.Generic;

namespace ExpressionCompiler.Tokens
{
    internal sealed class Constant : ValueToken
    {
        private static readonly Dictionary<string, Func<Position, Constant>> _constants;
        //---------------------------------------------------------------------
        public static readonly Func<Position, Constant> Pi = p => new Constant(p, Math.PI);
        public static readonly Func<Position, Constant> E  = p => new Constant(p, Math.E);
        //---------------------------------------------------------------------
        static Constant()
        {
            _constants = new Dictionary<string, Func<Position, Constant>>
            {
                ["pi"] = Pi,
                ["e"]  = E
            };
        }
        //---------------------------------------------------------------------
        private Constant(Position position, double value) : base(position, value) { }
        //---------------------------------------------------------------------
        public static explicit operator Constant((string Name, Position Position) constant)
        {
            if (_constants.TryGetValue(constant.Name, out Func<Position, Constant> res))
                return res(constant.Position);

            throw new InvalidOperationException($"No constant defined for {constant}");
        }
        //---------------------------------------------------------------------
        public static bool IsDefined(string constant)        => _constants.ContainsKey(constant);
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}