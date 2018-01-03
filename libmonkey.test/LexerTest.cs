using libmonkey.lexer;
using libmonkey.token;
using NUnit.Framework;

namespace libmonkey.test
{
    [TestFixture]
    public class LexerTest
    {
        [Test]
        public void TestNextToken()
        {
            var input = "=+(){},;";

            var expectedTokens = new[]
            {
                new Token(Token.Tokens.Assign, "="),
                new Token(Token.Tokens.Plus, "+"),
                new Token(Token.Tokens.LParen, "("),
                new Token(Token.Tokens.RParen, ")"),
                new Token(Token.Tokens.LBrace, "{"),
                new Token(Token.Tokens.RBrace, "}"),
                new Token(Token.Tokens.Comma, ","),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Eof, ""),
            };

            var lexer = new Lexer(input);

            foreach (var expectedToken in expectedTokens)
            {
                var token = lexer.NextToken();
                
                Assert.AreEqual(expectedToken.Type,token.Type);
                Assert.AreEqual(expectedToken.Literal,token.Literal);
            }
        }
    }
}