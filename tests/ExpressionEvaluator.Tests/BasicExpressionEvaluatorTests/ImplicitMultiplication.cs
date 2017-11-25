using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.BasicExpressionEvaluatorTests
{
    [TestFixture]
    public class ImplicitMultiplication
    {
        [Test, TestCaseSource(nameof(Implicit_Multiplication___OK_TestCases))]
        public void Implicit_Multiplication___OK(string expression, double expected)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

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