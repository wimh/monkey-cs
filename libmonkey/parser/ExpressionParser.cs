﻿using System.Collections.Generic;
using libmonkey.ast;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public class ExpressionParser : IExpressionParser
    {
        private readonly Dictionary<Token.Tokens, PrefixParserFn> _prefixParserFns =
            new Dictionary<Token.Tokens, PrefixParserFn>();
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly Dictionary<Token.Tokens, InfixParserFn> _infixParserFns =
            new Dictionary<Token.Tokens, InfixParserFn>();

        public ExpressionParser()
        {
            RegisterPrefixParserFn(Token.Tokens.Ident, ParseIdentifier);
        }

        // ReSharper disable once UnusedParameter.Local
        public IExpression ParseExpression(IPeekableEnumerator<Token> tokens, Precedence precedence)
        {
            if (!_prefixParserFns.TryGetValue(tokens.PeekNext.Type, out var prefix))
                return null;

            var leftExp = prefix(tokens);
            return leftExp;
        }

        private void RegisterPrefixParserFn(Token.Tokens tokens, PrefixParserFn fn)
        {
            _prefixParserFns[tokens] = fn;
        }

        // ReSharper disable once UnusedMember.Local
        private void RegisterInfixParserFn(Token.Tokens tokens, InfixParserFn fn)
        {
            _infixParserFns[tokens] = fn;
        }

        private IExpression ParseIdentifier(IPeekableEnumerator<Token> tokens)
        {
            tokens.MoveNext();
            return new Identifier(tokens.Current);
        }
    }
}
