namespace Genie.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns the last letter of the string
        /// </summary>
        /// <param name="str">string to get the last letter</param>
        /// <returns></returns>
        public static char LastLetter(this string str)
        {
            return str[str.Length - 1];
        }

        /// <summary>
        ///     Converts a noun from singular to plural
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns></returns>
        public static string ToPlural(this string str)
        {
            var s = str.ToLower();
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (s.EndsWith("ch") || s.EndsWith("x") || s.EndsWith("s") || s.EndsWith("z"))
            {
                return str + "es";
            }

            if (s.Length > 1 && s.EndsWith("y"))
            {
                if (s.LastLetter().IsVowel())
                {
                    return str + "s";
                }

                return str.TrimEnd('y', 'Y') + "ies";
            }

            if (s.EndsWith("o"))
            {
                return str + "es";
            }

            if (s.EndsWith("f"))
            {
                return str.TrimEnd('f', 'F') + "ves";
            }

            if (s.EndsWith("fe"))
            {
                return str.TrimEnd('E', 'e').TrimEnd('f', 'F') + "ves";
            }

            if (s.EndsWith("us"))
            {
                return str.TrimEnd('s', 'S').TrimEnd('u', 'U') + "i";
            }

            if (s.EndsWith("um"))
            {
                return str.TrimEnd('m', 'M').TrimEnd('u', 'U') + "a";
            }

            if (s.EndsWith("ex"))
            {
                return str.TrimEnd('x', 'X').TrimEnd('e', 'E') + "ices";
            }

            if (s.EndsWith("ix"))
            {
                return str.TrimEnd('x', 'X').TrimEnd('i', 'I') + "ices";
            }

            if (s.EndsWith("is"))
            {
                return str.TrimEnd('s', 'S').TrimEnd('i', 'I') + "es";
            }

            if (s.EndsWith("on"))
            {
                return str.TrimEnd('n', 'N').TrimEnd('o', 'O') + "a";
            }

            return str + "s";
        }


        public static string FirstCharLower(this string str)
        {
            if (str.Length < 1)
            {
                return str;
            }

            var first = str[0];
            if (str.Length > 1)
            {
                return (first + "").ToLower() + str.Substring(1);
            }

            return (first + "").ToLower();
        }

        public static string ToFieldName(this string str)
        {
            return "_" + str.FirstCharLower();
        }
    }
}