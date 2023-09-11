#if NETCOREAPP3_0_OR_GREATER
using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Kzrnm.Competitive.IO.Writer
{
    public class Utf8ConsoleWriterTests
    {
        private const int BufSize = 1 << 13;
        private readonly byte[] buffer = new byte[BufSize];
        private readonly string newLine;
        private readonly MemoryStream stream;
        private readonly Utf8ConsoleWriter cw;
        public Utf8ConsoleWriterTests()
        {
            stream = new MemoryStream(buffer);
            cw = new Utf8ConsoleWriter(stream);
            newLine = "\n";
        }
        private static byte[] ToBytes(string str)
        {
            var res = new byte[BufSize];
            for (int i = 0; i < str.Length; i++) res[i] = (byte)str[i];
            return res;
        }

        [Fact]
        public void WriteLineEmpty()
        {
            cw.WriteLine();
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes(newLine));
        }

        [Fact]
        public void Write()
        {
            cw.Write('A');
            cw.Write(-123456);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes("A-123456"));
        }

        [Fact]
        public void WriteLine()
        {
            cw.WriteLine(-123456);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes("-123456" + newLine));
        }

        [Fact]
        public void WriteLineJoinEmpty()
        {
            cw.WriteLineJoin();
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes(newLine));
        }

        [Fact]
        public void WriteLineJoin2()
        {
            cw.WriteLineJoin("foo", 1);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"foo 1{newLine}"));
        }

        [Fact]
        public void WriteLineJoin3()
        {
            cw.WriteLineJoin("foo", 1, -2L);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"foo 1 -2{newLine}"));
        }

        [Fact]
        public void WriteLineJoin4()
        {
            cw.WriteLineJoin("foo", 1, -2L, 'x');
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"foo 1 -2 x{newLine}"));
        }

        [Fact]
        public void WriteLineJoinMany()
        {
            cw.WriteLineJoin("foo", 1, -2L, 'x', "bar");
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"foo 1 -2 x bar{newLine}"));
        }

        [Fact]
        public void WriteLineJoinManySameType()
        {
            cw.WriteLineJoin(1, 2, 3, 4, 5);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinIEnumerable()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinList()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5).ToList());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinArray()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5).Select(i => $"{i}").ToArray());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesIEnumerable()
        {
            cw.WriteLines(Enumerable.Range(1, 5));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLinesList()
        {
            cw.WriteLines(Enumerable.Range(1, 5).ToList());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLineSpan()
        {
            cw.WriteLine("foobar".AsSpan());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes("foobar" + newLine));
        }

        [Fact]
        public void WriteLineJoinSpan()
        {
            cw.WriteLineJoin((Span<int>)Enumerable.Range(1, 5).ToArray());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesSpan()
        {
            cw.WriteLines((Span<int>)Enumerable.Range(1, 5).ToArray());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinReadOnlySpan()
        {
            cw.WriteLineJoin((ReadOnlySpan<int>)Enumerable.Range(1, 5).ToArray());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesReadOnlySpan()
        {
            cw.WriteLines((ReadOnlySpan<int>)Enumerable.Range(1, 5).ToArray());
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple2()
        {
            cw.WriteLineJoin((1, 2));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple3()
        {
            cw.WriteLineJoin((1, 2, 'a'));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 a{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple4()
        {
            cw.WriteLineJoin((1, 2, 'a', 4));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 a 4{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple5()
        {
            cw.WriteLineJoin((1, 2, 'a', 4, 5));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 a 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTupleClass()
        {
            cw.WriteLineJoin(Tuple.Create(1, 2, 'a', 4));
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 a 4{newLine}"));
        }

        [Fact]
        public void WriteLineCharArray()
        {
            var foo = new[] { 'f', 'o', 'o', 'b', 'a', 'r', 'b', 'a', 'z' };
            cw.WriteLine(foo);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"foobarbaz{newLine}"));
        }

        [Fact]
        public void WriteGridJaggedArray()
        {
            cw.WriteGrid(new int[][]
            {
                new int[]{ 1, 2, 3, },
                new int[]{ -1, -2, -3, },
                new int[]{ 4, 5, 6, },
            });
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
        }

        [Fact]
        public void WriteGridTuple()
        {
            cw.WriteGrid(new (int, int, int)[]
            {
                (1, 2, 3),
                (-1, -2, -3),
                (4, 5, 6),
            });
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
        }

        [Fact]
        public void WriteGridArray()
        {
            cw.WriteGrid(new int[,]
            {
                { 1, 2, 3, },
                { -1, -2, -3, },
                { 4, 5, 6, },
            });
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
        }

        [Fact]
        public void WriteLinesFormatter()
        {
            var pts = new[] {
                new Pt { x = 1, y = 2 },
                new Pt { x = -1, y = -2 },
                new Pt { x = 3, y = 4 },
                new Pt { x = -3, y = -4 },
                new Pt { x = 5, y = 6 },
                new Pt { x = -5, y = -6 },
            };
            cw.WriteLines(pts);
            buffer.Should().Equal(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.Should().Equal(ToBytes(@"1 2
-1 -2
3 4
-3 -4
5 6
-5 -6
".Replace("\r\n", "\n")));
        }
        struct Pt : IUtf8ConsoleWriterFormatter
        {
            public long x;
            public long y;

            public void Write(Utf8ConsoleWriter cw)
            {
                cw.Write(x);
                cw.Write(' ');
                cw.Write(y);
            }
            public override string ToString() => $"{x} {y}";
        }

        [Fact]
        public void EnsureLong()
        {
            var len = Utf8ConsoleWriter.BufSize - long.MinValue.ToString().Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(long.MinValue);
            cw.len.Should().Be(20);
            cw.Flush();
            buffer.Should().Equal(ToBytes(new string('1', len) + long.MinValue.ToString()));
        }
        [Fact]
        public void EnsureDouble()
        {
            var len = Utf8ConsoleWriter.BufSize - double.MinValue.ToString("F20").Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(double.MinValue);
            cw.len.Should().Be(331);
            cw.Flush();
            buffer.Should().Equal(ToBytes(new string('1', len) + double.MinValue.ToString("F20")));
        }
        [Fact]
        public void EnsureDecimal()
        {
            var len = Utf8ConsoleWriter.BufSize - decimal.MinValue.ToString("F20").Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(decimal.MinValue);
            cw.len.Should().Be(51);
            cw.Flush();
            buffer.Should().Equal(ToBytes(new string('1', len) + decimal.MinValue.ToString("F20")));
        }
    }
}
#endif
