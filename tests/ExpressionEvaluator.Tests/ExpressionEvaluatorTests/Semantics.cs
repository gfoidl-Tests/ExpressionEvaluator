using System.Collections.Generic;
using ExpressionCompiler.Parser;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture]
    public class Semantics
    {
        [Test]
        public void Value_followed_by_value___throws_ParsingException()
        {
            string expr = "3 4";

            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        [Test]
        public void No_left_paranthesis_when_right_is_given___throws_ParsingException()
        {
            string expr = "3 + 4 * 2)";

            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        [Test]
        public void Right_paranthesis_is_missing___throws_ParsingException(
            [Values("(3+4", "3*5 + (12 - 4.32")] string expr)
        {
            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        [Test]
        public void Operand_followed_by_operand___throws_ParsingException(
            [Values("3+*4", "-1 +/ 2", "4.23 -+ 2.234")] string expr)
        {
            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        [Test]
        public void Exponentation_following_other_operation___OK()
        {
            string exp = "2+e^4";
            var sut    = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(exp);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Intrinsic_not_followed_by_parantheis___throws_ParsingException(
            [Values("sin 3.14", "cos 0.84")] string expr)
        {
            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Complex_failures___throws_ParsingException_TestCases))]
        public void Complex_failures___throws_ParsingException(string expr)
        {
            var sut = new BasicExpressionEvaluator();

            Assert.Throws<ParsingException>(() => sut.Evaluate(expr));
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Complex_failures___throws_ParsingException_TestCases()
        {
            yield return new TestCaseData("(3+4*(3.2+12)))");
            yield return new TestCaseData("3*sin(0.32+cos(0.234-2)");
            yield return new TestCaseData("((3+4)(log(1.2)*sin(3.14)");
            yield return new TestCaseData("sin(cos(pi)");
        }
    }
}