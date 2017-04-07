using System.IO;

namespace Genie.Base
{
    internal class Writer
    {
        internal static void Write(string content, string path)
        {
            File.WriteAllText(path, content);
        }
    }
}
