using System;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests.ExpressionEvaluatorTests
{
    [TestFixture(typeof(BasicExpressionEvaluator))]
    [TestFixture(typeof(CachedExpressionEvaluator))]
    public class Intrinsics<T> where T : ExpressionEvaluator, new()
    {
        private ExpressionEvaluator _sut;
        //---------------------------------------------------------------------
        [SetUp]
        public void SetUp() => _sut = new T();
        //---------------------------------------------------------------------
        [Test]
        public void Simple_sin___OK()
        {
            double x          = 0.8312;
            string expression = "sin(x)";

            double actual = _sut.Evaluate(expression, x);

            Assert.AreEqual(Math.Sin(x), actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Sin_and_math___OK()
        {
            double x          = 0.2344;
            string expression = "2*sin(x)";

            double actual = _sut.Evaluate(expression, x);

            Assert.AreEqual(2 * Math.Sin(x), actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Sin_and_other_math___OK([Values("2x + 3sin(x) / 2", "2*x + 3 * sin(x)/2")]string expression)
        {
            double x = 0.1234;

            double actual = _sut.Evaluate(expression, x);

            Assert.AreEqual(2 * x + 3 * Math.Sin(x) / 2d, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Intrinsics_combined___OK()
        {
            double x          = 0.234353;
            string expression = "sin(cos(x))";

            double actual = _sut.Evaluate(expression, x);

            Assert.AreEqual(Math.Sin(Math.Cos(x)), actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Complex_use_of_intrinsics___OK()
        {
            double x          = 1.234;
            double y          = 10.234;
            string expression = "2sin(x)/cos(y)*log(x*y)-10";

            double actual = _sut.Evaluate(expression, x, y);

            Assert.AreEqual(2 * Math.Sin(x) / Math.Cos(y) * Math.Log(x * y) - 10, actual, 1e-10);
        }
    }
}