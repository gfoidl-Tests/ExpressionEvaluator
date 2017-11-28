using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    [DebuggerDisplay("{Name} | Precedence = {Precedence}")]
    internal abstract class Operation : Token
    {
        private static readonly Dictionary<char, Func<Position, Operation>> _operations;
        //---------------------------------------------------------------------
        public static readonly Func<Position, Operation> Addition       = p => new Addition(p);
        public static readonly Func<Position, Operation> Subtraction    = p => new Subtraction(p);
        public static readonly Func<Position, Operation> Multiplication = p => new Multiplication(p);
        public static readonly Func<Position, Operation> Division       = p => new Division(p);
        public static readonly Func<Position, Operation> Exponentation  = p => new Exponentation(p);
        public static readonly Func<Position, Operation> Modulo         = p => new Modulo(p);
        //---------------------------------------------------------------------
        public int Precedence { get; }
        //---------------------------------------------------------------------
        static Operation()
        {
            _operations = new Dictionary<char, Func<Position, Operation>>
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
        protected Operation(string name, int precedence, Position position)
            : base(name, position)
            => this.Precedence = precedence;
        //---------------------------------------------------------------------
        public static explicit operator Operation((char Name, Position Position) operation)
        {
            if (_operations.TryGetValue(operation.Name, out Func<Position, Operation> res))
                return res(operation.Position);

            throw new InvalidOperationException($"No Operation defined for {operation.Name}");
        }
        //---------------------------------------------------------------------
        public abstract Expression Apply(Expression left, Expression right);
        //---------------------------------------------------------------------
        public static bool IsDefined(char operation)         => _operations.ContainsKey(operation);
        internal override void Accept(ITokenVisitor visitor) => visitor.Visit(this);
    }
}