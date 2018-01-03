using libmonkey.token;

namespace libmonkey.lexer
{
    public class Lexer
    {
        private readonly string _input;
        private int _position;
        private int _readPosition;
        private char _ch;

        public Lexer(string input)
        {
            _input = input;
            
            ReadChar();
        }

        public Token NextToken()
        {
            Token tok;

            SkipWhitespace();
            
            switch (_ch)
            {
                case '=':
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
                case '{':
                    tok = new Token(Token.Tokens.LBrace, _ch.ToString());
                    break;
                case '}':
                    tok = new Token(Token.Tokens.RBrace, _ch.ToString());
                    break;
                case '\0':
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
            if (_readPosition >= _input.Length)
                _ch = '\0';
            else
                _ch = _input[_readPosition];

            _position = _readPosition;
            _readPosition++;
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