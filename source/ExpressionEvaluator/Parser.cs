using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ExpressionEvaluator.Tokens;
using ExpressionEvaluator.Visitors;

namespace ExpressionEvaluator
{
    internal class Parser
    {
        public ParsingResult Parse(IEnumerable<Token> tokens)
        {
            var arrayParameter = Expression.Parameter(typeof(double[]), "args");
            var treeVisitor    = new ParsingVisitor(arrayParameter);

            foreach (Token token in tokens)
            {
#if DEBUG
                Console.WriteLine(token);
#endif
                token.Accept(treeVisitor);
            }

            return treeVisitor.GetExpressionTree();
        }
    }
}