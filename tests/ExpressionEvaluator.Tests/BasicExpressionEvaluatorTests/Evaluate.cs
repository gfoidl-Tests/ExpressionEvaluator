﻿using System;
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
        //---------------------------------------------------------------------
        [Test]
        public void Subtract_two_numbers___OK([Values("2.7-3.2", "2.7 - 3.2")]string expression)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

            Assert.AreEqual(2.7 - 3.2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test]
        public void Subtract_more_than_two_numbers___OK()
        {
            string expression = "6-3-1";

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

            Assert.AreEqual(2, actual, 1e-10);
        }
        //---------------------------------------------------------------------
        [Test, TestCaseSource(nameof(Add_and_subtract___OK_TestCases))]
        public void Add_and_subtract___OK(string expression, double expected)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

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
        [Test, TestCaseSource(nameof(Expression_given___OK_TestCases))]
        public void Expression_given___OK(string expression, double expected)
        {
            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression);

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
        //---------------------------------------------------------------------
        [Test]
        public void Simple_variable___OK()
        {
            double a          = Math.PI;
            string expression = "a";

            var sut = new BasicExpressionEvaluator();

            double actual = sut.Evaluate(expression, a);

            Assert.AreEqual(Math.PI, actual, 1e-10);
        }
    }
}