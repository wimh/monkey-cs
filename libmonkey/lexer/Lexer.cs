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
                    tok = new Token(Token.Tokens.Illegal, _ch.ToString());
                    break;
            }

            ReadChar();
            return tok;
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
    }
}