using System;
using System.Collections.Generic;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public sealed class Constant : ValueToken
    {
        private static readonly Dictionary<string, Constant> _constants;
        //---------------------------------------------------------------------
        public static readonly Constant Pi = new Constant(Math.PI);
        public static readonly Constant E  = new Constant(Math.E);
        //---------------------------------------------------------------------
        static Constant()
        {
            _constants = new Dictionary<string, Constant>
            {
                ["pi"] = Pi,
                ["e"]  = E
            };
        }
        //---------------------------------------------------------------------
        private Constant(double value) : base(value) { }
        //---------------------------------------------------------------------
        public static explicit operator Constant(string constant)
        {
            if (_constants.TryGetValue(constant, out Constant res))
                return res;

            throw new InvalidOperationException($"No constant defined for {constant}");
        }
        //---------------------------------------------------------------------
        public static bool IsDefined(string constant) => _constants.ContainsKey(constant);
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}