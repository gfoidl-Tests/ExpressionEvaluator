using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionEvaluator
{
    internal class Parser
    {
        private readonly List<string>      _parameters      = new List<string>();
        private readonly Stack<Expression> _expressionStack = new Stack<Expression>();
        private readonly Stack<Symbol>     _operationStack  = new Stack<Symbol>();
        //---------------------------------------------------------------------
        public (Expression Tree, ParameterExpression Parameter) Parse(string expression)
        {
            _parameters     .Clear();
            _expressionStack.Clear();
            _operationStack .Clear();

            var arrayParameter = Expression.Parameter(typeof(double[]), "args");

            var sr = new StringReader(expression);
            int peek;

            while ((peek = sr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsDigit(next))
                {
                    _expressionStack.Push(this.ReadOperand(sr));
                    continue;
                }

                if (char.IsLetter(next))
                {
                    Expression parameterExpression = this.ReadParameter(sr, arrayParameter);
                    _expressionStack.Push(parameterExpression);
                    continue;
                }

                if (Operation.IsDefined(next))
                {
                    Operation currentOperation = this.ReadOperation(sr);

                    this.EvaluateWhile(() =>
                        _operationStack.Count > 0
                        && _operationStack.Peek() != Paranthesis.Left
                        && currentOperation.Precedence <= (_operationStack.Peek() as Operation).Precedence);

                    _operationStack.Push((Operation)next);
                    continue;
                }

                if (next == '(')
                {
                    sr.Read();
                    _operationStack.Push(Paranthesis.Left);
                    continue;
                }

                if (next == ')')
                {
                    sr.Read();
                    this.EvaluateWhile(() => _operationStack.Count > 0 && _operationStack.Peek() != Paranthesis.Left);
                    _operationStack.Pop();
                    continue;
                }

                if (next == ' ')
                {
                    sr.Read();
                    continue;
                }

                if (next != ' ')
                    throw new ArgumentException("Invalid character encountered", nameof(expression));
            }

            this.EvaluateWhile(() => _operationStack.Count > 0);

            return (_expressionStack.Pop(), arrayParameter);
        }
        //---------------------------------------------------------------------
        private Expression ReadOperand(StringReader sr)
        {
            var sb = new StringBuilder();
            int peek;

            while ((peek = sr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsDigit(next) || next == '.')
                {
                    sr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            return Expression.Constant(double.Parse(sb.ToString()));
        }
        //---------------------------------------------------------------------
        private Operation ReadOperation(StringReader sr)
        {
            char operation = (char)sr.Read();
            return (Operation)operation;
        }
        //---------------------------------------------------------------------
        private Expression ReadParameter(StringReader sr, Expression arrayParameter)
        {
            var sb = new StringBuilder();
            int peek;

            while ((peek = sr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsLetter(next))
                {
                    sr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            string parameter = sb.ToString();

            if (!_parameters.Contains(parameter))
                _parameters.Add(parameter);

            return Expression.ArrayIndex(arrayParameter, Expression.Constant(_parameters.IndexOf(parameter)));
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