using System;
using System.Collections.Generic;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler
{
    internal class ParsingResult
    {
        public Expression Tree                        { get; }
        public ParameterExpression ArrayParameter     { get; }
        public IReadOnlyCollection<string> Parameters { get; }
        //---------------------------------------------------------------------
        public ParsingResult(Expression expression, ParameterExpression paramExpr, IReadOnlyCollection<string> parameters)
        {
            this.Tree           = expression;
            this.ArrayParameter = paramExpr;
            this.Parameters     = parameters;
        }
        //---------------------------------------------------------------------
        public static implicit operator ParsingResult((Expression, ParameterExpression, List<string>) tmp)
            => new ParsingResult(tmp.Item1, tmp.Item2, tmp.Item3);
    }
}