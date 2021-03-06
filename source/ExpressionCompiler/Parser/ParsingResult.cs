﻿using System.Collections.Generic;
using ExpressionCompiler.Expressions;

namespace ExpressionCompiler.Parser
{
    internal class ParsingResult
    {
        public Expression Tree                        { get; }
        public IReadOnlyCollection<string> Parameters { get; }
        //---------------------------------------------------------------------
        public ParsingResult(Expression expression, IReadOnlyCollection<string> parameters)
        {
            this.Tree       = expression;
            this.Parameters = parameters;
        }
        //---------------------------------------------------------------------
        public static implicit operator ParsingResult((Expression, List<string>) tmp)
            => new ParsingResult(tmp.Item1, tmp.Item2);
    }
}