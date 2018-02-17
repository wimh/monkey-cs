using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public class Parser
    {
        private readonly Lexer _lexer;

        public Parser(Lexer lexer)
        {
            _lexer = lexer;
        }

        public IProgram ParseProgram()
        {
            var program = new Program();

            using (var tokens = _lexer.Tokens.GetPeekableEnumerator())
            {
                while (tokens.PeekNext != null)
                {
                    program.AddStatement(ParseStatement(tokens));
                }
            }

            return program;
        }

        private static IStatement ParseStatement(IPeekableEnumerator<Token> tokens)
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

        private static IStatement ParseLetStatement(IPeekableEnumerator<Token> tokens)
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

        private static bool ExpectNextToken(IPeekableEnumerator<Token> tokens, Token.Tokens ident)
        {
            if (tokens.PeekNext?.Type != ident)
                return false;
            tokens.MoveNext();
            return true;
        }
    }
}