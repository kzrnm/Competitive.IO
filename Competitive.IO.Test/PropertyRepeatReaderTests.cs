using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static Kzrnm.Competitive.IO.Helpers.ReaderHelpers;

namespace Kzrnm.Competitive.IO
{
    public class PropertyRepeatReaderTests
    {
        [Fact(Timeout = 1000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int.Should().Equal(123, -14421, -2147483647, 2147483647);
        });

        [Fact(Timeout = 1000)]
        public async Task IntImplicit() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            int[] r = cr.Repeat(4);
            r.Should().Equal(123, -14421, -2147483647, 2147483647);
        });

        [Fact(Timeout = 1000)]
        public async Task Int0() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int0.Should().Equal(122, -14422, -2147483648, 2147483646);
        });

        [Fact(Timeout = 1000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long.Should().Equal(123L, -14421L, -9223372036854775808L, 9223372036854775807L);
        });

        [Fact(Timeout = 1000)]
        public async Task LongImplicit() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            long[] r = cr.Repeat(4);
            r.Should().Equal(123L, -14421L, -9223372036854775808L, 9223372036854775807L);
        });

        [Fact(Timeout = 1000)]
        public async Task Long0() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long0.Should().Equal(122L, -14422L, 9223372036854775807L, 9223372036854775806L);
        });

        [Fact(Timeout = 1000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong.Should().Equal(123, 14421, 9223372036854775808, 18446744073709551615);
        });

        [Fact(Timeout = 1000)]
        public async Task ULongImplicit() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            ulong[] r = cr.Repeat(4);
            r.Should().Equal(123, 14421, 9223372036854775808, 18446744073709551615);
        });

        [Fact(Timeout = 1000)]
        public async Task ULong0() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong0.Should().Equal(122, 14420, 9223372036854775807, 18446744073709551614);
        });

        [Fact(Timeout = 1000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Double.Should().Equal(123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456);
        });

        [Fact(Timeout = 1000)]
        public async Task DoubleImplicit() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            double[] r = cr.Repeat(8);
            r.Should().Equal(123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456);
        });

        [Fact(Timeout = 1000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Ascii.Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 1000)]
        public async Task AsciiImplicit() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
            string[] r = cr.Repeat(5);
            r.Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 1000)]
        public async Task String() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).String.Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터");
        });

    }
}
