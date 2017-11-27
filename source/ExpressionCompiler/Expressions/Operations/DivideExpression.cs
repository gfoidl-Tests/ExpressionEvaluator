﻿using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Expressions
{
    internal class DivideExpression : BinaryExpression
    {
        public DivideExpression(Operation token, Expression left, Expression right)
            : base(token, left, right)
        { }
        //---------------------------------------------------------------------
        internal override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}