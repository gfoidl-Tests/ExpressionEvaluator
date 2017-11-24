using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.BasicExpressionEvaluatorTests
{
    [TestFixture]
    public class Evaluate
    {
        private Random _rnd = new Random();
        //---------------------------------------------------------------------
        [Test]
        public void Empty_string___is_0([Values("", " ", "\t")]string value)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(value);

            Assert.AreEqual(0, actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Double___OK()
        {
            double left = _rnd.Next(1, 100);
            double right = _rnd.Next(1, 100);
            double input = left + right / 100d;

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(input.ToString());

            Assert.AreEqual(input, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Add_two_numbers___OK([Values("2.7+3.2", "2.7 + 3.2")]string expression)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

            Assert.AreEqual(2.7 + 3.2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Add_more_than_two_numbers___OK()
        {
            string expression = "1+2+3";

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

            Assert.AreEqual(6, actual, 1e-10);
        }
    }
}