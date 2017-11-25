using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator.Visitors
{
    internal interface IVisitor
    {
        void Visit(Operation operation);
        void Visit(ParameterToken parameter);
        void Visit(Paranthesis paranthesis);
        void Visit(ValueToken valueToken);
    }
}