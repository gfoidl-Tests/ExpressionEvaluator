using System;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class Variables<T> where T : ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test]
        public void Simple_variable___OK()
        {
            double a          = Math.PI;
            string expression = "a";

            double actual = _sut.Evaluate(expression, a);

            Assert.AreEqual(Math.PI, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Variable_and_other_math___OK([Values("2*a+3-4*a", "2a + 3-4a")]string expression)
        {
            double a = Math.PI;

            double actual = _sut.Evaluate(expression, a);

            Assert.AreEqual(2 * a + 3 - 4 * a, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiple_variables___OK()
        {
            double a          = 2.6;
            double b          = 5.7;
            double c          = 24.15;
            string expression = "(((9-a/2)*2-b)/2-a-1)/(2+c/(2+4))";

            double actual = _sut.Evaluate(expression, a, b, c);

            Assert.AreEqual((((9 - a / 2) * 2 - b) / 2 - a - 1) / (2 + c / (2 + 4)), actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Less_args_given_than_parameters_in_expression___throws_ArgumentException()
        {
            double a    = 234.234234;
            string expr = "x+y";

            Assert.Throws<ArgumentException>(() => _sut.Evaluate(expr, a));
        }
    }
}