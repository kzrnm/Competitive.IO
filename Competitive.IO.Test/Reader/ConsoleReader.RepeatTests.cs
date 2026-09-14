using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kzrnm.Competitive.IO.Reader;
using static Kzrnm.Competitive.IO.Reader.Helpers;

namespace Kzrnm.Competitive.IO.Reader;

public class ConsoleReaderRepeatGenericTests
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
        await Assert.That(cr.Repeat<int>(4)).IsStrictlyEquivalentTo([123, -14421, -2147483647, 2147483647]);
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
        await Assert.That(cr.Repeat<uint>(4)).IsStrictlyEquivalentTo((uint[])[123, 14421, 0, 4294967295]);
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
        await Assert.That(cr.Repeat<long>(4)).IsStrictlyEquivalentTo([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
        await Assert.That(cr.Repeat<ulong>(4)).IsStrictlyEquivalentTo((ulong[])[123, 14421, 9223372036854775808, 18446744073709551615]);
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
        await Assert.That(cr.Repeat<double>(8)).IsStrictlyEquivalentTo([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
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
        await Assert.That(cr.Repeat<decimal>(8)).IsStrictlyEquivalentTo([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
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
        await Assert.That(cr.Repeat<string>(5)).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
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
        await Assert.That(cr.Repeat<char[]>(5).Select(c => new string(c))).IsStrictlyEquivalentTo(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
    }, cancellationToken);

#if NETCOREAPP3_0_OR_GREATER
    [Test]
    [Timeout(5000)]
    public async Task RepeatToSpan(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4
");
        var buf = new int[5];
        cr.Repeat(buf.AsSpan(1));
        await Assert.That(buf).IsStrictlyEquivalentTo([0, 1, 2, 3, 4]);
    }, cancellationToken);
#endif
}
