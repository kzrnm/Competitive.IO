using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kzrnm.Competitive.IO;
using Xunit;
namespace Kzrnm.Competitive.IO;

public class AsciiTests
{
    [Fact]
    public void Enumerable()
    {
        new Asciis("abcdefg"u8.ToArray()).Select(b => b - 97).ToArray()
            .ShouldBe([0, 1, 2, 3, 4, 5, 6,]);

        var list = new List<int>();
        foreach (var item in new Asciis("abcdefg"u8.ToArray()))
        {
            list.Add(item - 97);
        }
        list.ShouldBe([0, 1, 2, 3, 4, 5, 6,]);
    }

    [Fact]
    public void Cast()
    {
        var s = new Asciis("abcdefg"u8.ToArray());
        (s[0] == 'a').ShouldBeTrue();
        (s[1] == 'b').ShouldBeTrue();
        (s[2] == 'c').ShouldBeTrue();
        (s[3] == 'd').ShouldBeTrue();
        (s[4] == 'e').ShouldBeTrue();
        (s[5] == 'f').ShouldBeTrue();
        (s[6] == 'g').ShouldBeTrue();

        (s[0] == 0x61).ShouldBeTrue();
        (s[1] == 0x62).ShouldBeTrue();
        (s[2] == 0x63).ShouldBeTrue();
        (s[3] == 0x64).ShouldBeTrue();
        (s[4] == 0x65).ShouldBeTrue();
        (s[5] == 0x66).ShouldBeTrue();
        (s[6] == 0x67).ShouldBeTrue();
    }
}
