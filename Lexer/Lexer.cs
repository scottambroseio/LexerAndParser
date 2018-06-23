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

        internal Lexer(string input = "", int start = 0, int pos = 0): this(input)
        {
            Input = input;
            Start = start;
            Pos = pos;
        }

        public string Input { get; }
        public int Start { get; private set; }
        public int Pos { get; private set; }

        public IEnumerable<Token> Tokens
        {
            get { return tokens; }
        }

        public bool Next(int toIncrease = 1)
        {
            Pos += toIncrease;

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

        private void SetStartToPos()
        {
            Start = Pos;
        }
    }
}