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
            Minus,
            Bang,
            Asterisk,
            Slash,
            
            Lt,
            Gt,
            
            Eq,
            NotEq,
            
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
            True,
            False,
            If,
            Else,
            Return,
        }

        private static readonly Dictionary<string, Tokens> Keywords = new Dictionary<string, Tokens>
        {
            {"fn", Tokens.Function},
            {"let", Tokens.Let},
            {"true", Tokens.True},
            {"false", Tokens.False},
            {"if", Tokens.If},
            {"else", Tokens.Else},
            {"return", Tokens.Return},
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

        public override string ToString()
        {
            return string.Format("{0} {1}", Type, Literal);
        }
    }
}