using libmonkey.ast;
using libmonkey.token;
using NUnit.Framework;

namespace libmonkey.test
{
    [TestFixture]
    public class AstTest
    {
        [Test]
        public void LetStatementToString()
        {
            var p = new Program();
            p.AddStatement(
                new LetStatement(
                    new Identifier(
                        new Token(Token.Tokens.Ident, "myVar")),
                    new Identifier(
                        new Token(Token.Tokens.Ident, "anotherVar"))));
            Assert.AreEqual("let myVar = anotherVar;", p.ToString());
        }
    }
}