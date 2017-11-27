using System;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Parser
{
    [Serializable]
    public class ParsingException : Exception
    {
        public ParsingException() { }
        public ParsingException(string message) : base(message)
        {
#if DEBUG
            Console.WriteLine(message);
#endif
        }
        public ParsingException(string message, Exception inner) : base(message, inner) { }
        protected ParsingException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        //---------------------------------------------------------------------
        internal static ParsingException LiteralWithTwoSigns()
        {
            return new ParsingException("A literal can't have to signs (+ or -).");
        }
        //---------------------------------------------------------------------
        internal static ParsingException OperationFollowedByOperation(Token lastToken, Operation operation)
        {
            string msg = $"Operation {operation.Name} at position {operation.Position} can't follow operation {lastToken.Name} at position {lastToken.Position}.";

            return new ParsingException(msg);
        }
        //---------------------------------------------------------------------
        internal static ParsingException NoLeftParanthesis(RightParanthesis rightParanthesis)
        {
            string msg = $"No left parenthesis that matches the right paranthesis at position {rightParanthesis.Position}.";

            return new ParsingException(msg);
        }
        //---------------------------------------------------------------------
        internal static ParsingException ValueFollowedByValue(ValueToken lastToken, ValueToken valueToken)
        {
            string msg =
                $"Value {valueToken.Value} at position {valueToken.Position} can't follow value {lastToken.Value} at position {lastToken.Position}." +
                "\nAre you missing an operation?";

            return new ParsingException(msg);
        }
        //---------------------------------------------------------------------
        internal static ParsingException IntrinsicNoFollowedByLeftParanthesis(Intrinsic lastToken)
        {
            string msg = $"Intrinsic {lastToken.Name} at position {lastToken.Position} must be followed by a left paranthesis.";

            return new ParsingException(msg);
        }
        //---------------------------------------------------------------------
        internal static ParsingException ParanthesisNotMatched(Paranthesis paranthesis)
        {
            string msg = $"Paranthesis {paranthesis.Name} at position {paranthesis.Position} is not matched by counterpart.";

            return new ParsingException(msg);
        }
    }
}