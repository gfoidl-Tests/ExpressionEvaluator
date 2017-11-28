using ExpressionCompiler.Emitter.LaTeX;
using ExpressionCompiler.Expressions;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.Emitter.LaTeX.PrettyPrintRulesTests
{
    [TestFixture]
    public class NeedParanthesis
    {
        [Test]
        public void No_parent_Operation___false()
        {
            var expr = new AddExpression(default, default, default);

            var sut = new PrettyPrintRules();

            bool actual = sut.NeedParanthesis(expr, null);

            Assert.IsFalse(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Addition_with_parent_addition___false()
        {
            var expr = new AddExpression(default, default, default);

            var sut = new PrettyPrintRules();

            bool actual = sut.NeedParanthesis(expr, new AddExpression(default, expr, default));

            Assert.IsFalse(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Addition_with_parent_subtraction___false()
        {
            var expr = new AddExpression(default, default, default);

            var sut = new PrettyPrintRules();

            bool actual = sut.NeedParanthesis(expr, new SubtractExpression(default, expr, default));

            Assert.IsFalse(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Addition_with_parent_multiplication___true()
        {
            var expr = new AddExpression(default, default, default);

            var sut = new PrettyPrintRules();

            bool actual = sut.NeedParanthesis(expr, new MultiplyExpression(default, expr, default));

            Assert.IsTrue(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Addition_with_parent_intrinsic___false()
        {
            var expr = new AddExpression(default, default, default);

            var sut = new PrettyPrintRules();

            bool actual = sut.NeedParanthesis(expr, new SinExpression(default, expr));

            Assert.IsTrue(actual);
        }
    }
}