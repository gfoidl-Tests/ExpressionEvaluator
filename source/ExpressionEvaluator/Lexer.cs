using System;
using System.Collections.Generic;
using System.Text;
using ExpressionEvaluator.Tokens;

namespace ExpressionEvaluator
{
    internal class Lexer
    {
        private readonly PositionTextReader _tr;
        //---------------------------------------------------------------------
        public Lexer(PositionTextReader tr) => _tr = tr;
        //---------------------------------------------------------------------
        public IEnumerable<Token> ReadTokens()
        {
            int peek;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (this.ReadOperand(out ValueToken valueToken))
                    yield return valueToken;
                else if (char.IsLetter(next))
                    yield return this.ReadIdentifier();
                else if (Operation.IsDefined(next))
                    yield return this.ReadOperation();
                else if (next == '(')
                {
                    _tr.Read();
                    yield return Paranthesis.Left;
                }
                else if (next == ')')
                {
                    _tr.Read();
                    yield return Paranthesis.Right;
                }
                else if (next == ' ' || next == '\t')
                {
                    _tr.Read();
                    continue;
                }
                else
                    throw new InvalidOperationException($"Unknown token '{next}' at position {_tr.Position}");
            }
        }
        //---------------------------------------------------------------------
        private bool ReadOperand(out ValueToken valueToken)
        {
            var sb     = new StringBuilder();
            int peek   = _tr.Peek();
            valueToken = default;

            if (!IsValid((char)peek))
                return false;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (IsValid(next))
                {
                    _tr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            valueToken = new ValueToken(sb.ToString());
            return true;
            //-----------------------------------------------------------------
            bool IsValid(char c)
            {
                if (char.IsDigit(c) || c == '.')                 return true;
                if (_tr.Position == 0 && (c == '-' || c == '+')) return true;
                if (sb.Length > 0 && (c == 'e' || c == 'E'))     return true;

                return false;
            }
        }
        //---------------------------------------------------------------------
        private Operation ReadOperation()
        {
            char operation = (char)_tr.Read();
            return (Operation)operation;
        }
        //---------------------------------------------------------------------
        private Token ReadIdentifier()
        {
            var sb = new StringBuilder();
            int peek;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (char.IsLetter(next) || (sb.Length > 0 && char.IsDigit(next)))
                {
                    _tr.Read();
                    sb.Append(next);
                }
                else
                    break;
            }

            string identifier = sb.ToString();

            if (Intrinsic.IsDefined(identifier))
                return (Intrinsic)identifier;
            else if (Constant.IsDefined(identifier))
                return (Constant)identifier;

            return new ParameterToken(identifier);
        }
    }
}