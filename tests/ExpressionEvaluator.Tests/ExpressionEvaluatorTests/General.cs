using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class General<T> where T : ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Expression_given___OK_TestCases))]
        public void Expression_given___OK(string expression, double expected)
        {
            double a = 2.1324;
            double b = -0.234e2;
            double c = 234.8002;

            double actual = _sut.Evaluate(expression, a, b, c);

            Assert.AreEqual(expected, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Expression_given___OK_TestCases()
        {
            double a = 2.1324;
            double b = -0.234e2;
            double c = 234.8002;

            yield return new TestCaseData("a + sin(log(a+1)) - tan(a) * cos(a*b) + c * cos(pi/4)", a + Math.Sin(Math.Log(a + 1)) - Math.Tan(a) * Math.Cos(a * b) + c * Math.Cos(Math.PI / 4));
        }
    }
}