using libmonkey.lexer;
using libmonkey.token;
using MoreLinq;
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

10 == 10;
9 != 9;
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
                new Token(Token.Tokens.Int, "10"),
                new Token(Token.Tokens.Eq, "=="),
                new Token(Token.Tokens.Int, "10"),
                new Token(Token.Tokens.Semicolon, ";"),
                new Token(Token.Tokens.Int, "9"),
                new Token(Token.Tokens.NotEq, "!="),
                new Token(Token.Tokens.Int, "9"),
                new Token(Token.Tokens.Semicolon, ";"),
            };

            var lexer = new Lexer(input);

            var tokens = expectedTokens.EquiZip(lexer.Tokens, (e, a) => new {Expected = e, Actual = a});
            foreach(var token in tokens)
            {
                Assert.AreEqual(token.Expected.Type,token.Actual.Type);
                Assert.AreEqual(token.Expected.Literal,token.Actual.Literal);
            }
        }

        [Test]
        public void TokensReturnsIndependentEnumerator()
        {
            var lexer = new Lexer("1 + 2");

            int count = 0;
            foreach (var unused1 in lexer.Tokens)
            {
                foreach (var unused2 in lexer.Tokens)
                {
                    count++;
                }
            }

            Assert.AreEqual(9, count);
        }
    }
}