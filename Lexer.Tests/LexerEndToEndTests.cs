using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lexer.Tests
{
    [TestClass]
    public class LexerEndToEndTests
    {
        private const string textTemplate = "Hello world";
        private const string textActionTemplate = "Hello {{ name }}";
        private const string textActionTextTemplate = "Hello {{ name }}!";
        private const string actionTemplate = "{{ greeting }}";
        private const string actionTextTemplate = "{{ greeting }}!";
        private const string actionTextActionTemplate = "{{ firstName }} {{ lastName }}";
        private const string actionNoWhitespaceTemplate = "{{greeting}}";
        private const string actionLotsOfWhitespaceTemplate = "{{     greeting                           }}";

        [TestMethod]
        public void TextTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello world")
            };

            var lexer = new Lexer(textTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void TextActionTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var lexer = new Lexer(textActionTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void TextActionTextTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}"),
                new Token(Lexeme.Text, "!")
            };

            var lexer = new Lexer(textActionTextTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "greeting"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var lexer = new Lexer(actionTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionText()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "greeting"),
                new Token(Lexeme.RightMeta, "}}"),
                new Token(Lexeme.Text, "!")
            };

            var lexer = new Lexer(actionTextTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionTextAction()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "firstName"),
                new Token(Lexeme.RightMeta, "}}"),
                new Token(Lexeme.Text, " "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "lastName"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var lexer = new Lexer(actionTextActionTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionNoWhitespace()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "greeting"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var lexer = new Lexer(actionNoWhitespaceTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionLotsOfWhitespaceTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "greeting"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var lexer = new Lexer(actionLotsOfWhitespaceTemplate);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }
    }
}