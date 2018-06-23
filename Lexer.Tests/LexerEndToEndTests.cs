using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lexer.Tests
{
    [TestClass]
    public class LexerEndToEndTests
    {
        [TestMethod]
        public void AllTextTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello world")
            };

            var template = "Hello world";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void ActionNoTextTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var template = "{{ name }}";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void TextWithActionTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var template = "Hello {{ name }}";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void TextWithActionNoActionWhitespaceTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var template = "Hello {{name}}";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void TextWithActionLotsOfActionWhitespaceTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Identifier, "name"),
                new Token(Lexeme.RightMeta, "}}")
            };

            var template = "Hello {{                        name      }}";

            var lexer = new Lexer(template);

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

            var template = "Hello {{ name }}!";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void MissingIdentifierTemplate()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Error, "Expected a valid Identifier")
            };

            var template = "Hello {{}}!";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }

        [TestMethod]
        public void UnexpectedEof()
        {
            var expected = new[]
            {
                new Token(Lexeme.Text, "Hello "),
                new Token(Lexeme.LeftMeta, "{{"),
                new Token(Lexeme.Error, "Unexpected Eof")
            };

            var template = "Hello {{ ";

            var lexer = new Lexer(template);

            lexer.Run();

            var tokens = lexer.Tokens.ToArray();

            CollectionAssert.AreEquivalent(expected, tokens);
        }
    }
}