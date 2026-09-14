using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kzrnm.Competitive.IO.Reader;

namespace Kzrnm.Competitive.IO.Reader;

public class RepeatReaderTests
{
    protected virtual ConsoleReader GetConsoleReader(string v)
        => Helpers.GetConsoleReader(v);
    protected virtual ConsoleReader GetConsoleReader(string v, int bufferSize)
        => Helpers.GetConsoleReader(v, bufferSize);

    [Test]
    [Timeout(5000)]
    public async Task Select(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        await Assert.That(cr.Repeat(3).Select<(int, int)>(c => (c.Int(), c)))
        .IsStrictlyEquivalentTo([(123, -14421), (-2147483647, 2147483647), (1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectWithIndex(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        await Assert.That(cr.Repeat(3).Select<(int, int, int)>((c, i) => (i, c.Int(), c)))
        .IsStrictlyEquivalentTo([(0, 123, -14421), (1, -2147483647, 2147483647), (2, 1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Grid(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        var grid = cr.Grid(3, 2, c => c.Int());
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([123, -14421]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([-2147483647, 2147483647]);
        await Assert.That(grid[2]).IsStrictlyEquivalentTo([1, 2]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task GridWithIndex(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        var grid = cr.Grid(3, 2, (c, i, j) => (i, j, c.Int()));
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([(0, 0, 123), (0, 1, -14421)]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([(1, 0, -2147483647), (1, 1, 2147483647)]);
        await Assert.That(grid[2]).IsStrictlyEquivalentTo([(2, 0, 1), (2, 1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray2(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
-1 1
-2 2
-3 3
");
        var (a, b) = cr.Repeat(3).SelectArray(c => (c.Int(), c.Int()));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray3(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
-1 1 a
-2 2 b
-3 3 c
");
        var (a, b, c) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char()));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
        await Assert.That(c).IsStrictlyEquivalentTo(['a', 'b', 'c']);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray4(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
-1 1 a 0.5
-2 2 b 1.5
-3 3 c 1e8
");
        var (a, b, c, d) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char(), cr.Double()));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
        await Assert.That(c).IsStrictlyEquivalentTo(['a', 'b', 'c']);
        await Assert.That(d).IsStrictlyEquivalentTo([0.5, 1.5, 1e8]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Int(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
await ");
        await Assert.That(cr.Repeat(4).Int()).IsStrictlyEquivalentTo([123, -14421, -2147483647, 2147483647]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task IntImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
        int[] r = cr.Repeat(4);
        await Assert.That(r).IsStrictlyEquivalentTo([123, -14421, -2147483647, 2147483647]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Int0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
await ");
        await Assert.That(cr.Repeat(4).Int0()).IsStrictlyEquivalentTo([122, -14422, -2147483648, 2147483646]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UInt(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
await ");
        await Assert.That(cr.Repeat(4).UInt()).IsStrictlyEquivalentTo((uint[])[123, 14421, 0, 4294967295]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UIntImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
        uint[] r = cr.Repeat(4);
        await Assert.That(r).IsStrictlyEquivalentTo((uint[])[123, 14421, 0, 4294967295]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UInt0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
await ");
        await Assert.That(cr.Repeat(4).UInt0()).IsStrictlyEquivalentTo((uint[])[122, 14420, 4294967295, 4294967294]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
await ");
        await Assert.That(cr.Repeat(4).Long()).IsStrictlyEquivalentTo([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task LongImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
        long[] r = cr.Repeat(4);
        await Assert.That(r).IsStrictlyEquivalentTo([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
await ");
        await Assert.That(cr.Repeat(4).Long0()).IsStrictlyEquivalentTo([122L, -14422L, 9223372036854775807L, 9223372036854775806L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
await ");
        await Assert.That(cr.Repeat(4).ULong()).IsStrictlyEquivalentTo((ulong[])[123, 14421, 9223372036854775808, 18446744073709551615]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULongImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
        ulong[] r = cr.Repeat(4);
        await Assert.That(r).IsStrictlyEquivalentTo((ulong[])[123, 14421, 9223372036854775808, 18446744073709551615]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
await ");
        await Assert.That(cr.Repeat(4).ULong0()).IsStrictlyEquivalentTo((ulong[])[122, 14420, 9223372036854775807, 18446744073709551614]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Double(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
await ");
        await Assert.That(cr.Repeat(8).Double()).IsStrictlyEquivalentTo([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DoubleImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        double[] r = cr.Repeat(8);
        await Assert.That(r).IsStrictlyEquivalentTo([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Decimal(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
await ");
        await Assert.That(cr.Repeat(8).Decimal()).IsStrictlyEquivalentTo([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DecimalImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        decimal[] r = cr.Repeat(8);
        await Assert.That(r).IsStrictlyEquivalentTo([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Ascii(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
await ");
        await Assert.That(cr.Repeat(5).Ascii()).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task AsciiImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
        Asciis[] r = cr.Repeat(5);
        await Assert.That(r).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task String(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
await ");
        await Assert.That(cr.Repeat(8).String()).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task Line(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
await ");
        await Assert.That(cr.Repeat(4).Line()).IsStrictlyEquivalentTo(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task AsciiCharsImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
        char[][] r = cr.Repeat(5);
        await Assert.That(r.Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task StringChars(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
await ");
        await Assert.That(cr.Repeat(8).StringChars().Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task LineChars(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
await ");
        await Assert.That(cr.Repeat(4).LineChars().Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
    }, cancellationToken);

#if CI
    [Test]
    [Timeout(60000)]
#else
    [Test]
    [Timeout(10000)]
#endif
    public async Task RandomLong(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var rnd = new Random(GetType().GetHashCode());
        for (int q = 0; q < 800; q++)
        {
            var list = new List<long>();
            var sb = new StringBuilder();
            for (int s = rnd.Next(100, 500); s >= 0; s--)
            {
                sb.Append(rnd.Next(100) switch
                {
                    < 10 => "\n",
                    < 30 => "  ",
                    _ => " ",
                });
                var value = unchecked(((long)rnd.Next() << 33) | (long)rnd.Next());
                sb.Append(value);
                list.Add(value);
            }
            var cr = GetConsoleReader(sb.ToString(), 50);
            await Assert.That(cr.Repeat(list.Count).Long()).IsStrictlyEquivalentTo(list);
        }
    }, cancellationToken);

#if CI
    [Test]
    [Timeout(60000)]
#else
    [Test]
    [Timeout(10000)]
#endif
    public async Task RandomString(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var rnd = new Random(GetType().GetHashCode());
        for (int q = 0; q < 800; q++)
        {
            var list = new List<string>();
            var sb = new StringBuilder();
            for (int s = rnd.Next(100, 500); s >= 0; s--)
            {
                sb.Append(rnd.Next(100) switch
                {
                    < 10 => "\n",
                    < 30 => "  ",
                    _ => " ",
                });
                var value = new string([.. Enumerable.Repeat(rnd, rnd.Next(10, 60)).Select(rnd => (char)rnd.Next('a', 'z'))]);
                sb.Append(value);
                list.Add(value);
            }
            var cr = GetConsoleReader(sb.ToString(), 50);
            await Assert.That(cr.Repeat(list.Count).String()).IsStrictlyEquivalentTo(list);
        }
    }, cancellationToken);

#if CI
    [Test]
    [Timeout(60000)]
#else
    [Test]
    [Timeout(10000)]
#endif
    public async Task RandomLargeString(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var rnd = new Random(GetType().GetHashCode());
        for (int q = 0; q < 80; q++)
        {
            var list = new List<string>();
            var sb = new StringBuilder();
            for (int s = rnd.Next(100, 500); s >= 0; s--)
            {
                sb.Append(rnd.Next(100) switch
                {
                    < 10 => "\n",
                    < 30 => "  ",
                    _ => " ",
                });
                var value = new string([.. Enumerable.Repeat(rnd, rnd.Next(120, 1200)).Select(rnd => (char)rnd.Next('a', 'z'))]);
                sb.Append(value);
                list.Add(value);
            }
            var cr = GetConsoleReader(sb.ToString(), 50);
            await Assert.That(cr.Repeat(list.Count).String()).IsStrictlyEquivalentTo(list);
        }
    }, cancellationToken);
}
