using System;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.BasicExpressionEvaluatorTests
{
    [TestFixture]
    public class Variables
    {
        [Test]
        public void Simple_variable___OK()
        {
            double a          = Math.PI;
            string expression = "a";

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression, a);

            Assert.AreEqual(Math.PI, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Variable_and_other_math___OK([Values("2*a+3-4*a", "2a + 3-4a")]string expression)
        {
            double a = Math.PI;

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression, a);

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

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression, a, b, c);

            Assert.AreEqual((((9 - a / 2) * 2 - b) / 2 - a - 1) / (2 + c / (2 + 4)), actual, 1e-10);
        }
    }
}