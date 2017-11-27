using System.Collections.Generic;
using System.Linq;
using ExpressionCompiler.Parser;
using ExpressionCompiler.Tokens;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.LexerTests
{
    public class Arguments : BaseFixture
    {
        [Test, TestCaseSource(nameof(Argument_given___OK_TestCases))]
        public void Argument_given___OK(string expression)
        {
            Lexer sut = this.CreateSut(expression);

            ParameterToken actual = sut.ReadTokens()
                .Cast<ParameterToken>()
                .Single();

            Assert.AreEqual(expression, actual.Parameter);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Argument_given___OK_TestCases()
        {
            yield return new TestCaseData("a");
            yield return new TestCaseData("x2");
            yield return new TestCaseData("sina");
        }
    }
}