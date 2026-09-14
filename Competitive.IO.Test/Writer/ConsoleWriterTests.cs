using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO.Writer;

public sealed class ConsoleWriterTests : IDisposable
{
    private const int BufSize = 1 << 8;
    private readonly byte[] buffer = new byte[BufSize];
    private readonly string newLine;
    private readonly MemoryStream stream;
    private readonly ConsoleWriter cw;
    public void Dispose()
    {
        stream.Dispose();
        cw.Dispose();
    }

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
        cw.Write('A');
        cw.Write(-123456);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("A-123456"));
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
    public async Task WriteGridJaggedArray()
    {
        cw.WriteGrid([
            [1, 2, 3, ],
            [-1, -2, -3, ],
            [4, 5, 6, ],
        ]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
    }

#if !NETFRAMEWORK
    [Test]
    public async Task WriteGridTuple()
    {
        cw.WriteGrid([
            (1, 2, 3),
            (-1, -2, -3),
            (4, 5, 6),
        ]);
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
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
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes($"1 2 3{newLine}-1 -2 -3{newLine}4 5 6{newLine}"));
    }

    [Test]
    public async Task WriteLineSpan()
    {
        cw.WriteLine("foobar".AsSpan());
        await Assert.That(buffer).IsStrictlyEquivalentTo(Enumerable.Repeat((byte)0, BufSize));
        cw.Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("foobar" + newLine));
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
#endif
}
