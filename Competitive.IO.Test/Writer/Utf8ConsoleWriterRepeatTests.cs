#if NETCOREAPP3_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO.Writer;




public sealed class Utf8ConsoleWriterRepeatTests : IDisposable
{
    private const int BufSize = 1 << 10;
    private readonly byte[] buffer = new byte[BufSize];
    private readonly MemoryStream stream;
#pragma warning disable TUnit0023 // Member should be disposed within a clean up method
    private readonly Utf8ConsoleWriter cw;
#pragma warning restore TUnit0023 // Member should be disposed within a clean up method

    public void Dispose()
    {
        stream.Dispose();
        ((IDisposable)cw).Dispose();
    }
    public Utf8ConsoleWriterRepeatTests()
    {
        stream = new MemoryStream(buffer);
        cw = new Utf8ConsoleWriter(stream, 8);
    }
    private static byte[] ToBytes(string str)
    {
        var rt = new byte[BufSize];
        Encoding.UTF8.GetBytes(str, rt);
        return rt;
    }

    public static IEnumerable<int> WriteRepeat_Data(int size)
    {
        for (int i = 1; i <= size; i++)
        {
            yield return i;
        }
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatAscii(int len)
    {
        cw.Write('a', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('a', len) + "$"));
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatAscii2(int len)
    {
        cw.Write("012");
        cw.Write('a', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("012" + new string('a', len) + "$"));
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatGreek(int len)
    {
        cw.Write('ψ', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('ψ', len) + "$"));
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatGreek2(int len)
    {
        cw.Write("012");
        cw.Write('ψ', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("012" + new string('ψ', len) + "$"));
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatHiragana(int len)
    {
        cw.Write('こ', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes(new string('こ', len) + "$"));
    }

    [Test]
    [MethodDataSource(nameof(WriteRepeat_Data), Arguments = [100])]
    public async Task WriteRepeatHiragana2(int len)
    {
        cw.Write("012");
        cw.Write('こ', len);
        cw.Write('$').Flush();
        await Assert.That(buffer).IsStrictlyEquivalentTo(ToBytes("012" + new string('こ', len) + "$"));
    }
}
#endif
