using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lexer.Tests
{
    [TestClass]
    public class LexFunctionsTests
    {
        [TestMethod]
        public void LexText_EmitsSingleTextTokenOnAllTextInput()
        {
            var lexer = new Lexer("Hello world");

            LexFunctions.LexText(lexer);

            var tokens = lexer.Tokens.ToArray();

            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(Lexeme.Text, tokens[0].Type);
            Assert.AreEqual("Hello world", tokens[0].Value);
        }

        [TestMethod]
        public void LexText_ReturnsNullWhenFoundEof()
        {
            var lexer = new Lexer("Hello world");

            var nextLexFunction = LexFunctions.LexText(lexer);

            Assert.IsNull(nextLexFunction);
        }

        [TestMethod]
        public void LexText_EmitsNoTextTokenWhenTextStartsWithLeftMeta()
        {
            var lexer = new Lexer("{{ name }}");

            LexFunctions.LexText(lexer);

            var result = lexer.Tokens.ToArray();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void LexText_ReturnsLexLeftMetaWhenFoundLeftMeta()
        {
            var lexer = new Lexer("{{ name }}");

            var nextLexFunction = LexFunctions.LexText(lexer);

            Assert.AreEqual(LexFunctions.LexLeftMeta, nextLexFunction);
        }

        [TestMethod]
        public void LexText_EmitsSingleTextTokenForTextPrecedingLeftMeta()
        {
            var lexer = new Lexer("Hello {{ name }}");

            LexFunctions.LexText(lexer);

            var tokens = lexer.Tokens.ToArray();

            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(Lexeme.Text, tokens[0].Type);
            Assert.AreEqual("Hello ", tokens[0].Value);
        }

        [TestMethod]
        public void LexLeftMeta_AdvancesPosByTwo()
        {
            var lexer = new Lexer("{{ name }}");

            LexFunctions.LexLeftMeta(lexer);

            Assert.AreEqual(2, lexer.Pos);
        }

        [TestMethod]
        public void LexLeftMeta_AdvancesStartToPos()
        {
            var lexer = new Lexer("{{ name }}");

            LexFunctions.LexLeftMeta(lexer);

            Assert.AreEqual(lexer.Pos, lexer.Start);
        }

        [TestMethod]
        public void LexLeftMeta_EmitsLeftMeta()
        {
            var lexer = new Lexer("{{ name }}");

            LexFunctions.LexLeftMeta(lexer);

            var tokens = lexer.Tokens.ToArray();

            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(Lexeme.LeftMeta, tokens[0].Type);
            Assert.AreEqual("{{", tokens[0].Value);
        }

        [TestMethod]
        public void LexLeftMeta_ReturnsLexInsideAction()
        {
            var lexer = new Lexer("{{ name }}");

            var nextLexFunction = LexFunctions.LexLeftMeta(lexer);

            Assert.AreEqual(LexFunctions.LexInsideAction, nextLexFunction);
        }
    }
}