#if NETCOREAPP3_0_OR_GREATER
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kzrnm.Competitive.IO.Writer;

public sealed class Utf8ConsoleWriterTests : IDisposable
{
    private const int BufSize = 1 << 13;
    private readonly byte[] buffer = new byte[BufSize];
    private readonly string newLine;
    private readonly MemoryStream stream;
#pragma warning disable TUnit0023 // Member should be disposed within a clean up method
    private readonly Utf8ConsoleWriter cw;
#pragma warning restore TUnit0023 // Member should be disposed within a clean up method
    public void Dispose()
    {
        stream.Dispose();
        ((IDisposable)cw).Dispose();
    }

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

    [Test]
    public async Task WriteLineEmpty()
    {
        cw.WriteLine();
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(newLine));
    }

    [Test]
    public async Task Write()
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
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("2147483647int-2147483648" +
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

    [Test]
    public async Task WriteLine()
    {
        cw.WriteLine(-123456);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("-123456" + newLine));
    }

    [Test]
    public async Task WriteLine2()
    {
        cw.WriteLine(Enumerable.Repeat((byte)'A', 4154).ToArray().AsSpan());
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('A', 4154)));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('A', 4154) + newLine));
    }

    [Test]
    public async Task WriteLineJoinEmpty()
    {
        cw.WriteLineJoin();
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(newLine));
    }

    [Test]
    public async Task WriteLineJoin2()
    {
        cw.WriteLineJoin("foo", 1);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"foo 1{newLine}"));
    }

    [Test]
    public async Task WriteLineJoin3()
    {
        cw.WriteLineJoin("foo", 1, -2L);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"foo 1 -2{newLine}"));
    }

    [Test]
    public async Task WriteLineJoin4()
    {
        cw.WriteLineJoin("foo", 1, -2L, 'x');
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"foo 1 -2 x{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinMany()
    {
        cw.WriteLineJoin("foo", 1, -2L, 'x', "bar");
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"foo 1 -2 x bar{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinManySameType()
    {
        cw.WriteLineJoin(1, 2, 3, 4, 5);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinIEnumerable()
    {
        cw.WriteLineJoin(Enumerable.Range(1, 5));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinList()
    {
        cw.WriteLineJoin(Enumerable.Range(1, 5).ToList());
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinArray()
    {
        cw.WriteLineJoin(Enumerable.Range(1, 5).Select(i => $"{i}").ToArray());
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLinesIEnumerable()
    {
        cw.WriteLines(Enumerable.Range(1, 5));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1\n2\n3\n4\n5{newLine}"));
    }

    [Test]
    public async Task WriteLinesList()
    {
        cw.WriteLines(Enumerable.Range(1, 5).ToList());
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1\n2\n3\n4\n5{newLine}"));
    }

    [Test]
    public async Task WriteLineSpan()
    {
        cw.WriteLine("foobarテスト".AsSpan());
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("foobarテスト" + newLine));

        cw.Write("λόγος".AsSpan());
        cw.Write("ゆく河の流れは絶えずしてしかももとの水にあらず".AsSpan());
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("foobarテスト" + newLine + "λόγοςゆく河の流れは絶えずしてしかももとの水にあらず"));
    }

    [Test]
    public async Task WriteLineU8()
    {
        cw.WriteLine("foobarテスト"u8);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("foobarテスト" + newLine));

        cw.Write("λόγος"u8);
        cw.Write("ゆく河の流れは絶えずしてしかももとの水にあらず"u8);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("foobarテスト" + newLine + "λόγοςゆく河の流れは絶えずしてしかももとの水にあらず"));
    }

    [Test]
    public async Task WriteLineJoinSpan()
    {
        cw.WriteLineJoin((Span<int>)[.. Enumerable.Range(1, 5)]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLinesSpan()
    {
        cw.WriteLines((Span<int>)[.. Enumerable.Range(1, 5)]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1\n2\n3\n4\n5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinReadOnlySpan()
    {
        cw.WriteLineJoin((ReadOnlySpan<int>)[.. Enumerable.Range(1, 5)]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLinesReadOnlySpan()
    {
        cw.WriteLines((ReadOnlySpan<int>)[.. Enumerable.Range(1, 5)]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1\n2\n3\n4\n5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinTuple2()
    {
        cw.WriteLineJoin((1, 2));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinTuple3()
    {
        cw.WriteLineJoin((1, 2, 'a'));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 a{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinTuple4()
    {
        cw.WriteLineJoin((1, 2, 'a', 4));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 a 4{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinTuple5()
    {
        cw.WriteLineJoin((1, 2, 'a', 4, 5));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 a 4 5{newLine}"));
    }

    [Test]
    public async Task WriteLineJoinTupleClass()
    {
        cw.WriteLineJoin(Tuple.Create(1, 2, 'a', 4));
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 a 4{newLine}"));
    }

    [Test]
    public async Task WriteLineCharArray()
    {
        var foo = new[] { 'f', 'o', 'o', 'b', 'a', 'r', 'b', 'a', 'z' };
        cw.WriteLine(foo);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"foobarbaz{newLine}"));
    }

    [Test]
    public async Task WriteGridJaggedArray()
    {
        cw.WriteGrid(
        [
            [ 1, 2, 3, ],
            [ -1, -2, -3, ],
            [ 4, 5, 6, ],
        ]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
    }

    [Test]
    public async Task WriteGridTuple()
    {
        cw.WriteGrid(
        [
            (1, 2, 3),
            (-1, -2, -3),
            (4, 5, 6),
        ]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
    }

    [Test]
    public async Task WriteGridArray()
    {
        cw.WriteGrid(new int[,]
        {
            { 1, 2, 3, },
            { -1, -2, -3, },
            { 4, 5, 6, },
        });
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3\n-1 -2 -3\n4 5 6{newLine}"));
    }

    [Test]
    public async Task WriteLinesFormatter()
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
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(@"1 2
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

    [Test]
    public async Task EnsureLong()
    {
        var len = cw.buf.Length - long.MinValue.ToString().Length + 1;
        for (int i = 0; i < len; i++)
            cw.Write('1');
        cw.Write(long.MinValue);
        await Assert.That(cw.len).IsEqualTo(20);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('1', len) + long.MinValue.ToString()));
    }
    [Test]
    public async Task EnsureDouble()
    {
        var len = cw.buf.Length - double.MinValue.ToString("F20").Length + 1;
        for (int i = 0; i < len; i++)
            cw.Write('1');
        cw.Write(double.MinValue);
        await Assert.That(cw.len).IsEqualTo(331);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('1', len) + double.MinValue.ToString("F20")));
    }
    [Test]
    public async Task EnsureDecimal()
    {
        var len = cw.buf.Length - decimal.MinValue.ToString("F20").Length + 1;
        for (int i = 0; i < len; i++)
            cw.Write('1');
        cw.Write(decimal.MinValue);
        await Assert.That(cw.len).IsEqualTo(51);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('1', len) + decimal.MinValue.ToString("F20")));
    }

    [Test]
    public async Task BufferWriterRaw()
    {
        IBufferWriter<byte> bw = cw;
        var span = bw.GetSpan(1 << 6);
        Enumerable.Repeat((byte)'#', 1 << 4).ToArray().AsSpan().CopyTo(span);
        await Assert.That(span.Length).IsBetween(1 << 6, int.MaxValue);
        bw.Advance(1 << 4);
        await Assert.That(cw.len).IsEqualTo(1 << 4);
        span = bw.GetSpan((1 << 12) + 3);
        span[0] = (byte)'!';
        await Assert.That(cw.buf).Count().IsEqualTo((1 << 12) + 3);
        await Assert.That(cw.len).IsEqualTo(0);
        bw.Advance(1);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('#', 1 << 4) + "!"));
        await Assert.That(cw.len).IsEqualTo(0);
    }

    [Test]
    public async Task BufferWriter1()
    {
        IBufferWriter<byte> bw = cw;
        bw.Write(Enumerable.Repeat((byte)'#', 1 << 10).ToArray());
        await Assert.That(cw.len).IsEqualTo(1 << 10);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('#', 1 << 10)));
        await Assert.That(cw.len).IsEqualTo(0);
    }

    [Test]
    public async Task BufferWriter2()
    {
        IBufferWriter<byte> bw = cw;
        bw.Write(Enumerable.Repeat((byte)'#', 1 << 12)
            .Concat(Enumerable.Repeat((byte)'$', 1 << 6)).ToArray());
        await Assert.That(cw.len).IsEqualTo(1 << 6);
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('#', 1 << 12) + new string('$', 1 << 6)));
    }
}
#endif
