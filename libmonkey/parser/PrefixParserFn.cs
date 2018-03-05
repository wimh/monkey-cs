using libmonkey.ast;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public delegate IExpression PrefixParserFn(IPeekableEnumerator<Token> tokens);
}