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
            var semanticVisitor = new SemanticVisitor();
            var parsingVisitor  = new ParsingVisitor();

            foreach (Token token in tokens)
            {
#if DEBUG
                Console.WriteLine(token);
#endif
                token.Accept(semanticVisitor);
                token.Accept(parsingVisitor);
            }

            semanticVisitor.Validate();
            return parsingVisitor.GetExpressionTree();
        }
    }
}