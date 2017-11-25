using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    public sealed class Intrinsic : Token
    {
        private readonly Func<Expression, Expression>         _operation;
        private static readonly Dictionary<string, Intrinsic> _intrinsics;
        //---------------------------------------------------------------------
        public static readonly Intrinsic Sinus   = new Intrinsic(Intrinsics.Sin, nameof(Sinus));
        public static readonly Intrinsic Cosinus = new Intrinsic(Intrinsics.Cos, nameof(Cosinus));
        public static readonly Intrinsic Tangens = new Intrinsic(Intrinsics.Tan, nameof(Tangens));
        public static readonly Intrinsic Log     = new Intrinsic(Intrinsics.Log, nameof(Log));
        //---------------------------------------------------------------------
        static Intrinsic()
        {
            _intrinsics = new Dictionary<string, Intrinsic>()
            {
                ["sin"] = Sinus,
                ["cos"] = Cosinus,
                ["tan"] = Tangens,
                ["log"] = Log
            };
        }
        //---------------------------------------------------------------------
        private Intrinsic(Func<Expression, Expression> intrinsic, string name)
            : base(name)
            => _operation = intrinsic;
        //---------------------------------------------------------------------
        public static explicit operator Intrinsic(string intrinsic)
        {
            if (_intrinsics.TryGetValue(intrinsic, out Intrinsic res))
                return res;

            throw new InvalidOperationException($"No intrinsic defined for {intrinsic}");
        }
        //---------------------------------------------------------------------
        public Expression Apply(Expression arg)        => _operation(arg);
        public static bool IsDefined(string intrinsic) => _intrinsics.ContainsKey(intrinsic);
        public override void Accept(IVisitor visitor)  => visitor.Visit(this);
    }
}