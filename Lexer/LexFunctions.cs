using Common;

namespace Lexer
{
    public static class LexFunctions
    {
        public static LexFunction LexText(Lexer lexer)
        {
            while (true)
            {
                if (lexer.Input.Substring(lexer.Pos).StartsWith("{{"))
                {
                    if (lexer.TokenExists())
                    {
                        lexer.Emit(Lexeme.Text);
                    }

                    return LexLeftMeta;
                }

                if (lexer.Next())
                {
                    continue;
                }

                // Found eof
                // emit any text up to this point as the final text token
                if (lexer.TokenExists())
                {
                    lexer.Emit(Lexeme.Text);
                }

                // Return terminal state to terminate the state machine
                return null;
            }
        }

        public static LexFunction LexLeftMeta(Lexer lexer)
        {
            // We don't need to validate we've found a left meta / check eof as we've already done that check
            // Just advance pos by two (length of left meta), emit and continue
            lexer.Next(2);
            lexer.Emit(Lexeme.LeftMeta);
            return LexInsideAction;
        }

        public static LexFunction LexInsideAction(Lexer lexer)
        {
            return null;
        }
    }
}