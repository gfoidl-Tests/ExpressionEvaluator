using System.Linq;
using ExpressionEvaluator.Tokens;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.LexerTests
{
    public class Digits : BaseFixture
    {
        [Test]
        public void Digits___OK([Values("2", "3.14")]string expression)
        {
            Lexer sut = this.CreateSut(expression);

            ValueToken actual = sut.ReadTokens()
                .Cast<ValueToken>()
                .Single();

            Assert.AreEqual(double.Parse(expression), actual.Value);
        }
    }
}