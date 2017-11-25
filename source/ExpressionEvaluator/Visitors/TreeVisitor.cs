using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator.Visitors
{
    internal class TreeVisitor : IVisitor
    {
        private readonly ParameterExpression _arrayParameter;
        private readonly List<string>        _parameters      = new List<string>();
        private readonly Stack<Expression>   _expressionStack = new Stack<Expression>();
        private readonly Stack<Token>        _operationStack  = new Stack<Token>();
        //---------------------------------------------------------------------
        public TreeVisitor(ParameterExpression arrayParameter) => _arrayParameter = arrayParameter;
        //---------------------------------------------------------------------
        public Expression GetExpressionTree()
        {
            this.EvaluateWhile(() => _operationStack.Count > 0);
            return _expressionStack.Pop();
        }
        //---------------------------------------------------------------------
        public void Visit(ValueToken valueToken)
        {
            var expr = Expression.Constant(valueToken.Value);
            _expressionStack.Push(expr);
        }
        //---------------------------------------------------------------------
        public void Visit(ParameterToken parameter)
        {
            if (!_parameters.Contains(parameter.Parameter))
                _parameters.Add(parameter.Parameter);

            var idxExpr = Expression.Constant(_parameters.IndexOf(parameter.Parameter));
            var expr    = Expression.ArrayIndex(_arrayParameter, idxExpr);

            _expressionStack.Push(expr);
        }
        //---------------------------------------------------------------------
        public void Visit(Operation operation)
        {
            this.EvaluateWhile(() =>
                _operationStack.Count > 0
                && _operationStack.Peek() != Paranthesis.Left
                && operation.Precedence <= (_operationStack.Peek() as Operation).Precedence);

            _operationStack.Push(operation);
        }
        //---------------------------------------------------------------------
        public void Visit(Paranthesis paranthesis)
        {
            if (paranthesis == Paranthesis.Left)
                _operationStack.Push(paranthesis);
            else
            {
                this.EvaluateWhile(() => _operationStack.Count > 0 && _operationStack.Peek() != Paranthesis.Left);
                _operationStack.Pop();
            }
        }
        //---------------------------------------------------------------------
        private void EvaluateWhile(Func<bool> condition)
        {
            while (condition())
            {
                Expression right = _expressionStack.Pop();
                Expression left  = _expressionStack.Pop();

                Operation operation = _operationStack.Pop() as Operation;
                Expression result   = operation.Apply(left, right);

                _expressionStack.Push(result);
            }
        }
    }
}