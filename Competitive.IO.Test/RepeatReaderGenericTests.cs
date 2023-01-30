using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static Kzrnm.Competitive.IO.Helpers.ReaderHelpers;

namespace Kzrnm.Competitive.IO
{
    public class RepeatReaderGenericTests
    {
        [Fact(Timeout = 3000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Read<int>().Should().Equal(123, -14421, -2147483647, 2147483647);
        });

        [Fact(Timeout = 3000)]
        public async Task UInt() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).Read<uint>().Should().Equal(123, 14421, 0, 4294967295);
        });

        [Fact(Timeout = 3000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Read<long>().Should().Equal(123L, -14421L, -9223372036854775808L, 9223372036854775807L);
        });

        [Fact(Timeout = 3000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).Read<ulong>().Should().Equal(123, 14421, 9223372036854775808, 18446744073709551615);
        });

        [Fact(Timeout = 3000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Read<double>().Should().Equal(123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456);
        });

        [Fact(Timeout = 3000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Read<decimal>().Should().Equal(123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m);
        });

        [Fact(Timeout = 3000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Read<string>().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });


        [Fact(Timeout = 3000)]
        public async Task AsciiChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Read<char[]>().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });
    }
}
