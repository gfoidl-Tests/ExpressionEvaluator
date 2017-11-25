using System.Collections.Generic;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class Paranthesis<T> where T : ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Expression_given___OK_TestCases))]
        public void Expression_given___OK(string expression, double expected)
        {
            double actual = _sut.Evaluate(expression);

            Assert.AreEqual(expected, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        private static IEnumerable<TestCaseData> Expression_given___OK_TestCases()
        {
            yield return new TestCaseData("2+5*3", 2 + 5 * 3);
            yield return new TestCaseData("10/3.1+2*5-12.32/5", 10 / 3.1 + 2 * 5 - 12.32 / 5);
            yield return new TestCaseData("2*(5+3)", 2 * (5 + 3));
            yield return new TestCaseData("(5+3)*2", (5 + 3) * 2);
            yield return new TestCaseData("(5+3)*5-2", (5 + 3) * 5 - 2);
            yield return new TestCaseData("(5+3)*(5-2)", (5 + 3) * (5 - 2));
            yield return new TestCaseData("((5+3)*3-(8-2)/2)/2", ((5 + 3) * 3 - (8 - 2) / 2d) / 2d);
            yield return new TestCaseData("(4*(3+5)-4-8/2-(6-4)/2)*((2+4)*4-(8-5)/3)-5", (4 * (3 + 5) - 4 - 8 / 2 - (6 - 4) / 2) * ((2 + 4) * 4 - (8 - 5) / 3) - 5);
            yield return new TestCaseData("(((9-6/2)*2-4)/2-6-1)/(2+24/(2+4))", (((9 - 6 / 2) * 2 - 4) / 2d - 6 - 1) / (2 + 24 / (2 + 4)));
        }
    }
}