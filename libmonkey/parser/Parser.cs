using System.Collections.Generic;
using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.token;

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

            using (var tokens = _lexer.Tokens.GetEnumerator())
            {
                while (tokens.MoveNext())
                {
                    program.AddStatement(ParseStatement(tokens));
                }
            }

            return program;
        }

        private static IStatement ParseStatement(IEnumerator<Token> tokens)
        {
            switch (tokens.Current.Type)
            {
                    case Token.Tokens.Let:
                        return ParseLetStatement(tokens);
            }

            return null;
        }

        private static IStatement ParseLetStatement(IEnumerator<Token> tokens)
        {
            if (tokens.Current.Type != Token.Tokens.Let)
                return null;

            if (!tokens.MoveNext()) return null;
            if (tokens.Current.Type != Token.Tokens.Ident)
                return null;
            var identifier = new Identifier(tokens.Current);

            if (!tokens.MoveNext()) return null;
            if (tokens.Current.Type != Token.Tokens.Assign)
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
    }
}