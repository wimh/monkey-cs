namespace libmonkey.token
{
    public struct Token
    {
        // in case I do need strings: https://stackoverflow.com/questions/424366/c-sharp-string-enums
        public enum Tokens {
            Illegal,
            Eof,
            
            // Identifiers + literals
            Ident, // add, foobar, x, y, ...
            Int,
            
            // Operators
            Assign,
            Plus,
            
            // Delimiters
            Comma,
            Semicolon,
            
            LParen,
            RParen,
            LBrace,
            RBrace,
            
            // Keywords
            Function,
            Let,
        }

        public Tokens Type;
        public string Literal;

        public Token(Tokens type, string literal)
        {
            Type = type;
            Literal = literal;
        }
    }
}