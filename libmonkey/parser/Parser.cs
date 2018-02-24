using System.Collections.Generic;
using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private readonly List<string> _errors=new List<string>();

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
        }

        public IEnumerable<string> Errors => _errors;

        private void AddError(string error)
        {
            _errors.Add(error);
        }

        public IProgram ParseProgram()
        {
            var program = new Program();

            using (var tokens = _lexer.Tokens.GetPeekableEnumerator())
            {
                while (tokens.PeekNext != null)
                {
                    var statement = ParseStatement(tokens);
                    if (statement != null)
                        program.AddStatement(statement);
                    else
                        tokens.MoveNext();
                }
            }

            return program;
        }

        private IStatement ParseStatement(IPeekableEnumerator<Token> tokens)
        {
            switch (tokens.PeekNext?.Type)
            {
                case null:
                    return null;
                case Token.Tokens.Let:
                    return ParseLetStatement(tokens);
            }

            return null;
        }

        private IStatement ParseLetStatement(IPeekableEnumerator<Token> tokens)
        {
            if (!ExpectNextToken(tokens, Token.Tokens.Let))
                return null;

            if (!ExpectNextToken(tokens, Token.Tokens.Ident))
                return null;
            var identifier = new Identifier(tokens.Current);

            if (!ExpectNextToken(tokens, Token.Tokens.Assign))
                return null;

            if (!tokens.MoveNext()) return null;
            // TODO: parse expression
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == Token.Tokens.Semicolon)
                    return new LetStatement(identifier, null);
            }

            return null;
        }

        private bool ExpectNextToken(IPeekableEnumerator<Token> tokens, Token.Tokens ident)
        {
            if (tokens.PeekNext?.Type != ident)
            {
                AddError(string.Format("expected {0}, got {1} instead",
                    ident,
                    tokens.PeekNext?.ToString() ?? "<EOF>"));
                return false;
            }

            tokens.MoveNext();
            return true;
        }
    }
}