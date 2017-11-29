using System;
using System.Collections.Generic;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Parser
{
    internal class ParsingVisitor : ITokenVisitor
    {
        private readonly ParameterExpression _arrayParameter  = new ParameterExpression(new ParameterToken(Position.NotDefined, "args"));
        private readonly List<string>        _parameters      = new List<string>();
        private readonly Stack<Expression>   _expressionStack = new Stack<Expression>();
        private readonly Stack<Token>        _operationStack  = new Stack<Token>();
        private readonly Stack<Intrinsic>    _intrinsicStack  = new Stack<Intrinsic>();
        private Token _lastToken;
        //---------------------------------------------------------------------
        public ParsingResult GetExpressionTree()
        {
            this.EvaluateWhile(() => _operationStack.Count > 0);

            return (_expressionStack.Pop(), _parameters);
        }
        //---------------------------------------------------------------------
        public void Visit(ValueToken valueToken)
        {
            var expr = new ConstantExpression(valueToken);
            _expressionStack.Push(expr);

            _lastToken = valueToken;
        }
        //---------------------------------------------------------------------
        public void Visit(Constant constant)
        {
            if (_lastToken is ValueToken)
                this.Visit(Operation.Multiplication(Position.NotDefined));

            this.Visit(constant as ValueToken);
        }
        //---------------------------------------------------------------------
        public void Visit(ParameterToken parameter)
        {
            if (_lastToken is ValueToken)
                this.Visit(Operation.Multiplication(Position.NotDefined));

            if (!_parameters.Contains(parameter.Parameter))
                _parameters.Add(parameter.Parameter);

            var idxExpr = new IndexExpression(_parameters.IndexOf(parameter.Parameter));
            var expr    = new ArrayIndexExpression(parameter, _arrayParameter, idxExpr);

            _expressionStack.Push(expr);

            _lastToken = parameter;
        }
        //---------------------------------------------------------------------
        public void Visit(Operation operation)
        {
            this.EvaluateWhile(() =>
                _operationStack.Count > 0
                && !(_operationStack.Peek() is LeftParanthesis)
                && operation.Precedence <= (_operationStack.Peek() as Operation).Precedence);

            _operationStack.Push(operation);

            _lastToken = operation;
        }
        //---------------------------------------------------------------------
        public void Visit(Intrinsic intrinsic)
        {
            if (_lastToken is ValueToken)
                this.Visit(Operation.Multiplication(Position.NotDefined));

            _intrinsicStack.Push(intrinsic);

            _lastToken = intrinsic;
        }
        //---------------------------------------------------------------------
        public void Visit(LeftParanthesis paranthesis)
        {
            if (_lastToken is RightParanthesis)
                this.Visit(Operation.Multiplication(Position.NotDefined));

            _operationStack.Push(paranthesis);

            _lastToken = paranthesis;
        }
        //---------------------------------------------------------------------
        public void Visit(RightParanthesis paranthesis)
        {
            this.EvaluateWhile(() => _operationStack.Count > 0 && !(_operationStack.Peek() is LeftParanthesis));
            _operationStack.Pop();

            if (_intrinsicStack.Count > 0)
            {
                Intrinsic intrinsic = _intrinsicStack.Pop();
                Expression arg      = _expressionStack.Pop();
                Expression res      = intrinsic.Apply(arg);

                _expressionStack.Push(res);
            }

            _lastToken = paranthesis;
        }
        //---------------------------------------------------------------------
        private void EvaluateWhile(Func<bool> condition)
        {
            while (condition())
            {
                Token token       = _operationStack.Pop();
                Expression result = default;

                if (token is Operation operation)
                {
                    Expression right = _expressionStack.Pop();
                    Expression left  = _expressionStack.Pop();

                    result = operation.Apply(left, right);
                }
                else
                    throw new InvalidOperationException($"Unknown operation '{token.Name}' on operation stack");

                _expressionStack.Push(result);
            }
        }
    }
}