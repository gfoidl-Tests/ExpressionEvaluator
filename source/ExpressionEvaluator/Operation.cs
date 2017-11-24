using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ExpressionEvaluator
{
    [DebuggerDisplay("{Name}")]
    internal sealed class Operation
    {
        private readonly Func<Expression, Expression, Expression> _operation;
        private static readonly Dictionary<char, Operation>       _operations;
        //---------------------------------------------------------------------
        public static readonly Operation Addition       = new Operation(Expression.Add     , 1, nameof(Addition));
        public static readonly Operation Subtraction    = new Operation(Expression.Subtract, 1, nameof(Subtraction));
        public static readonly Operation Multiplication = new Operation(Expression.Multiply, 2, nameof(Multiplication));
        public static readonly Operation Division       = new Operation(Expression.Divide  , 2, nameof(Division));
        //---------------------------------------------------------------------
        public string Name    { get; }
        public int Precedence { get; }
        //---------------------------------------------------------------------
        static Operation()
        {
            _operations = new Dictionary<char, Operation>
            {
                ['+'] = Addition,
                ['-'] = Subtraction,
                ['*'] = Multiplication,
                ['/'] = Division
            };
        }
        //---------------------------------------------------------------------
        private Operation(Func<Expression, Expression, Expression> operation, int precedence, string name)
        {
            this.Name       = name;
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
    }
}