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
    }
}