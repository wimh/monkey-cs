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
            const string input = @"let five = 5;
let ten = 10;

let add = fn(x,y) {
  x + y;
};

let result = add(five, ten);
!-/*5;
5 < 10 > 5;

if (5 < 10) {
  return true;
} else {
  return false;
}
";

            var expectedTokens = new[]
            {
                new Token(Token.Tokens.Let, "let"),
                new Token(Token.Tokens.Ident, "five"),
                new Token(Token.Tokens.Assign, "="),
                new Token(Token.Tokens.Int, "5"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Let, "let"),
                new Token(Token.Tokens.Ident, "ten"),
                new Token(Token.Tokens.Assign, "="),
                new Token(Token.Tokens.Int, "10"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Let, "let"),
                new Token(Token.Tokens.Ident, "add"),
                new Token(Token.Tokens.Assign, "="),
                new Token(Token.Tokens.Function, "fn"),
                new Token(Token.Tokens.LParen, "("),
                new Token(Token.Tokens.Ident, "x"),
                new Token(Token.Tokens.Comma, ","),
                new Token(Token.Tokens.Ident, "y"),
                new Token(Token.Tokens.RParen, ")"),
                new Token(Token.Tokens.LBrace, "{"),
                new Token(Token.Tokens.Ident, "x"),
                new Token(Token.Tokens.Plus, "+"),
                new Token(Token.Tokens.Ident, "y"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.RBrace, "}"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Let, "let"),
                new Token(Token.Tokens.Ident, "result"),
                new Token(Token.Tokens.Assign, "="),
                new Token(Token.Tokens.Ident, "add"),
                new Token(Token.Tokens.LParen, "("),
                new Token(Token.Tokens.Ident, "five"),
                new Token(Token.Tokens.Comma, ","),
                new Token(Token.Tokens.Ident, "ten"),
                new Token(Token.Tokens.RParen, ")"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Bang, "!"),
                new Token(Token.Tokens.Minus, "-"),
                new Token(Token.Tokens.Slash, "/"),
                new Token(Token.Tokens.Asterisk, "*"),
                new Token(Token.Tokens.Int, "5"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Int, "5"),
                new Token(Token.Tokens.Lt, "<"),
                new Token(Token.Tokens.Int, "10"),
                new Token(Token.Tokens.Gt, ">"),
                new Token(Token.Tokens.Int, "5"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.If, "if"),
                new Token(Token.Tokens.LParen, "("),
                new Token(Token.Tokens.Int, "5"),
                new Token(Token.Tokens.Lt, "<"),
                new Token(Token.Tokens.Int, "10"),
                new Token(Token.Tokens.RParen, ")"),
                new Token(Token.Tokens.LBrace, "{"),
                new Token(Token.Tokens.Return, "return"),
                new Token(Token.Tokens.True, "true"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.RBrace, "}"),
                new Token(Token.Tokens.Else, "else"),
                new Token(Token.Tokens.LBrace, "{"),
                new Token(Token.Tokens.Return, "return"),
                new Token(Token.Tokens.False, "false"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.RBrace, "}"),
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