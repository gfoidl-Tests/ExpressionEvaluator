using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.LexerTests
{
    public class Intrinsic : BaseFixture
    {
        [Test, TestCaseSource(nameof(Intrinsic_given___OK_TestCases))]
        public void Intrinsic_given___OK(string expression, string intrinsic)
        {
            Lexer sut = this.CreateSut(expression);

            Tokens.Intrinsic actual = sut.ReadTokens()
                .Cast<Tokens.Intrinsic>()
                .Single();

            Assert.AreSame(intrinsic, actual.Name);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Intrinsic_given___OK_TestCases()
        {
            yield return new TestCaseData("sin", nameof(Tokens.Intrinsic.Sinus));
            yield return new TestCaseData("cos", nameof(Tokens.Intrinsic.Cosinus));
            yield return new TestCaseData("tan", nameof(Tokens.Intrinsic.Tangens));
        }
    }
}