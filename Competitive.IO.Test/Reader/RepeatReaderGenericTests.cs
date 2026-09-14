using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Kzrnm.Competitive.IO.Reader.Helpers;

namespace Kzrnm.Competitive.IO.Reader;

public class RepeatReaderGenericTests
{
    [Test]
    [Timeout(5000)]
    public async Task Int(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
        await Assert.That(cr.Repeat(4).Read<int>()).IsStrictlyEquivalentTo([123, -14421, -2147483647, 2147483647]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UInt(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
        await Assert.That(cr.Repeat(4).Read<uint>()).IsStrictlyEquivalentTo((uint[])[123, 14421, 0, 4294967295]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
        await Assert.That(cr.Repeat(4).Read<long>()).IsStrictlyEquivalentTo([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
        await Assert.That(cr.Repeat(4).Read<ulong>()).IsStrictlyEquivalentTo((ulong[])[123, 14421, 9223372036854775808, 18446744073709551615]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Double(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        await Assert.That(cr.Repeat(8).Read<double>()).IsStrictlyEquivalentTo([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Decimal(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
        await Assert.That(cr.Repeat(8).Read<decimal>()).IsStrictlyEquivalentTo([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Ascii(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
        await Assert.That(cr.Repeat(5).Read<string>()).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task AsciiChars(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
        await Assert.That(cr.Repeat(5).Read<char[]>().Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);


#if NETCOREAPP3_0_OR_GREATER
    [Test]
    [Timeout(5000)]
    public async Task Select(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4
");
        var buf = new int[5];
        cr.Repeat(4).Select(buf.AsSpan(1), cr => cr.Int());
        await Assert.That(buf).IsStrictlyEquivalentTo([0, 1, 2, 3, 4]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task SelectIndex(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4
");
        var buf = new int[5];
        cr.Repeat(4).Select(buf.AsSpan(1), (cr, i) => cr.Int() * i);
        await Assert.That(buf).IsStrictlyEquivalentTo([0, 0, 2, 6, 12]);
    }, cancellationToken);
#endif
}
