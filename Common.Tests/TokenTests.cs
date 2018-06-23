using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void Token_Construction()
        {
            var token = new Token(Lexeme.Text, "Hello world");

            Assert.AreEqual(Lexeme.Text, token.Type);
            Assert.AreEqual("Hello world", token.Value);
        }

        [TestMethod]
        public void Token_ToString()
        {
            var expected = @"Type: ""Text"" Value: ""Hello world""";
            var token = new Token(Lexeme.Text, "Hello world");

            Assert.AreEqual(expected, token.ToString());
        }
    }
}
