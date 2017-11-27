using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Visitors
{
    internal interface ITokenVisitor
    {
        void Visit(ValueToken       valueToken);
        void Visit(Constant         constant);
        void Visit(ParameterToken   parameter);
        void Visit(LeftParanthesis  paranthesis);
        void Visit(RightParanthesis paranthesis);
        void Visit(Operation        operation);
        void Visit(Intrinsic        intrinsic);
    }
}