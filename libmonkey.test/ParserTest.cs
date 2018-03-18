using System.Linq;
using libmonkey.ast;
using libmonkey.lexer;
using libmonkey.parser;
using libmonkey.token;
using libmonkey.utils;
using Moq;
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

            var sut = new Parser(lexer, new ExpressionParser());
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
            var sut = new Parser(new Lexer(input), new ExpressionParser());

            var program = sut.ParseProgram();
            Assert.NotNull(program);

            if (program.Statements.Any())
                Assert.IsNotInstanceOf<LetStatement>(program.Statements.First());

            Assert.AreEqual(expectedError, sut.Errors.First());
        }

        [Test]
        [TestCase("return 5;")]
        [TestCase("return x;")]
        [TestCase("return sqrt(9);")]
        public void ParseReturnStatement(string input)
        {
            var sut = new Parser(new Lexer(input), new ExpressionParser());
            var program = sut.ParseProgram();

            Assert.NotNull(program);
            Assert.AreEqual(0, sut.Errors.Count());

            var statements = program.Statements.ToArray();
            Assert.AreEqual(1, statements.Length);

            var returnStatement = statements[0] as ReturnStatement;
            Assert.NotNull(returnStatement);
            Assert.IsNull(returnStatement.Expression); // TODO
        }

        [Test]
        public void TestExpression()
        {
            var input = "5;";

            var expression = new Mock<IExpression>().Object;
            var expressionParser = new Mock<IExpressionParser>();
            expressionParser
                .Setup(p => p.ParseExpression(It.IsAny<IPeekableEnumerator<Token>>(), It.IsAny<Precedence>()))
                .Callback((IPeekableEnumerator<Token> enu, Precedence _) =>
                {
                    Assert.AreEqual(Token.Tokens.Int, enu.Current.Type);
                })
                .Returns(expression);

            var sut = new Parser(new Lexer(input), expressionParser.Object);
            var program = sut.ParseProgram();

            var statement = program.Statements.OfType<ExpressionStatement>().Single();
            Assert.AreSame(expression, statement.Expression);
        }
    }
}