﻿using System.IO;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.LexerTests
{
    [TestFixture]
    public abstract class BaseFixture
    {
        internal Lexer CreateSut(string expression)
        {
            return new Lexer(new PositionTextReader(new StringReader(expression)));
        }
    }
}