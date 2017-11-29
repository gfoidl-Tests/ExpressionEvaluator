using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Emitter.LaTeX
{
    internal class PrettyPrintRules
    {
        public bool PrintOperator(BinaryExpression binaryExpression)
        {
            if (!(binaryExpression is MultiplyExpression)) return true;

            Expression left  = binaryExpression.Left;
            Expression right = binaryExpression.Right;

            if (!(right is ArrayIndexExpression)) return true;
            if (!(left  is ConstantExpression))   return true;

            var valueToken = left.Token as ValueToken;

            if (valueToken == null)     return true;
            if (valueToken is Constant) return true;

            return false;
        }
        //---------------------------------------------------------------------
        public bool NeedParanthesis(Expression current, Expression parent)
        {
            if (parent == null) return false;
            if (!(current is AddExpression) && !(current is SubtractExpression)) return false;

            if (parent is AddExpression || parent is SubtractExpression) return false;

            return true;
        }
    }
}