using System;
using System.Linq;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.LexerTests
{
    public class Constant : BaseFixture
    {
        [Test]
        public void Pi_given___OK()
        {
            Lexer sut = this.CreateSut("pi");

            Tokens.Constant actual = sut.ReadTokens()
                .Cast<Tokens.Constant>()
                .Single();

            Assert.AreEqual(Math.PI, actual.Value);
        }
        //---------------------------------------------------------------------
        [Test]
        public void E_given___OK()
        {
            Lexer sut = this.CreateSut("e");

            Tokens.Constant actual = sut.ReadTokens()
                .Cast<Tokens.Constant>()
                .Single();

            Assert.AreEqual(Math.E, actual.Value);
        }
    }
}