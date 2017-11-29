using ExpressionCompiler.Expressions;
using ExpressionCompiler.Tokens;
using NUnit.Framework;

namespace ExpressionCompiler.Tests.Optimizer.OptimizerTests
{
    [TestFixture]
    public class Optimize
    {
        [Test]
        public void Addition_of_constants___replaced_with_result()
        {
            double a = 1.12;
            double b = 2.23;

            var ae   = new ConstantExpression(new ValueToken(default, a));
            var be   = new ConstantExpression(new ValueToken(default, b));
            var tree = new AddExpression(new Addition(default), ae, be);

            var sut = new ExpressionCompiler.Optimizer.Optimizer(tree);

            ConstantExpression actual = sut.Optimize() as ConstantExpression;

            Assert.AreEqual(a + b, actual.Value, 1e-10);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Subtraction_of_constants___replaced_with_result()
        {
            double a = 1.12;
            double b = 2.23;

            var ae   = new ConstantExpression(new ValueToken(default, a));
            var be   = new ConstantExpression(new ValueToken(default, b));
            var tree = new SubtractExpression(new Subtraction(default), ae, be);

            var sut = new ExpressionCompiler.Optimizer.Optimizer(tree);

            ConstantExpression actual = sut.Optimize() as ConstantExpression;

            Assert.AreEqual(a - b, actual.Value, 1e-10);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Multiplication_of_constants___replaced_with_result()
        {
            double a = 1.12;
            double b = 2.23;

            var ae   = new ConstantExpression(new ValueToken(default, a));
            var be   = new ConstantExpression(new ValueToken(default, b));
            var tree = new MultiplyExpression(new Multiplication(default), ae, be);

            var sut = new ExpressionCompiler.Optimizer.Optimizer(tree);

            ConstantExpression actual = sut.Optimize() as ConstantExpression;

            Assert.AreEqual(a * b, actual.Value, 1e-10);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Division_of_constants___replaced_with_result()
        {
            double a = 1.12;
            double b = 2.23;

            var ae   = new ConstantExpression(new ValueToken(default, a));
            var be   = new ConstantExpression(new ValueToken(default, b));
            var tree = new DivideExpression(new Division(default), ae, be);

            var sut = new ExpressionCompiler.Optimizer.Optimizer(tree);

            ConstantExpression actual = sut.Optimize() as ConstantExpression;

            Assert.AreEqual(a / b, actual.Value, 1e-10);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Parameter_multiplication_by_2___replaced_with_addition()
        {
            var token = new ParameterToken(default, "a");
            var a     = new ArrayIndexExpression(
                token,
                new ParameterExpression(token),
                new IndexExpression(0));

            var multiplication = new MultiplyExpression(
                new Multiplication(default),
                new ConstantExpression(new ValueToken(default, 2)),
                a);

            var sut = new ExpressionCompiler.Optimizer.Optimizer(multiplication);

            AddExpression actual = sut.Optimize() as AddExpression;

            Assert.AreEqual(a, actual.Left);
            Assert.AreEqual(a, actual.Right);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Two_multiplyed_by_parameter___replaced_with_addition()
        {
            var token = new ParameterToken(default, "a");
            var a     = new ArrayIndexExpression(
                token,
                new ParameterExpression(token),
                new IndexExpression(0));

            var multiplication = new MultiplyExpression(
                new Multiplication(default),
                a,
                new ConstantExpression(new ValueToken(default, 2)));

            var sut = new ExpressionCompiler.Optimizer.Optimizer(multiplication);

            AddExpression actual = sut.Optimize() as AddExpression;

            Assert.AreEqual(a, actual.Left);
            Assert.AreEqual(a, actual.Right);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Parameter_pow_2___replaced_with_multiplication()
        {
            var token = new ParameterToken(default, "a");
            var a     = new ArrayIndexExpression(
                token,
                new ParameterExpression(token),
                new IndexExpression(0));

            var pow = new ExponentationExpression(
                new Exponentation(default),
                a,
                new ConstantExpression(new ValueToken(default, 2)));

            var sut = new ExpressionCompiler.Optimizer.Optimizer(pow);

            MultiplyExpression actual = sut.Optimize() as MultiplyExpression;

            Assert.AreEqual(a, actual.Left);
            Assert.AreEqual(a, actual.Right);
            Assert.IsTrue(sut.DidAnyOptimization);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Parameter_pow_not_2___not_replaced_with_multiplication([Values(1, 3, 4, 5)] int exponent)
        {
            var token = new ParameterToken(default, "a");
            var a     = new ArrayIndexExpression(
                token,
                new ParameterExpression(token),
                new IndexExpression(0));

            var pow = new ExponentationExpression(
                default,
                a,
                new ConstantExpression(new ValueToken(default, exponent)));

            var sut = new ExpressionCompiler.Optimizer.Optimizer(pow);

            Expression actual = sut.Optimize();

            Assert.IsInstanceOf<ExponentationExpression>(actual);
            Assert.IsFalse(sut.DidAnyOptimization);
        }
    }
}