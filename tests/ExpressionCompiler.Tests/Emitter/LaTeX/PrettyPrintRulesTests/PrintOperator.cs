using ExpressionCompiler.Emitter.LaTeX;
using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.Emitter.LaTeX.PrettyPrintRulesTests
{
    [TestFixture]
    internal class PrintOperator
    {
        [Test]
        public void No_multiplication___true()
        {
            BinaryExpression binaryExpression = new AddExpression(
                default,
                new ConstantExpression(new ValueToken(Position.NotDefined, 1.2)),
                new ArrayIndexExpression(default, default, default));

            var sut = new PrettyPrintRules();

            bool actual = sut.PrintOperator(binaryExpression);

            Assert.IsTrue(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiplication_with_constant___true()
        {
            BinaryExpression binaryExpression = new MultiplyExpression(
                default,
                new ConstantExpression(Constant.Pi(default)),
                new ArrayIndexExpression(default, default, default));

            var sut = new PrettyPrintRules();

            bool actual = sut.PrintOperator(binaryExpression);

            Assert.IsTrue(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiplication_with_value_that_is_not_a_constant___false()
        {
            BinaryExpression binaryExpression = new MultiplyExpression(
                default,
                new ConstantExpression(new ValueToken(Position.NotDefined, 1.2)),
                new ArrayIndexExpression(default, default, default));

            var sut = new PrettyPrintRules();

            bool actual = sut.PrintOperator(binaryExpression);

            Assert.IsFalse(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiplication_with_other_term_on_left___true()
        {
            BinaryExpression binaryExpression = new MultiplyExpression(
                default,
                new AddExpression(default, default, default),
                new ArrayIndexExpression(default, default, default));

            var sut = new PrettyPrintRules();

            bool actual = sut.PrintOperator(binaryExpression);

            Assert.IsTrue(actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiplication_with_other_term_on_right___true()
        {
            BinaryExpression binaryExpression = new MultiplyExpression(
                default,
                new ConstantExpression(new ValueToken(Position.NotDefined, 1.2)),
                new AddExpression(default, default, default));

            var sut = new PrettyPrintRules();

            bool actual = sut.PrintOperator(binaryExpression);

            Assert.IsTrue(actual);
        }
    }
}