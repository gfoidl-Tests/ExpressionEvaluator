using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.LexerTests
{
    public class Intrinsic : BaseFixture
    {
        [Test, TestCaseSource(nameof(Intrinsic_given___OK_TestCases))]
        public void Intrinsic_given___OK(string expression, Tokens.Intrinsic intrinsic)
        {
            Lexer sut = this.CreateSut(expression);

            Tokens.Intrinsic actual = sut.ReadTokens()
                .Cast<Tokens.Intrinsic>()
                .Single();

            Assert.AreSame(intrinsic, actual);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Intrinsic_given___OK_TestCases()
        {
            yield return new TestCaseData("sin", Tokens.Intrinsic.Sinus);
            yield return new TestCaseData("cos", Tokens.Intrinsic.Cosinus);
            yield return new TestCaseData("tan", Tokens.Intrinsic.Tangens);
        }
    }
}