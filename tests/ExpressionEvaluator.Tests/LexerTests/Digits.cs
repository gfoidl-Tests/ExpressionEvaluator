using System.Linq;
using ExpressionEvaluator.Tokens;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.LexerTests
{
    public class Digits : BaseFixture
    {
        [Test]
        public void Digits___OK([Values("2", "3.14", "-1", "3e2", "-3e4", "1E2", "1_000", "2_000_000")]string expression)
        {
            Lexer sut = this.CreateSut(expression);

            ValueToken actual = sut.ReadTokens()
                .Cast<ValueToken>()
                .Single();

            Assert.AreEqual(double.Parse(expression.Replace("_", "")), actual.Value);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Minus_Plus___throws_ParsingException([Values("+-1", "-+1")] string expression)
        {
            Lexer sut = this.CreateSut(expression);

            Assert.Throws<ParsingException>(() => sut.ReadTokens().ToList());
        }
    }
}