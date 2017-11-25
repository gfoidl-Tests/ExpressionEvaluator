using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class ImplicitMultiplication<T> where T : ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Implicit_Multiplication___OK_TestCases))]
        public void Implicit_Multiplication___OK(string expression, double expected)
        {
            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(expected, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Implicit_Multiplication___OK_TestCases()
        {
            yield return new TestCaseData("2pi", 2 * Math.PI);
            yield return new TestCaseData("(3+4)(2*pi)", (3 + 4) * (2 * Math.PI));
            yield return new TestCaseData("(3+4)(2pi)", (3 + 4) * (2 * Math.PI));
        }
    }
}