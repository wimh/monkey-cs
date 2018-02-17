using System.Collections;
using System.Collections.Generic;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.lexer
{
    public class Lexer
    {
        private readonly string _input;
        private int _position;
        private int _readPosition;
        private char _ch;

        private const char EofChar = '\0';

        public Lexer(string input)
        {
            _input = input;
            Tokens = new TokensImp(this);

            ReadChar();
        }

        public IPeekableEnumerable<Token> Tokens { get; }

        private Token NextToken()
        {
            Token tok;

            SkipWhitespace();

            switch (_ch)
            {
                case '=':
                    if (PeekChar() == '=')
                    {
                        var ch1 = _ch.ToString();
                        ReadChar();
                        tok = new Token(Token.Tokens.Eq, ch1 + _ch);
                    }
                    else
                        tok = new Token(Token.Tokens.Assign, _ch.ToString());
                    break;
                case ';':
                    tok = new Token(Token.Tokens.Semicolon, _ch.ToString());
                    break;
                case '(':
                    tok = new Token(Token.Tokens.LParen, _ch.ToString());
                    break;
                case ')':
                    tok = new Token(Token.Tokens.RParen, _ch.ToString());
                    break;
                case ',':
                    tok = new Token(Token.Tokens.Comma, _ch.ToString());
                    break;
                case '+':
                    tok = new Token(Token.Tokens.Plus, _ch.ToString());
                    break;
                case '-':
                    tok = new Token(Token.Tokens.Minus, _ch.ToString());
                    break;
                case '!':
                    if (PeekChar() == '=')
                    {
                        var ch1 = _ch.ToString();
                        ReadChar();
                        tok = new Token(Token.Tokens.NotEq, ch1 + _ch);
                    }
                    else
                        tok = new Token(Token.Tokens.Bang, _ch.ToString());
                    break;
                case '/':
                    tok = new Token(Token.Tokens.Slash, _ch.ToString());
                    break;
                case '*':
                    tok = new Token(Token.Tokens.Asterisk, _ch.ToString());
                    break;
                case '<':
                    tok = new Token(Token.Tokens.Lt, _ch.ToString());
                    break;
                case '>':
                    tok = new Token(Token.Tokens.Gt, _ch.ToString());
                    break;
                case '{':
                    tok = new Token(Token.Tokens.LBrace, _ch.ToString());
                    break;
                case '}':
                    tok = new Token(Token.Tokens.RBrace, _ch.ToString());
                    break;
                case EofChar:
                    tok = new Token(Token.Tokens.Eof, "");
                    break;
                default:
                    if (IsLetter(_ch))
                    {
                        var identifier = ReadIdentifier();
                        tok = new Token(Token.LookupIdent(identifier), identifier);
                        return tok;
                    }
                    else if (IsDigit(_ch))
                    {
                        var number = ReadNumber();
                        tok = new Token(Token.Tokens.Int, number);
                        return tok;
                    }
                    else
                        tok = new Token(Token.Tokens.Illegal, _ch.ToString());
                    break;
            }

            ReadChar();
            return tok;
        }

        private class TokensImp : IPeekableEnumerable<Token>
        {
            private readonly Lexer _parent;

            internal TokensImp(Lexer parent)
            {
                _parent = parent;
            }

            public IPeekableEnumerator<Token> GetPeekableEnumerator()
            {
                return new PeekableEnumerator<Token>(GetEnumerator());
            }

            public IEnumerator<Token> GetEnumerator()
            {
                for (var token = _parent.NextToken(); token.Type != Token.Tokens.Eof; token = _parent.NextToken())
                {
                    yield return token;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private void SkipWhitespace()
        {
            while (_ch == ' ' || _ch == '\t' || _ch == '\n' || _ch == '\r')
                ReadChar();
        }

        private string ReadIdentifier()
        {
            var startPosition = _position;
            while(IsLetter(_ch))
                ReadChar();
            return _input.Substring(startPosition, _position - startPosition);
        }

        private string ReadNumber()
        {
            var startPosition = _position;
            while(IsDigit(_ch))
                ReadChar();
            return _input.Substring(startPosition, _position - startPosition);
        }

        private void ReadChar()
        {
            _ch = PeekChar();

            _position = _readPosition;
            _readPosition++;
        }

        private char PeekChar()
        {
            if (_readPosition >= _input.Length)
                return EofChar;
            return _input[_readPosition];
        }

        private static bool IsLetter(char ch)
        {
            return 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || ch == '_';
        }

        private static bool IsDigit(char ch)
        {
            return '0' <= ch && ch <= '9';
        }
    }
}