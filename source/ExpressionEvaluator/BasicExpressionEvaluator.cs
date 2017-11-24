using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionEvaluator
{
    public class BasicExpressionEvaluator
    {
        static BasicExpressionEvaluator()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }
        //---------------------------------------------------------------------
        public double Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return 0;

            var expressionStack = new Stack<Expression>();
            var operationStack  = new Stack<Operation>();
            var sr              = new StringReader(expression);
            int peek;

            while ((peek = sr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsDigit(next))
                {
                    expressionStack.Push(this.ReadOperand(sr));
                    continue;
                }

                if (Operation.IsDefined(next))
                {
                    Operation currentOperation = this.ReadOperation(sr);

                    while (true)
                    {
                        if (operationStack.Count == 0)
                        {
                            operationStack.Push((Operation)next);
                            break;
                        }

                        var lastOperation = operationStack.Peek();

                        if (currentOperation.Precedence > lastOperation.Precedence)
                        {
                            operationStack.Push((Operation)next);
                            break;
                        }

                        Expression right = expressionStack.Pop();
                        Expression left  = expressionStack.Pop();

                        expressionStack.Push(operationStack.Pop().Apply(left, right));
                    }
                    continue;
                }

                if (next == ' ')
                {
                    sr.Read();
                    continue;
                }

                if (next != ' ')
                    throw new ArgumentException("Invalid character encountered", nameof(expression));
            }

            while (operationStack.Count > 0)
            {
                Expression right = expressionStack.Pop();
                Expression left  = expressionStack.Pop();

                expressionStack.Push(operationStack.Pop().Apply(left, right));
            }

            var compiled = Expression.Lambda<Func<double>>(expressionStack.Pop()).Compile();
            return compiled();
        }
        //---------------------------------------------------------------------
        private Expression ReadOperand(StringReader sr)
        {
            StringBuilder sb = new StringBuilder();
            int peek;

            while ((peek = sr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsDigit(next) || next == '.')
                {
                    sr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            return Expression.Constant(double.Parse(sb.ToString()));
        }
        //---------------------------------------------------------------------
        private Operation ReadOperation(StringReader sr)
        {
            char operation = (char)sr.Read();
            return (Operation)operation;
        }
    }
}