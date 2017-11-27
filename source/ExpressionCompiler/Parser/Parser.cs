using System;
using System.Collections.Generic;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Parser
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

            var result = parsingVisitor.GetExpressionTree();
#if DEBUG
            Console.WriteLine("\nExpression Tree");
            Console.WriteLine(result.Tree);
            Console.WriteLine($"No of params: {result.Parameters.Count}");
#endif
            return result;
        }
    }
}