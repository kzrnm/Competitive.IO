using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static Kzrnm.Competitive.IO.Helpers.ReaderHelpers;

namespace Kzrnm.Competitive.IO
{
    public class RepeatReaderTests
    {
        [Fact(Timeout = 3000)]
        public async Task Select() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int)>(c => (c.Int(), c))
            .Should().Equal((123, -14421), (-2147483647, 2147483647), (1, 2));
        });

        [Fact(Timeout = 3000)]
        public async Task SelectWithIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int, int)>((c, i) => (i, c.Int(), c))
            .Should().Equal((0, 123, -14421), (1, -2147483647, 2147483647), (2, 1, 2));
        });

        [Fact(Timeout = 3000)]
        public async Task Grid() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, c => c.Int());
            grid[0].Should().Equal(123, -14421);
            grid[1].Should().Equal(-2147483647, 2147483647);
            grid[2].Should().Equal(1, 2);
        });

        [Fact(Timeout = 3000)]
        public async Task GridWithIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, (c, i, j) => (i, j, c.Int()));
            grid[0].Should().Equal((0, 0, 123), (0, 1, -14421));
            grid[1].Should().Equal((1, 0, -2147483647), (1, 1, 2147483647));
            grid[2].Should().Equal((2, 0, 1), (2, 1, 2));
        });

        [Fact(Timeout = 3000)]
        public async Task SelectArray2() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1
-2 2
-3 3
");
            var (a, b) = cr.Repeat(3).SelectArray(c => (c.Int(), c.Int()));
            a.Should().Equal(-1, -2, -3);
            b.Should().Equal(1, 2, 3);
        });

        [Fact(Timeout = 3000)]
        public async Task SelectArray3() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1 a
-2 2 b
-3 3 c
");
            var (a, b, c) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char()));
            a.Should().Equal(-1, -2, -3);
            b.Should().Equal(1, 2, 3);
            c.Should().Equal('a', 'b', 'c');
        });

        [Fact(Timeout = 3000)]
        public async Task SelectArray4() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1 a 0.5
-2 2 b 1.5
-3 3 c 1e8
");
            var (a, b, c, d) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char(), cr.Double()));
            a.Should().Equal(-1, -2, -3);
            b.Should().Equal(1, 2, 3);
            c.Should().Equal('a', 'b', 'c');
            d.Should().Equal(0.5, 1.5, 1e8);
        });

        [Fact(Timeout = 3000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int().Should().Equal(123, -14421, -2147483647, 2147483647);
        });

        [Fact(Timeout = 3000)]
        public async Task IntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            int[] r = cr.Repeat(4);
            r.Should().Equal(123, -14421, -2147483647, 2147483647);
        });

        [Fact(Timeout = 3000)]
        public async Task Int0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int0().Should().Equal(122, -14422, -2147483648, 2147483646);
        });

        [Fact(Timeout = 3000)]
        public async Task UInt() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).UInt().Should().Equal(123, 14421, 0, 4294967295);
        });

        [Fact(Timeout = 3000)]
        public async Task UIntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            uint[] r = cr.Repeat(4);
            r.Should().Equal(123, 14421, 0, 4294967295);
        });

        [Fact(Timeout = 3000)]
        public async Task UInt0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).UInt0().Should().Equal(122, 14420, 4294967295, 4294967294);
        });

        [Fact(Timeout = 3000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long().Should().Equal(123L, -14421L, -9223372036854775808L, 9223372036854775807L);
        });

        [Fact(Timeout = 3000)]
        public async Task LongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            long[] r = cr.Repeat(4);
            r.Should().Equal(123L, -14421L, -9223372036854775808L, 9223372036854775807L);
        });

        [Fact(Timeout = 3000)]
        public async Task Long0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long0().Should().Equal(122L, -14422L, 9223372036854775807L, 9223372036854775806L);
        });

        [Fact(Timeout = 3000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong().Should().Equal(123, 14421, 9223372036854775808, 18446744073709551615);
        });

        [Fact(Timeout = 3000)]
        public async Task ULongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            ulong[] r = cr.Repeat(4);
            r.Should().Equal(123, 14421, 9223372036854775808, 18446744073709551615);
        });

        [Fact(Timeout = 3000)]
        public async Task ULong0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong0().Should().Equal(122, 14420, 9223372036854775807, 18446744073709551614);
        });

        [Fact(Timeout = 3000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Double().Should().Equal(123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456);
        });

        [Fact(Timeout = 3000)]
        public async Task DoubleImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            double[] r = cr.Repeat(8);
            r.Should().Equal(123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456);
        });

        [Fact(Timeout = 3000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Decimal().Should().Equal(123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m);
        });

        [Fact(Timeout = 3000)]
        public async Task DecimalImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            decimal[] r = cr.Repeat(8);
            r.Should().Equal(123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m);
        });

        [Fact(Timeout = 3000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Ascii().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 3000)]
        public async Task AsciiImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
            string[] r = cr.Repeat(5);
            r.Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 3000)]
        public async Task String() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).String().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터");
        });


        [Fact(Timeout = 3000)]
        public async Task Line() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(4).Line().Should().Equal("abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------");
        });

        [Fact(Timeout = 3000)]
        public async Task AsciiChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).AsciiChars().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 3000)]
        public async Task AsciiCharsImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
            char[][] r = cr.Repeat(5);
            r.Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz");
        });

        [Fact(Timeout = 3000)]
        public async Task StringChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).StringChars().Should().Equal("abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터");
        });


        [Fact(Timeout = 3000)]
        public async Task LineChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(4).LineChars().Should().Equal("abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------");
        });
    }
}
