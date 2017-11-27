using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionCompiler
{
    public class ParsingResult
    {
        private Func<double[], double> _compiled;
        //---------------------------------------------------------------------
        public Expression Tree                        { get; }
        public ParameterExpression ArrayParameter     { get; }
        public IReadOnlyCollection<string> Parameters { get; }
        //---------------------------------------------------------------------
        public Func<double[], double> Delegate
        {
            get
            {
                if (_compiled == null) this.Compile();

                return _compiled;
            }
        }
        //---------------------------------------------------------------------
        public ParsingResult(Expression expression, ParameterExpression paramExpr, IReadOnlyCollection<string> parameters)
        {
            this.Tree           = expression;
            this.ArrayParameter = paramExpr;
            this.Parameters     = parameters;
        }
        //---------------------------------------------------------------------
        public static implicit operator ParsingResult((Expression, ParameterExpression, List<string>) tmp) => 
            new ParsingResult(tmp.Item1, tmp.Item2, tmp.Item3);
        //---------------------------------------------------------------------
        private void Compile()
        {
            if (this.Tree == null)
                throw new InvalidOperationException($"{nameof(this.Tree)} is null, parse results first");

            var lambda = Expression.Lambda<Func<double[], double>>(this.Tree, this.ArrayParameter);
            _compiled  = lambda.Compile();
        }
    }
}