﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionEvaluator
{
    public class BasicExpressionEvaluator
    {
        private readonly List<ParameterExpression> _parameters      = new List<ParameterExpression>();
        private readonly Stack<Expression>         _expressionStack = new Stack<Expression>();
        private readonly Stack<Symbol>             _operationStack  = new Stack<Symbol>();
        //---------------------------------------------------------------------
        static BasicExpressionEvaluator()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression, double variable = 0)
        {
            if (string.IsNullOrWhiteSpace(expression)) return 0;

            _parameters     .Clear();
            _expressionStack.Clear();
            _operationStack .Clear();

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
                    ParameterExpression parameterExpression = this.ReadParameter(sr);
                    _parameters.Add(parameterExpression);
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

                if (next=='(')
                {
                    sr.Read();
                    _operationStack.Push(Paranthesis.Left);
                    continue;
                }

                if (next==')')
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

            if (_parameters.Count == 0)
            {
                var compiled = Expression.Lambda<Func<double>>(_expressionStack.Pop()).Compile();
                return compiled();
            }
            else
            {
                var compiled = Expression.Lambda<Func<double, double>>(_expressionStack.Pop(), _parameters).Compile();
                return compiled(variable);
            }
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
        private ParameterExpression ReadParameter(StringReader sr)
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

            return Expression.Parameter(typeof(double), sb.ToString());
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