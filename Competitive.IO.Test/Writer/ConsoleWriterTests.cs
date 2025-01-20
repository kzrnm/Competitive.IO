using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Kzrnm.Competitive.IO.Writer
{
    public class ConsoleWriterTests
    {
        private const int BufSize = 1 << 8;
        private readonly byte[] buffer = new byte[BufSize];
        private readonly string newLine;
        private readonly MemoryStream stream;
        private readonly ConsoleWriter cw;
        public ConsoleWriterTests()
        {
            stream = new MemoryStream(buffer);
            cw = new ConsoleWriter(stream, new UTF8Encoding(false));
            newLine = cw.StreamWriter.NewLine;
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
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes(newLine));
        }

        [Fact]
        public void Write()
        {
            cw.Write('A');
            cw.Write(-123456);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("A-123456"));
        }

        [Fact]
        public void WriteLine()
        {
            cw.WriteLine(-123456);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("-123456" + newLine));
        }

        [Fact]
        public void WriteLineJoinEmpty()
        {
            cw.WriteLineJoin();
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes(newLine));
        }

        [Fact]
        public void WriteLineJoin2()
        {
            cw.WriteLineJoin("foo", 1);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"foo 1{newLine}"));
        }

        [Fact]
        public void WriteLineJoin3()
        {
            cw.WriteLineJoin("foo", 1, -2L);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"foo 1 -2{newLine}"));
        }

        [Fact]
        public void WriteLineJoin4()
        {
            cw.WriteLineJoin("foo", 1, -2L, 'x');
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"foo 1 -2 x{newLine}"));
        }

        [Fact]
        public void WriteLineJoinMany()
        {
            cw.WriteLineJoin("foo", 1, -2L, 'x', "bar");
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"foo 1 -2 x bar{newLine}"));
        }

        [Fact]
        public void WriteLineJoinManySameType()
        {
            cw.WriteLineJoin(1, 2, 3, 4, 5);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinIEnumerable()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinList()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5).ToList());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinArray()
        {
            cw.WriteLineJoin(Enumerable.Range(1, 5).Select(i => $"{i}").ToArray());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesIEnumerable()
        {
            cw.WriteLines(Enumerable.Range(1, 5));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLinesList()
        {
            cw.WriteLines(Enumerable.Range(1, 5).ToList());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteGridJaggedArray()
        {
            cw.WriteGrid([
                [1, 2, 3, ],
                [-1, -2, -3, ],
                [4, 5, 6, ],
            ]);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
        }

#if !NETFRAMEWORK
        [Fact]
        public void WriteGridTuple()
        {
            cw.WriteGrid([
                (1, 2, 3),
                (-1, -2, -3),
                (4, 5, 6),
            ]);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
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
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
        }

        [Fact]
        public void WriteLineSpan()
        {
            cw.WriteLine("foobar".AsSpan());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("foobar" + newLine));
        }

        [Fact]
        public void WriteLineJoinSpan()
        {
            cw.WriteLineJoin((Span<int>)Enumerable.Range(1, 5).ToArray());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesSpan()
        {
            cw.WriteLines((Span<int>)Enumerable.Range(1, 5).ToArray());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinReadOnlySpan()
        {
            cw.WriteLineJoin((ReadOnlySpan<int>)Enumerable.Range(1, 5).ToArray());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3 4 5{newLine}"));
        }

        [Fact]
        public void WriteLinesReadOnlySpan()
        {
            cw.WriteLines((ReadOnlySpan<int>)Enumerable.Range(1, 5).ToArray());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1\n2\n3\n4\n5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple2()
        {
            cw.WriteLineJoin((1, 2));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple3()
        {
            cw.WriteLineJoin((1, 2, 'a'));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 a{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple4()
        {
            cw.WriteLineJoin((1, 2, 'a', 4));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 a 4{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTuple5()
        {
            cw.WriteLineJoin((1, 2, 'a', 4, 5));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 a 4 5{newLine}"));
        }

        [Fact]
        public void WriteLineJoinTupleClass()
        {
            cw.WriteLineJoin(Tuple.Create(1, 2, 'a', 4));
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 a 4{newLine}"));
        }
#endif
    }
}
