using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using FluentAssertions.Collections;

namespace Kzrnm.Competitive.IO.Reader
{
    internal static class Helpers
    {
        public static MemoryStream UTF8Stream(string str)
            => new SplitedStream(new UTF8Encoding(false).GetBytes(str));

        public static PropertyConsoleReader GetPropertyConsoleReader(string str)
            => new(UTF8Stream(str + "\n"), new UTF8Encoding(false));
        public static ConsoleReader GetConsoleReader(string str)
            => new(UTF8Stream(str + "\n"), new UTF8Encoding(false));
        public static AndConstraint<GenericCollectionAssertions<char[]>> Equal(
            this GenericCollectionAssertions<char[]> a, params string[] s)
        {
            var r = new char[s.Length][];
            for (int i = 0; i < s.Length; i++)
                r[i] = s[i].ToCharArray();
            return a.Equal(s, (c, d) => c.SequenceEqual(d));
        }

        private class SplitedStream : MemoryStream
        {
            public SplitedStream(byte[] buffer) : base(buffer) { }

            public override int Read(byte[] buffer, int offset, int count)
            {
                var pos = Position;
                var tmp = new byte[count];
                var read = base.Read(tmp, 0, count);
                if (read == 0)
                    throw new Exception("if in console, console is freezed.");

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
