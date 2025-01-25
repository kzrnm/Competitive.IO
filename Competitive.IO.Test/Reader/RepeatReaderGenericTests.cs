using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Kzrnm.Competitive.IO.Reader.Helpers;

namespace Kzrnm.Competitive.IO.Reader
{
    public class RepeatReaderGenericTests
    {
        [Fact(Timeout = 5000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Read<int>().ShouldBe([123, -14421, -2147483647, 2147483647]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UInt() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).Read<uint>().ShouldBe([123, 14421, 0, 4294967295]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Read<long>().ShouldBe([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).Read<ulong>().ShouldBe([123, 14421, 9223372036854775808, 18446744073709551615]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Read<double>().ShouldBe([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Read<decimal>().ShouldBe([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Read<string>().ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task AsciiChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Read<char[]>().Select(c => new string(c)).ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);


#if NETCOREAPP3_0_OR_GREATER
        [Fact(Timeout = 5000)]
        public async Task Select() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
1 2 3 4
");
            var buf = new int[5];
            cr.Repeat(5).Select(buf.AsSpan(1), cr => cr.Int());
            buf.ShouldBe([0, 1, 2, 3, 4]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
1 2 3 4
");
            var buf = new int[5];
            cr.Repeat(5).Select(buf.AsSpan(1), (cr, i) => cr.Int() * i);
            buf.ShouldBe([0, 0, 2, 6, 12]);
        }, TestContext.Current.CancellationToken);
#endif
    }
}
