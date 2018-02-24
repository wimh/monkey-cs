using System.Linq;
using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.parser;
using NUnit.Framework;

namespace libmonkey.test
{
    [TestFixture]
    public class ParserTest
    {
        [Test]
        public void TestLetStatement()
        {
            string input = @"let x = 5;
                             let y = 10;
                             let foobar = 838485;
                            ";

            var lexer = new Lexer(input);

            var sut = new Parser(lexer);
            var program = sut.ParseProgram();

            Assert.NotNull(program);

            var statements = program.Statements.ToArray();
            Assert.AreEqual(3, statements.Length);

            AssertLetStatement(statements[0], "x");
            AssertLetStatement(statements[1], "y");
            AssertLetStatement(statements[2], "foobar");
        }

        private static void AssertLetStatement(IStatement statement, string ident)
        {
            var letstatement = statement as LetStatement;
            Assert.NotNull(letstatement);
            Assert.AreEqual(ident, letstatement.Identifier.Value);
            Assert.IsNull(letstatement.Expression); // TODO
        }

        [Test]
        [TestCase("let x 5;", "expected Assign, got Int 5 instead")]
        [TestCase("let = 10;", "expected Ident, got Assign = instead")]
        [TestCase("let 838485;", "expected Ident, got Int 838485 instead")]
        [TestCase("let", "expected Ident, got <EOF> instead")]
        public void TestLetStatementParseErrors(string input, string expectedError)
        {
            var sut = new Parser(new Lexer(input));

            var program = sut.ParseProgram();
            Assert.NotNull(program);
            Assert.AreEqual(0, program.Statements.Count());

            Assert.AreEqual(expectedError, sut.Errors.First());
        }
    }
}