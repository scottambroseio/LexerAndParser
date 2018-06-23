namespace Common
{
    public class Token
    {
        public Token(Lexeme type, string value)
        {
            Type = type;
            Value = value;
        }

        public Lexeme Type { get; }
        public string Value { get; }

        public override string ToString()
        {
            return $"Type: \"{Type}\" Value: \"{Value}\"";
        }
    }
}