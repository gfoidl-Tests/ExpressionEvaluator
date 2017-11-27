using System;
using System.Collections.Generic;
using ExpressionCompiler.Tokens;
using ExpressionCompiler.Visitors;

namespace ExpressionCompiler
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