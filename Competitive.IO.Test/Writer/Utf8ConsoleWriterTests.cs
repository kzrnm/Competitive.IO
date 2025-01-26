#if NETCOREAPP3_0_OR_GREATER
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;
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
            var rt = new byte[BufSize];
            Encoding.UTF8.GetBytes(str, rt);
            return rt;
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
            cw.Write(int.MaxValue).Write("int".ToCharArray()).Write(int.MinValue);
            cw.Write(long.MaxValue).Write("long".ToCharArray()).Write(long.MinValue);
            cw.Write(uint.MaxValue).Write("uint".ToCharArray()).Write(uint.MinValue);
            cw.Write(ulong.MaxValue).Write("ulong".ToCharArray()).Write(ulong.MinValue);
            cw.Write(short.MaxValue).Write("short".ToCharArray()).Write(short.MinValue);
            cw.Write(ushort.MaxValue).Write("ushort".ToCharArray()).Write(ushort.MinValue);
            cw.Write(byte.MaxValue).Write("byte".ToCharArray()).Write(byte.MinValue);
            cw.Write(sbyte.MaxValue).Write("sbyte".ToCharArray()).Write(sbyte.MinValue);
            cw.Write(float.MaxValue).Write("float".ToCharArray()).Write(float.MinValue);
            cw.Write(double.MaxValue).Write("double".ToCharArray()).Write(double.MinValue);
            cw.Write(decimal.MaxValue).Write("decimal".ToCharArray()).Write(decimal.MinValue);
            cw.Write('A');
            cw.Write('あ');
            cw.Write("λόγος");
            cw.Write("ιδέα".AsSpan());
            cw.Write("φύσις"u8);
            cw.Write(new Asciis("ReadOnlnSpan<byte>"u8.ToArray()));
            cw.Write(new Utf8ConsoleWriterFormatter());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("2147483647int-2147483648" +
                "9223372036854775807long-9223372036854775808" +
                "4294967295uint0" +
                "18446744073709551615ulong0" +
                "32767short-32768" +
                "65535ushort0" +
                "255byte0" +
                "127sbyte-128" +
                "340282346638528859811704183484516925440.00000000000000000000float-340282346638528859811704183484516925440.00000000000000000000" +
             "179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368.00000000000000000000double-179769313486231570814527423731704356798070567525844996598917476803157260780028538760589558632766878171540458953514382464234321326889464182768467546703537516986049910576551282076245490090389328944075868508455133942304583236903222948165808559332123348274797826204144723168738177180919299881250404026184124858368.00000000000000000000" +
                "79228162514264337593543950335.00000000000000000000decimal-79228162514264337593543950335.00000000000000000000" +
                "AあλόγοςιδέαφύσιςReadOnlnSpan<byte>Utf8ConsoleWriterFormatter"));
        }
        readonly struct Utf8ConsoleWriterFormatter : IUtf8ConsoleWriterFormatter
        {
            public void Write(Utf8ConsoleWriter cw) => cw.Write("Utf8ConsoleWriterFormatter");
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
        public void WriteLineSpan()
        {
            cw.WriteLine("foobarテスト".AsSpan());
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("foobarテスト" + newLine));

            cw.Write("λόγος".AsSpan());
            cw.Write("ゆく河の流れは絶えずしてしかももとの水にあらず".AsSpan());
            cw.Flush();
            buffer.ShouldBe(ToBytes("foobarテスト" + newLine + "λόγοςゆく河の流れは絶えずしてしかももとの水にあらず"));
        }

        [Fact]
        public void WriteLineU8()
        {
            cw.WriteLine("foobarテスト"u8);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes("foobarテスト" + newLine));

            cw.Write("λόγος"u8);
            cw.Write("ゆく河の流れは絶えずしてしかももとの水にあらず"u8);
            cw.Flush();
            buffer.ShouldBe(ToBytes("foobarテスト" + newLine + "λόγοςゆく河の流れは絶えずしてしかももとの水にあらず"));
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

        [Fact]
        public void WriteLineCharArray()
        {
            var foo = new[] { 'f', 'o', 'o', 'b', 'a', 'r', 'b', 'a', 'z' };
            cw.WriteLine(foo);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"foobarbaz{newLine}"));
        }

        [Fact]
        public void WriteGridJaggedArray()
        {
            cw.WriteGrid(
            [
                [ 1, 2, 3, ],
                [ -1, -2, -3, ],
                [ 4, 5, 6, ],
            ]);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
        }

        [Fact]
        public void WriteGridTuple()
        {
            cw.WriteGrid(
            [
                (1, 2, 3),
                (-1, -2, -3),
                (4, 5, 6),
            ]);
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
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
            buffer.ShouldBe(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
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
            buffer.ShouldBe(Enumerable.Repeat((byte)0, BufSize));
            cw.Flush();
            buffer.ShouldBe(ToBytes(@"1 2
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

            public readonly void Write(Utf8ConsoleWriter cw)
            {
                cw.Write(x);
                cw.Write(' ');
                cw.Write(y);
            }
            public override readonly string ToString() => $"{x} {y}";
        }

        [Fact]
        public void EnsureLong()
        {
            var len = cw.buf.Length - long.MinValue.ToString().Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(long.MinValue);
            cw.len.ShouldBe(20);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('1', len) + long.MinValue.ToString()));
        }
        [Fact]
        public void EnsureDouble()
        {
            var len = cw.buf.Length - double.MinValue.ToString("F20").Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(double.MinValue);
            cw.len.ShouldBe(331);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('1', len) + double.MinValue.ToString("F20")));
        }
        [Fact]
        public void EnsureDecimal()
        {
            var len = cw.buf.Length - decimal.MinValue.ToString("F20").Length + 1;
            for (int i = 0; i < len; i++)
                cw.Write('1');
            cw.Write(decimal.MinValue);
            cw.len.ShouldBe(51);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('1', len) + decimal.MinValue.ToString("F20")));
        }

        [Fact]
        public void BufferWriterRaw()
        {
            IBufferWriter<byte> bw = cw;
            var span = bw.GetSpan(1 << 6);
            span.Length.ShouldBeGreaterThanOrEqualTo(1 << 6);
            Enumerable.Repeat((byte)'#', 1 << 4).ToArray().AsSpan().CopyTo(span);
            bw.Advance(1 << 4);
            cw.len.ShouldBe(1 << 4);
            span = bw.GetSpan((1 << 12) + 3);
            cw.buf.Length.ShouldBe((1 << 12) + 3);
            cw.len.ShouldBe(0);
            span[0] = (byte)'!';
            bw.Advance(1);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('#', 1 << 4) + "!"));
            cw.len.ShouldBe(0);
        }

        [Fact]
        public void BufferWriter1()
        {
            IBufferWriter<byte> bw = cw;
            bw.Write(Enumerable.Repeat((byte)'#', 1 << 10).ToArray());
            cw.len.ShouldBe(1 << 10);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('#', 1 << 10)));
            cw.len.ShouldBe(0);
        }

        [Fact]
        public void BufferWriter2()
        {
            IBufferWriter<byte> bw = cw;
            bw.Write(Enumerable.Repeat((byte)'#', 1 << 12)
                .Concat(Enumerable.Repeat((byte)'$', 1 << 6)).ToArray());
            cw.len.ShouldBe(1 << 6);
            cw.Flush();
            buffer.ShouldBe(ToBytes(new string('#', 1 << 12) + new string('$', 1 << 6)));
        }
    }
}
#endif
