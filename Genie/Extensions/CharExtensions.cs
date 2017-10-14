namespace Genie.Core.Extensions
{
    public static class CharExtensions
    {
        /// <summary>
        ///     Char is a vowel ?
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsVowel(this char c)
        {
            switch (c)
            {
                case 'a':
                case 'A':
                case 'e':
                case 'E':
                case 'i':
                case 'I':
                case 'o':
                case 'O':
                case 'u':
                case 'U':
                    return true;
                default:
                    return false;
            }
        }
    }
}