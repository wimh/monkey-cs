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
        private readonly IExpressionParser _expressionParser;
        private readonly List<string> _errors = new List<string>();

        public Parser(Lexer lexer, IExpressionParser expressionParser)
        {
            _lexer = lexer;
            _expressionParser = expressionParser;

            _expressionParser.ParserError += (s, a) => AddError(a);
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
                while (tokens.MoveNext())
                {
                    var statement = ParseStatement(tokens);
                    if (statement != null)
                        program.AddStatement(statement);
                }
            }

            return program;
        }

        private IStatement ParseStatement(IPeekableEnumerator<Token> tokens)
        {
            switch (tokens.Current?.Type)
            {
                case null:
                    return null;
                case Token.Tokens.Let:
                    return ParseLetStatement(tokens);
                case Token.Tokens.Return:
                    return ParseReturnStatement(tokens);
                default:
                    return ParseExpressionStatement(tokens);
            }
        }

        private IStatement ParseExpressionStatement(IPeekableEnumerator<Token> tokens)
        {
            var expression = _expressionParser.ParseExpression(tokens, Precedence.Lowest);
            if (expression == null)
                return null;

            var statement = new ExpressionStatement(expression);

            if (tokens.PeekNext?.Type == Token.Tokens.Semicolon)
                tokens.MoveNext();

            return statement;
        }

        private IStatement ParseLetStatement(IPeekableEnumerator<Token> tokens)
        {
            if (ExpectTokenAndAdvance(tokens, Token.Tokens.Let) == null)
                return null;

            var identifierToken = ExpectTokenAndAdvance(tokens, Token.Tokens.Ident);
            if (identifierToken == null)
                return null;
            var identifier = new Identifier(identifierToken.Literal);

            if (ExpectTokenAndAdvance(tokens, Token.Tokens.Assign) == null)
                return null;

            // TODO: parse expression
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == Token.Tokens.Semicolon)
                    return new LetStatement(identifier, null);
            }

            return null;
        }

        private IStatement ParseReturnStatement(IPeekableEnumerator<Token> tokens)
        {
            if (ExpectTokenAndAdvance(tokens, Token.Tokens.Return) == null)
                return null;

            // TODO: parse expression
            while (tokens.MoveNext())
            {
                if (tokens.Current.Type == Token.Tokens.Semicolon)
                    return new ReturnStatement(null);
            }

            return null;
        }

        // ReSharper disable once UnusedMember.Local
        private bool ExpectNextToken(IPeekableEnumerator<Token> tokens, Token.Tokens ident)
        {
            if (tokens.PeekNext?.Type != ident)
            {
                AddError(string.Format("expected {0}, got {1} instead",
                    ident,
                    tokens.PeekNext?.ToString() ?? "<EOF>"));
                return false;
            }

            return true;
        }

        private Token ExpectTokenAndAdvance(IPeekableEnumerator<Token> tokens, Token.Tokens ident)
        {
            var currentToken = tokens.Current;
            if (currentToken?.Type != ident)
            {
                AddError(string.Format("expected {0}, got {1} instead",
                    ident,
                    currentToken?.ToString() ?? "<EOF>"));
                return null;
            }

            tokens.MoveNext();
            return currentToken;
        }
    }
}