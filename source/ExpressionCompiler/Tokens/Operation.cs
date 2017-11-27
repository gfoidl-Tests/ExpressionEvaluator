using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler.Tokens
{
    [DebuggerDisplay("{Name} | Precedence = {Precedence}")]
    internal sealed class Operation : Token
    {
        private readonly Func<Expression, Expression, Expression>      _operation;
        private static readonly Dictionary<char, Func<int, Operation>> _operations;
        //---------------------------------------------------------------------
        public static readonly Func<int, Operation> Addition       = p => new Operation(p, Expression.Add     , 1, nameof(Addition));
        public static readonly Func<int, Operation> Subtraction    = p => new Operation(p, Expression.Subtract, 1, nameof(Subtraction));
        public static readonly Func<int, Operation> Multiplication = p => new Operation(p, Expression.Multiply, 2, nameof(Multiplication));
        public static readonly Func<int, Operation> Division       = p => new Operation(p, Expression.Divide  , 2, nameof(Division));
        public static readonly Func<int, Operation> Exponentation  = p => new Operation(p, Expression.Power   , 3, nameof(Exponentation));
        public static readonly Func<int, Operation> Modulo         = p => new Operation(p, Expression.Modulo  , 2, nameof(Modulo));
        //---------------------------------------------------------------------
        public int Precedence { get; }
        //---------------------------------------------------------------------
        static Operation()
        {
            _operations = new Dictionary<char, Func<int, Operation>>
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
        private Operation(int position, Func<Expression, Expression, Expression> operation, int precedence, string name)
            : base(name, position)
        {
            this.Precedence = precedence;
            _operation      = operation;
        }
        //---------------------------------------------------------------------
        public static explicit operator Operation((char Name, int Position) operation)
        {
            if (_operations.TryGetValue(operation.Name, out Func<int, Operation> res))
                return res(operation.Position);

            throw new InvalidOperationException($"No Operation defined for {operation.Name}");
        }
        //---------------------------------------------------------------------
        public Expression Apply(Expression left, Expression right) => _operation(left, right);
        public static bool IsDefined(char operation)               => _operations.ContainsKey(operation);
        public override void Accept(ITokenVisitor visitor)             => visitor.Visit(this);
    }
}