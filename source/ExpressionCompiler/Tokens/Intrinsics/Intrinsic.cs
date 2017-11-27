using System;
using System.Collections.Generic;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal abstract class Intrinsic : Token
    {
        private static readonly Dictionary<string, Func<int, Intrinsic>> _intrinsics;
        //---------------------------------------------------------------------
        public static readonly Func<int, Intrinsic> Sinus   = p => new Sinus(p);
        public static readonly Func<int, Intrinsic> Cosinus = p => new Cosinus(p);
        public static readonly Func<int, Intrinsic> Tangens = p => new Tangens(p);
        public static readonly Func<int, Intrinsic> Log     = p => new Log(p);
        //---------------------------------------------------------------------
        static Intrinsic()
        {
            _intrinsics = new Dictionary<string, Func<int, Intrinsic>>
            {
                ["sin"] = Sinus,
                ["cos"] = Cosinus,
                ["tan"] = Tangens,
                ["log"] = Log
            };
        }
        //---------------------------------------------------------------------
        protected Intrinsic(string name, int position)
            : base(name, position)
        { }
        //---------------------------------------------------------------------
        public static explicit operator Intrinsic((string Name, int Position) intrinsic)
        {
            if (_intrinsics.TryGetValue(intrinsic.Name, out Func<int, Intrinsic> res))
                return res(intrinsic.Position);

            throw new InvalidOperationException($"No intrinsic defined for {intrinsic}");
        }
        //---------------------------------------------------------------------
        public abstract Expression Apply(Expression arg);
        //---------------------------------------------------------------------
        public static bool IsDefined(string intrinsic)       => _intrinsics.ContainsKey(intrinsic);
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}