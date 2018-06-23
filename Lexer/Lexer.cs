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

            if (Pos < Input.Length)
            {
                return true;
            }

            Pos = Input.Length;
            return false;
        }

        public bool TokenExists()
        {
            return Pos > Start;
        }

        public bool IgnoreWhitespace()
        {
            if (Pos >= Input.Length)
            {
                return false;
            }

            do
            {
                if (Input[Pos] == ' ')
                {
                    continue;
                }

                SetStartToPos();
                return true;
            } while (Next());

            // unexpected eof
            return false;
        }

        public void Emit(Lexeme type)
        {
            var value = Input.Substring(Start, Pos - Start);

            tokens.Add(new Token(type, value));

            SetStartToPos();
        }

        public void EmitError(string value)
        {
            tokens.Add(new Token(Lexeme.Error, value));
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