using libmonkey.ast;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public delegate IExpression InfixParserFn(IPeekableEnumerator<Token> tokens, IExpression expression);
}