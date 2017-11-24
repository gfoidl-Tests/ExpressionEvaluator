using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator
{
    internal class Parser
    {
        private readonly List<string>      _parameters      = new List<string>();
        private readonly Stack<Expression> _expressionStack = new Stack<Expression>();
        private readonly Stack<Token>      _operationStack  = new Stack<Token>();
        //---------------------------------------------------------------------
        public (Expression Tree, ParameterExpression Parameter) Parse(IEnumerable<Token> tokens)
        {
            _parameters     .Clear();
            _expressionStack.Clear();
            _operationStack .Clear();

            var arrayParameter = Expression.Parameter(typeof(double[]), "args");

            foreach (Token token in tokens)
            {
                if (token is ValueToken value)
                {
                    var expr = Expression.Constant(value.Value);
                    _expressionStack.Push(expr);
                }
                else if (token is ParameterToken parameter)
                {
                    if (!_parameters.Contains(parameter.Parameter))
                        _parameters.Add(parameter.Parameter);

                    var expr = Expression.ArrayIndex(arrayParameter, Expression.Constant(_parameters.IndexOf(parameter.Parameter)));
                    _expressionStack.Push(expr);
                }
                else if (token is Operation operation)
                {
                    this.EvaluateWhile(() =>
                        _operationStack.Count > 0
                        && _operationStack.Peek() != Paranthesis.Left
                        && operation.Precedence <= (_operationStack.Peek() as Operation).Precedence);

                    _operationStack.Push(operation);
                }
                else if (token is Paranthesis paranthesis)
                {
                    if (paranthesis == Paranthesis.Left)
                        _operationStack.Push(paranthesis);
                    else
                    {
                        this.EvaluateWhile(() => _operationStack.Count > 0 && _operationStack.Peek() != Paranthesis.Left);
                        _operationStack.Pop();
                    }
                }
            }

            this.EvaluateWhile(() => _operationStack.Count > 0);

            return (_expressionStack.Pop(), arrayParameter);
        }
        //---------------------------------------------------------------------
        private void EvaluateWhile(Func<bool> condition)
        {
            while (condition())
            {
                Expression right = _expressionStack.Pop();
                Expression left  = _expressionStack.Pop();

                _expressionStack.Push((_operationStack.Pop() as Operation).Apply(left, right));
            }
        }
    }
}