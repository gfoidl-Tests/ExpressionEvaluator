using System;
using System.Collections.Generic;
using System.Text;
using ExpressionCompiler.Tokens;

namespace ExpressionCompiler.Parser
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
                    yield return Paranthesis.Left(_tr.Position);
                }
                else if (next == ')')
                {
                    _tr.Read();
                    yield return Paranthesis.Right(_tr.Position);
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
            var sb         = new StringBuilder();
            int peek       = _tr.Peek();
            valueToken     = default;
            bool hasDigits = false;

            if (!IsValid((char)peek))
                return false;

            int startPosition = _tr.Position;

            while ((peek = _tr.Peek()) > -1)
            {
                char next = (char)peek;

                if (IsValid(next))
                {
                    _tr.Read();

                    if (char.IsDigit(next)) hasDigits = true;

                    if (next != '_')
                        sb.Append(next);
                }
                else
                    break;
            }

            if (hasDigits)
            {
                valueToken = new ValueToken((startPosition + 1, _tr.Position), sb.ToString());
                return true;
            }

            throw ParsingException.LiteralWithTwoSigns();
            //-----------------------------------------------------------------
            bool IsValid(char c)
            {
                if (char.IsDigit(c) || c == '.')                         return true;
                if (_tr.Position == 0 && (c == '-' || c == '+'))         return true;
                if (sb.Length > 0 && (c == 'e' || c == 'E' || c == '_')) return true;

                return false;
            }
        }
        //---------------------------------------------------------------------
        private Operation ReadOperation()
        {
            char operation = (char)_tr.Read();
            return (Operation)(operation, (_tr.Position, _tr.Position));
        }
        //---------------------------------------------------------------------
        private Token ReadIdentifier()
        {
            var sb = new StringBuilder();
            int peek;
            int startPosition = _tr.Position;

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

            Position position = (startPosition + 1, _tr.Position);

            if (Intrinsic.IsDefined(identifier))
                return (Intrinsic)(identifier, position);
            else if (Constant.IsDefined(identifier))
                return (Constant)(identifier, position);

            return new ParameterToken(position, identifier);
        }
    }
}