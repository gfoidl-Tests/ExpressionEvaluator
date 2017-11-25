using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator.Tokens
{
    [DebuggerDisplay("{Name}")]
    internal sealed class Operation : Token
    {
        private readonly Func<Expression, Expression, Expression> _operation;
        private static readonly Dictionary<char, Operation>       _operations;
        //---------------------------------------------------------------------
        public static readonly Operation Addition       = new Operation(Expression.Add     , 1, nameof(Addition));
        public static readonly Operation Subtraction    = new Operation(Expression.Subtract, 1, nameof(Subtraction));
        public static readonly Operation Multiplication = new Operation(Expression.Multiply, 2, nameof(Multiplication));
        public static readonly Operation Division       = new Operation(Expression.Divide  , 2, nameof(Division));
        public static readonly Operation Exponentation  = new Operation(Expression.Power   , 3, nameof(Exponentation));
        public static readonly Operation Modulo         = new Operation(Expression.Modulo  , 2, nameof(Modulo));
        //---------------------------------------------------------------------
        public int Precedence { get; }
        //---------------------------------------------------------------------
        static Operation()
        {
            _operations = new Dictionary<char, Operation>
            {
                ['+'] = Addition,
                ['-'] = Subtraction,
                ['*'] = Multiplication,
                ['/'] = Division,
                ['^'] = Exponentation,
                ['%'] = Modulo
            };
        }
        //---------------------------------------------------------------------
        private Operation(Func<Expression, Expression, Expression> operation, int precedence, string name)
            : base(name)
        {
            this.Precedence = precedence;
            _operation      = operation;
        }
        //---------------------------------------------------------------------
        public static explicit operator Operation(char operation)
        {
            if (_operations.TryGetValue(operation, out Operation res))
                return res;

            throw new InvalidOperationException($"No Operation defined for {operation}");
        }
        //---------------------------------------------------------------------
        public Expression Apply(Expression left, Expression right) => _operation(left, right);
        //---------------------------------------------------------------------
        public static bool IsDefined(char operation) => _operations.ContainsKey(operation);
        //---------------------------------------------------------------------
        public override void Accept(IVisitor visitor) => visitor.Visit(this);
    }
}