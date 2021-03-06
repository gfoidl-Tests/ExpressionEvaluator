﻿using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Tokens
{
    internal class Addition : Operation
    {
        public Addition(Position position) : base("Add", 1, position) { }
        //---------------------------------------------------------------------
        public override Expression Apply(Expression left, Expression right) => new AddExpression(this, left, right);
    }
}