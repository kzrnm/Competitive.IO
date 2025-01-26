using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Kzrnm.Competitive.IO.Reader
{
    internal static class Helpers
    {
        public static MemoryStream UTF8Stream(string str)
            => new SplitedStream(new UTF8Encoding(false).GetBytes(str));

        public static PropertyConsoleReader GetPropertyConsoleReader(string str)
            => new(UTF8Stream(str), new UTF8Encoding(false));
        public static ConsoleReader GetConsoleReader(string str)
            => new(UTF8Stream(str), new UTF8Encoding(false));
        public static PropertyConsoleReader GetPropertyConsoleReader(string str, int bufferSize)
            => new(UTF8Stream(str), new UTF8Encoding(false), bufferSize);
        public static ConsoleReader GetConsoleReader(string str, int bufferSize)
            => new(UTF8Stream(str), new UTF8Encoding(false), bufferSize);

        private class SplitedStream(byte[] buffer) : MemoryStream(buffer)
        {
            public override int Read(byte[] buffer, int offset, int count)
            {
                var pos = Position;
                var tmp = new byte[count];
                var read = base.Read(tmp, 0, count);
                if (read == 0)
                    return 0;

                var sp = tmp.AsSpan(0, read);
                var lineLength = sp.IndexOf((byte)'\n') + 1;
                if (lineLength > 0)
                {
                    Position = pos + lineLength;
                    sp = sp.Slice(0, lineLength);
                }
                else
                    lineLength = read;
                sp.CopyTo(buffer.AsSpan(offset, count));
                return lineLength;
            }
        }
    }
}
