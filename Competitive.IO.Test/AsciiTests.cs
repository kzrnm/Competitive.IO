using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO;

public class AsciiTests
{
    [Test]
    public async Task AsSpan()
    {
#if !NETFRAMEWORK
        var a = new Asciis("abcdefg"u8.ToArray());
        await Assert.That(a.AsSpan().ToArray()).IsStrictlyEquivalentTo("abcdefg"u8.ToArray());
        await Assert.That(((Span<Ascii>)a).ToArray()).IsStrictlyEquivalentTo(new Ascii[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
#endif
    }

    [Test]
    public async Task Enumerable()
    {
        await Assert.That(new Asciis("abcdefg"u8.ToArray()).Select(b => b - 97).ToArray())
            .IsStrictlyEquivalentTo([0, 1, 2, 3, 4, 5, 6,]);

        var list = new List<int>();
        foreach (var item in new Asciis("abcdefg"u8.ToArray()))
        {
            list.Add(item - 97);
        }
        await Assert.That(list).IsStrictlyEquivalentTo([0, 1, 2, 3, 4, 5, 6,]);
    }

    [Test]
    public async Task Sort()
    {
        Asciis[] arr = [
            new("abc"u8.ToArray()),
            new("ABCde"u8.ToArray()),
            new("AbcDe"u8.ToArray()),
            new("abcde"u8.ToArray()),
            new("ab"u8.ToArray()),
            new("bac"u8.ToArray()),
            new("qqqqqq"u8.ToArray()),
        ];

        Array.Sort(arr);
        await Assert.That(arr).IsStrictlyEquivalentTo([
            "ABCde",
            "AbcDe",
            "ab",
            "abc",
            "abcde",
            "bac",
            "qqqqqq",
        ]);
    }

    [Test]
    public async Task Cast()
    {
        var s = new Asciis("abcdefg"u8.ToArray());
        await Assert.That(s[0] == 'a').IsTrue();
        await Assert.That(s[1] == 'b').IsTrue();
        await Assert.That(s[2] == 'c').IsTrue();
        await Assert.That(s[3] == 'd').IsTrue();
        await Assert.That(s[4] == 'e').IsTrue();
        await Assert.That(s[5] == 'f').IsTrue();
        await Assert.That(s[6] == 'g').IsTrue();

        await Assert.That(s[0] == 0x61).IsTrue();
        await Assert.That(s[1] == 0x62).IsTrue();
        await Assert.That(s[2] == 0x63).IsTrue();
        await Assert.That(s[3] == 0x64).IsTrue();
        await Assert.That(s[4] == 0x65).IsTrue();
        await Assert.That(s[5] == 0x66).IsTrue();
        await Assert.That(s[6] == 0x67).IsTrue();
    }
}
