using Common;

namespace Lexer
{
    public static class LexFunctions
    {
        private static readonly LexFunction UnexpectedEof = LexError("Unexpected Eof");
        private static readonly LexFunction ExpectedRightMeta = LexError("Expected Right Meta");
        private static readonly LexFunction ExpectedIdentifier = LexError("Expected a valid Identifier");

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

        public static LexFunction LexRightMeta(Lexer lexer)
        {
            // We don't need to validate we've found a left meta / check eof as we've already done that check
            // Just advance pos by two (length of left meta), emit and continue
            lexer.Next(2);
            lexer.Emit(Lexeme.RightMeta);
            return LexText;
        }

        public static LexFunction LexInsideAction(Lexer lexer)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!lexer.IgnoreWhitespace())
            {
                return UnexpectedEof;
            }

            return LexIdentifier;
        }

        public static LexFunction LexIdentifier(Lexer lexer)
        {
            while ("abcdefghijklmnopqrstuvwxyz".IndexOf(char.ToLower(lexer.Input[lexer.Pos])) >= 0)
            {
                if (lexer.Next())
                {
                    continue;
                }

                return UnexpectedEof;
            }

            // check have an actual identifier
            if (lexer.Pos > lexer.Start)
            {
                lexer.Emit(Lexeme.Identifier);
            }
            else
            {
                return ExpectedIdentifier;
            }

            if (!lexer.IgnoreWhitespace())
            {
                return UnexpectedEof;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (lexer.Input.Substring(lexer.Pos).StartsWith("}}"))
            {
                return LexRightMeta;
            }

            return ExpectedRightMeta;
        }

        public static LexFunction LexError(string message)
        {
            LexFunction _LexError(Lexer lexer)
            {
                lexer.EmitError(message);
                return null;
            }

            return _LexError;
        }
    }
}