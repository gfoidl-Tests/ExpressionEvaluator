using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    internal sealed class Intrinsic : Token
    {
        private readonly Func<Expression, Expression>                    _operation;
        private static readonly Dictionary<string, Func<int, Intrinsic>> _intrinsics;
        //---------------------------------------------------------------------
        public static readonly Func<int, Intrinsic> Sinus   = p => new Intrinsic(p, Intrinsics.Sin, nameof(Sinus));
        public static readonly Func<int, Intrinsic> Cosinus = p => new Intrinsic(p, Intrinsics.Cos, nameof(Cosinus));
        public static readonly Func<int, Intrinsic> Tangens = p => new Intrinsic(p, Intrinsics.Tan, nameof(Tangens));
        public static readonly Func<int, Intrinsic> Log     = p => new Intrinsic(p, Intrinsics.Log, nameof(Log));
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
        private Intrinsic(int position, Func<Expression, Expression> intrinsic, string name)
            : base(name, position)
            => _operation = intrinsic;
        //---------------------------------------------------------------------
        public static explicit operator Intrinsic((string Name, int Position) intrinsic)
        {
            if (_intrinsics.TryGetValue(intrinsic.Name, out Func<int, Intrinsic> res))
                return res(intrinsic.Position);

            throw new InvalidOperationException($"No intrinsic defined for {intrinsic}");
        }
        //---------------------------------------------------------------------
        public Expression Apply(Expression arg)        => _operation(arg);
        public static bool IsDefined(string intrinsic) => _intrinsics.ContainsKey(intrinsic);
        public override void Accept(IVisitor visitor)  => visitor.Visit(this);
    }
}