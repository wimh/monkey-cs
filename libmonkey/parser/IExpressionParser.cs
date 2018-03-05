using libmonkey.ast;
using libmonkey.token;
using libmonkey.utils;

namespace libmonkey.parser
{
    public interface IExpressionParser
    {
        IExpression ParseExpression(IPeekableEnumerator<Token> tokens, Precedence precedence);
    }
}