using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lexer.Tests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void Lexer_Construction()
        {
            var lexer = new Lexer("Hello world");

            Assert.AreEqual("Hello world", lexer.Input);
            Assert.AreEqual(0, lexer.Start);
            Assert.AreEqual(0, lexer.Pos);
        }

        [TestMethod]
        public void Lexer_Next_IncrementsPosByOneByDefault()
        {
            var lexer = new Lexer();

            lexer.Next();

            Assert.AreEqual(1, lexer.Pos);
        }

        [TestMethod]
        public void Lexer_Next_IncrementsPosBySuppliedAmount()
        {
            var lexer = new Lexer();

            lexer.Next(2);

            Assert.AreEqual(2, lexer.Pos);
        }

        [TestMethod]
        public void Lexer_Next_ReturnsTrueWhenNotAtEof()
        {
            var lexer = new Lexer("Hello world");

            var result = lexer.Next();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Lexer_Next_ReturnsFalseWhenAtEof()
        {
            var lexer = new Lexer("Hello", pos: 4);

            var result = lexer.Next();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Lexer_TokenExists_ReturnsTrueWhenTokenExists()
        {
            var lexer = new Lexer(start: 0, pos: 4);

            Assert.IsTrue(lexer.TokenExists());
        }

        [TestMethod]
        public void Lexer_TokenExists_ReturnsFalseWhenNoTokenExists()
        {
            var lexer = new Lexer();

            Assert.IsFalse(lexer.TokenExists());
        }

        [TestMethod]
        public void Lexer_Emit_EmitsTokenCorrectly()
        {
            var lexer = new Lexer("Hello", 0, 5);

            lexer.Emit(Lexeme.Text);

            var tokens = lexer.Tokens.ToArray();

            Assert.AreEqual(1, tokens.Length);
            Assert.AreEqual(Lexeme.Text, tokens[0].Type);
            Assert.AreEqual("Hello", tokens[0].Value);
        }

        [TestMethod]
        public void Lexer_Emit_AdvancesStart()
        {
            var lexer = new Lexer("Hello", 0, 5);

            lexer.Emit(Lexeme.Text);

            Assert.AreEqual(5, lexer.Start);
        }
    }
}