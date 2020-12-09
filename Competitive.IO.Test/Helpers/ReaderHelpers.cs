using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO.Helpers
{
    internal static class ReaderHelpers
    {
        public static MemoryStream UTF8Stream(string str)
            => new MemoryStream(new UTF8Encoding(false).GetBytes(str));

        public static PropertyConsoleReader GetPropertyConsoleReader(string str)
            => new PropertyConsoleReader(UTF8Stream(str), new UTF8Encoding(false));
        public static ConsoleReader GetConsoleReader(string str)
            => new ConsoleReader(UTF8Stream(str), new UTF8Encoding(false));
    }
}
