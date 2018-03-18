using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.parser;
using libmonkey.token;
using NUnit.Framework;

namespace libmonkey.test
{
    [TestFixture]
    public class ExpressionParserTest
    {
        [Test]
        public void TestIdentifierExpression()
        {
            var input = "foobar;";
            var tokens = new Lexer(input).Tokens.GetPeekableEnumerator();
            tokens.MoveNext();

            var sut = new ExpressionParser();

            var expression = sut.ParseExpression(tokens, Precedence.Lowest);

            var identifier = (IIdentifier)expression;
            Assert.AreEqual("foobar", identifier.Value);

            // verify if enumerator has not been advanced
            Assert.AreEqual(Token.Tokens.Ident, tokens.Current.Type);
        }

        [Test]
        public void TestIntegerLiteralExpression()
        {
            var input = "789;";
            var tokens = new Lexer(input).Tokens.GetPeekableEnumerator();
            tokens.MoveNext();

            var sut = new ExpressionParser();

            var expression = sut.ParseExpression(tokens, Precedence.Lowest);

            var identifier = (IIntegerLiteral)expression;
            Assert.AreEqual(789, identifier.Value);

            // verify if enumerator has not been advanced
            Assert.AreEqual(Token.Tokens.Int, tokens.Current.Type);
        }
    }
}