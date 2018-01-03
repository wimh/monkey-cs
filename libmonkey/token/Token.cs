using System.Collections.Generic;

namespace libmonkey.token
{
    public struct Token
    {
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

        private static readonly Dictionary<string, Tokens> Keywords = new Dictionary<string, Tokens>
        {
            {"fn", Tokens.Function},
            {"let", Tokens.Let},
        };

        public readonly Tokens Type;
        public readonly string Literal;

        public Token(Tokens type, string literal)
        {
            Type = type;
            Literal = literal;
        }

        public static Tokens LookupIdent(string ident)
        {
            if (Keywords.ContainsKey(ident))
                return Keywords[ident];

            return Tokens.Ident;
        }
    }
}