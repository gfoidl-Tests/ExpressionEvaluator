﻿using System.IO;
using ExpressionCompiler.Parser;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.LexerTests
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