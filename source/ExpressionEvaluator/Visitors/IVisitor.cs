using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator.Visitors
{
    public interface IVisitor
    {
        void Visit(Operation operation);
        void Visit(ParameterToken parameter);
        void Visit(LeftParanthesis paranthesis);
        void Visit(RightParanthesis paranthesis);
        void Visit(ValueToken valueToken);
        void Visit(Intrinsic intrinsic);
    }
}