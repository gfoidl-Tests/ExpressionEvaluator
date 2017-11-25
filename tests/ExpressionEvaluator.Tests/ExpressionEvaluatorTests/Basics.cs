using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class Basics<T> where T: ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        private readonly Random     _rnd = new Random();
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test]
        public void Empty_string___is_NaN([Values("", " ", "\t")]string value)
        {
            double actual = _sut.Evaluate(value);

            Assert.AreEqual(double.NaN, actual);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Double___OK()
        {
            double left  = _rnd.Next(1, 100);
            double right = _rnd.Next(1, 100);
            double input = left + right / 100d;

            double actual = _sut.Evaluate(input.ToString());

            Assert.AreEqual(input, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Add_two_numbers___OK([Values("2.7+3.2", "2.7 + 3.2")]string expression)
        {
            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(2.7 + 3.2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Add_more_than_two_numbers___OK()
        {
            string expression = "1+2+3";

            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(6, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Subtract_two_numbers___OK([Values("2.7-3.2", "2.7 - 3.2")]string expression)
        {
            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(2.7 - 3.2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Subtract_more_than_two_numbers___OK()
        {
            string expression = "6-3-1";

            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Add_and_subtract___OK_TestCases))]
        public void Add_and_subtract___OK(string expression, double expected)
        {
            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(expected, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Add_and_subtract___OK_TestCases()
        {
            yield return new TestCaseData("15+8-4-2+7", 15 + 8 - 4 - 2 + 7);
            yield return new TestCaseData("17.89-2.47+7.16", 17.89 - 2.47 + 7.16);
            yield return new TestCaseData("12-3.4+1.2", 12 - 3.4 + 1.2);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Exponentation___OK()
        {
            double a = 3.12;
            double b = 5.48;

            string expression = "a^b";

            double actual = _sut.Evaluate(expression, a, b);

            Assert.AreEqual(Math.Pow(a, b), actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Modulo___OK()
        {
            double a = 18.12;
            double b = 5.48;

            string expression = "a % b";

            double actual = _sut.Evaluate(expression, a, b);

            Assert.AreEqual(a % b, actual, 1e-10);
        }
    }
}