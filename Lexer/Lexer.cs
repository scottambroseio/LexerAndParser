using System.Collections.Generic;
using Common;

namespace Lexer
{
    public class Lexer
    {
        private readonly ICollection<Token> tokens;

        public Lexer(string input)
        {
            Input = input;
            tokens = new List<Token>();
        }

        public string Input { get; }
        public int Start { get; private set; }
        public int Pos { get; private set; }

        public IEnumerable<Token> Tokens
        {
            get { return tokens; }
        }

        private void IncrementPos(int toIncrease = 1)
        {
            Pos += toIncrease;
        }

        private void SetStartToPos()
        {
            Start = Pos;
        }

        public bool Next()
        {
            IncrementPos();

            return Pos < Input.Length;
        }

        public bool TokenExists()
        {
            return Pos > Start;
        }

        public void Emit(Lexeme type)
        {
            var value = Input.Substring(Start, Pos - Start);

            tokens.Add(new Token(type, value));

            SetStartToPos();
        }

        public void Run()
        {
            // Run the state machine
            LexFunction function = LexFunctions.LexText;
            while (function != null)
            {
                function = function(this);
            }
        }
    }
}