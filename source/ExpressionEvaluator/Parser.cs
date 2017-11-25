using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionEvaluator.Tokens;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator
{
    internal class Parser
    {
        public Expression Parse(IEnumerable<Token> tokens, ParameterExpression arrayParameter)
        {
            var treeVisitor = new TreeVisitor(arrayParameter);

            foreach (Token token in tokens)
                token.Accept(treeVisitor);

            return treeVisitor.GetExpressionTree();
        }
    }
}