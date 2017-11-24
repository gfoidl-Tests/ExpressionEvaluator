using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator
{
    internal class Lexer
    {
        private readonly TextReader _tr;
        //---------------------------------------------------------------------
        public Lexer(TextReader tr) => _tr = tr;
        //---------------------------------------------------------------------
        public IEnumerable<Token> ReadTokens()
        {
            int peek;
            int position = 0;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;
                position++;

                if (char.IsDigit(next))
                    yield return this.ReadOperand();
                else if (char.IsLetter(next))
                    yield return this.ReadParameter();
                else if (Operation.IsDefined(next))
                    yield return this.ReadOperation();
                else if (next == '(')
                {
                    yield return Paranthesis.Left;
                    _tr.Read();
                }
                else if (next == ')')
                {
                    yield return Paranthesis.Right;
                    _tr.Read();
                }
                else if (next == ' ' || next == '\t')
                {
                    _tr.Read();
                    continue;
                }
                else
                    throw new InvalidOperationException($"Unknown token '{next}' at position {position}");
            }
        }
        //---------------------------------------------------------------------
        private ValueToken ReadOperand()
        {
            var sb = new StringBuilder();
            int peek;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsDigit(next) || next == '.')
                {
                    _tr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            return new ValueToken(sb.ToString());
        }
        //---------------------------------------------------------------------
        private Operation ReadOperation()
        {
            char operation = (char)_tr.Read();
            return (Operation)operation;
        }
        //---------------------------------------------------------------------
        private ParameterToken ReadParameter()
        {
            var sb = new StringBuilder();
            int peek;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsLetter(next))
                {
                    _tr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            return new ParameterToken(sb.ToString());
        }
    }
}