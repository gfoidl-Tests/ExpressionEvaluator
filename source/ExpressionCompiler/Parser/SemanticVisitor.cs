using System.Collections.Generic;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Parser
{
    internal class SemanticVisitor : ITokenVisitor
    {
        private Token _lastToken;
        private readonly Stack<Paranthesis> _paranthesisStack = new Stack<Paranthesis>();
        //---------------------------------------------------------------------
        public void Validate()
        {
            if (_paranthesisStack.Count > 0)
            {
                Paranthesis paranthesis = _paranthesisStack.Pop();
                throw ParsingException.ParanthesisNotMatched(paranthesis);
            }
        }
        //---------------------------------------------------------------------
        public void Visit(Operation operation)
        {
            this.CheckIntrinsic(operation);

            if (_lastToken is Operation)
                throw ParsingException.OperationFollowedByOperation(_lastToken, operation);

            _lastToken = operation;
        }
        //---------------------------------------------------------------------
        public void Visit(ParameterToken parameter)
        {
            this.CheckIntrinsic(parameter);

            _lastToken = parameter;
        }
        //---------------------------------------------------------------------
        public void Visit(LeftParanthesis paranthesis)
        {
            _paranthesisStack.Push(paranthesis);
            _lastToken = paranthesis;
        }
        //---------------------------------------------------------------------
        public void Visit(RightParanthesis paranthesis)
        {
            this.CheckIntrinsic(paranthesis);

            if (_paranthesisStack.Count == 0 || !(_paranthesisStack.Peek() is LeftParanthesis))
                throw ParsingException.NoLeftParanthesis(paranthesis);

            _paranthesisStack.Pop();

            _lastToken = paranthesis;
        }
        //---------------------------------------------------------------------
        public void Visit(ValueToken valueToken)
        {
            this.CheckIntrinsic(valueToken);

            if (_lastToken is ValueToken)
                throw ParsingException.ValueFollowedByValue(_lastToken as ValueToken, valueToken);

            _lastToken = valueToken;
        }
        //---------------------------------------------------------------------
        public void Visit(Constant constant)
        {
            this.CheckIntrinsic(constant);
        }
        //---------------------------------------------------------------------
        public void Visit(Intrinsic intrinsic)
        {
            _lastToken = intrinsic;
        }
        //---------------------------------------------------------------------
        private void CheckIntrinsic(Token current)
        {
            if (!(_lastToken is Intrinsic) || current is LeftParanthesis) return;

            throw ParsingException.IntrinsicNoFollowedByLeftParanthesis(_lastToken as Intrinsic);
        }
    }
}