/// <summary>
/// Just Tools By RemziStudios
/// </summary>
namespace JustTools.MathUtility
{
    public static class AutoParse
    {
        public static int? IntParse(string Content)
        {
            if (string.IsNullOrEmpty(Content)) { return null; }
          
            try
            {
                return int.Parse(ClearString(Content, '-'));
            }
            catch
            {
                return null;
            }
        }

        public static long? LongParse(string Content)
        {
            if (string.IsNullOrEmpty(Content)) { return null; }

            try
            {
                return long.Parse(ClearString(Content, '-'));
            }
            catch
            {
                return null;
            }
        }

        public static uint? UIntParse(string Content)
        {
            if (string.IsNullOrEmpty(Content)) { return null; }

            try
            {
                return uint.Parse(ClearString(Content));
            }
            catch
            {
                return null;
            }
        }
        
        public static float? FloatParse (string Content)
        {
            if (string.IsNullOrEmpty(Content)) { return null; }

            try
            {
                return float.Parse(ClearString(Content, '.', '-'));
            }
            catch
            {
                return null;
            }
        }

        public static ulong? ULongParse(string Content)
        {
            if (string.IsNullOrEmpty(Content)) { return null; }

            try
            {
                return ulong.Parse(ClearString(Content));
            }
            catch
            {
                return null;
            }
        }

        private static string ClearString(string Content, params char[] AdditionalChars)
        {
            string result = "";

            for (int i = 0; i < Content.Length; i++)
            {
                if (char.IsDigit(Content[i]) || CharMatches(Content[i], AdditionalChars))
                {
                    result += Content[i];
                }
            }

            return result;
        }

        private static bool CharMatches(char Char, char[] Chars)
        {
            for (int i = 0; i < Chars.Length; i++)
            {
                if (Char == Chars[i])
                {
                    return true;
                }
            }

            return false;
        }
        

    }
}
