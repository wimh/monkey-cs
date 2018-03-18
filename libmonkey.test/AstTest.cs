using libmonkey.ast;
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
                    new Identifier("myVar"),
                    new Identifier("anotherVar")));
            Assert.AreEqual("let myVar = anotherVar;", p.ToString());
        }
    }
}