using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO.Reader;

[InheritsTests]
public class PropertyRepeatReaderTests : RepeatReaderTests
{
    protected override ConsoleReader GetConsoleReader(string v)
        => GetPropertyConsoleReader(v);
    protected override ConsoleReader GetConsoleReader(string v, int bufferSize)
        => GetPropertyConsoleReader(v, bufferSize);
    protected static PropertyConsoleReader GetPropertyConsoleReader(string v)
        => Helpers.GetPropertyConsoleReader(v);
    protected static PropertyConsoleReader GetPropertyConsoleReader(string v, int bufferSize)
        => Helpers.GetPropertyConsoleReader(v, bufferSize);

    [Test]
    [Timeout(5000)]
    public async Task SelectPropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        await Assert.That(cr.Repeat(3).Select<(int, int)>(c => (c.Int, c)))
        .IsStrictlyEquivalentTo([(123, -14421), (-2147483647, 2147483647), (1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectWithIndexPropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        await Assert.That(cr.Repeat(3).Select<(int, int, int)>((c, i) => (i, c.Int, c)))
        .IsStrictlyEquivalentTo([(0, 123, -14421), (1, -2147483647, 2147483647), (2, 1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task GridProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        var grid = cr.Grid(3, 2, c => c.Int);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([123, -14421]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([-2147483647, 2147483647]);
        await Assert.That(grid[2]).IsStrictlyEquivalentTo([1, 2]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task GridWithIndexPropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
        var grid = cr.Grid(3, 2, (c, i, j) => (i, j, c.Int));
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([(0, 0, 123), (0, 1, -14421)]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([(1, 0, -2147483647), (1, 1, 2147483647)]);
        await Assert.That(grid[2]).IsStrictlyEquivalentTo([(2, 0, 1), (2, 1, 2)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray2PropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
-1 1
-2 2
-3 3
");
        var (a, b) = cr.Repeat(3).SelectArray(c => (c.Int, c.Int));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray3PropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
-1 1 a
-2 2 b
-3 3 c
");
        var (a, b, c) = cr.Repeat(3).SelectArray(cc => (cc.Int, cc.Int, cr.Char));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
        await Assert.That(c).IsStrictlyEquivalentTo(['a', 'b', 'c']);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectArray4PropProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
-1 1 a 0.5
-2 2 b 1.5
-3 3 c 1e8
");
        var (a, b, c, d) = cr.Repeat(3).SelectArray(cc => (cc.Int, cc.Int, cr.Char, cr.Double));
        await Assert.That(a).IsStrictlyEquivalentTo([-1, -2, -3]);
        await Assert.That(b).IsStrictlyEquivalentTo([1, 2, 3]);
        await Assert.That(c).IsStrictlyEquivalentTo(['a', 'b', 'c']);
        await Assert.That(d).IsStrictlyEquivalentTo([0.5, 1.5, 1e8]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task IntProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
        await Assert.That(cr.Repeat(4).Int).IsStrictlyEquivalentTo([123, -14421, -2147483647, 2147483647]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Int0Prop(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
        await Assert.That(cr.Repeat(4).Int0).IsStrictlyEquivalentTo([122, -14422, -2147483648, 2147483646]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task LongProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
        await Assert.That(cr.Repeat(4).Long).IsStrictlyEquivalentTo([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long0Prop(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
        await Assert.That(cr.Repeat(4).Long0).IsStrictlyEquivalentTo([122L, -14422L, 9223372036854775807L, 9223372036854775806L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULongProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
        await Assert.That(cr.Repeat(4).ULong).IsStrictlyEquivalentTo((ulong[])[123, 14421, 9223372036854775808, 18446744073709551615]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong0Prop(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
        await Assert.That(cr.Repeat(4).ULong0).IsStrictlyEquivalentTo((ulong[])[122, 14420, 9223372036854775807, 18446744073709551614]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DoubleProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        await Assert.That(cr.Repeat(8).Double).IsStrictlyEquivalentTo([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, .000123456, .000123456]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DecimalProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        await Assert.That(cr.Repeat(8).Decimal).IsStrictlyEquivalentTo([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task AsciiProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
        await Assert.That(cr.Repeat(5).Ascii).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task StringProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
        await Assert.That(cr.Repeat(8).String).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task StringCharsProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
        await Assert.That(cr.Repeat(8).StringChars.Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task LineCharsProp(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
        await Assert.That(cr.Repeat(4).LineChars.Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
    }, cancellationToken);
}
