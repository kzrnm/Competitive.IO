using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static Kzrnm.Competitive.IO.Helpers.ReaderHelpers;

namespace Kzrnm.Competitive.IO
{
    public class ConsoleReaderTests
    {
        [Fact(Timeout = 1000)]
        public async Task Line() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

1 2 3 4 5 6
| a | b | b |
");
            cr.Line().Should().Be("1 2 3 4 5 6");
            cr.Line().Should().Be("| a | b | b |");
        });

        [Fact(Timeout = 1000)]
        public async Task Char() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

a b c
def
");
            cr.Char().Should().Be('a');
            cr.Char().Should().Be('b');
            cr.Char().Should().Be('c');
            cr.Char().Should().Be('d');
            cr.Char().Should().Be('e');
            cr.Char().Should().Be('f');
        });

        [Fact(Timeout = 1000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            cr.Int().Should().Be(123);
            cr.Int().Should().Be(-14421);
            cr.Int().Should().Be(-2147483647);
            cr.Int().Should().Be(2147483647);
        });

        [Fact(Timeout = 1000)]
        public async Task IntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            int r;
            r = cr;
            r.Should().Be(123);
            r = cr;
            r.Should().Be(-14421);
            r = cr;
            r.Should().Be(-2147483647);
            r = cr;
            r.Should().Be(2147483647);
        });

        [Fact(Timeout = 1000)]
        public async Task Int0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            cr.Int0().Should().Be(122);
            cr.Int0().Should().Be(-14422);
            cr.Int0().Should().Be(-2147483648);
            cr.Int0().Should().Be(2147483646);
        });

        [Fact(Timeout = 1000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            cr.Long().Should().Be(123);
            cr.Long().Should().Be(-14421);
            cr.Long().Should().Be(-9223372036854775808);
            cr.Long().Should().Be(9223372036854775807);
        });

        [Fact(Timeout = 1000)]
        public async Task LongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            long r;
            r = cr;
            r.Should().Be(123);
            r = cr;
            r.Should().Be(-14421);
            r = cr;
            r.Should().Be(-9223372036854775808);
            r = cr;
            r.Should().Be(9223372036854775807);
        });

        [Fact(Timeout = 1000)]
        public async Task Long0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            cr.Long0().Should().Be(122);
            cr.Long0().Should().Be(-14422);
            cr.Long0().Should().Be(9223372036854775807);
            cr.Long0().Should().Be(9223372036854775806);
        });

        [Fact(Timeout = 1000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.ULong().Should().Be(123);
            cr.ULong().Should().Be(14421);
            cr.ULong().Should().Be(9223372036854775808);
            cr.ULong().Should().Be(18446744073709551615);
        });

        [Fact(Timeout = 1000)]
        public async Task ULongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            ulong r;
            r = cr;
            r.Should().Be(123);
            r = cr;
            r.Should().Be(14421);
            r = cr;
            r.Should().Be(9223372036854775808);
            r = cr;
            r.Should().Be(18446744073709551615);
        });

        [Fact(Timeout = 1000)]
        public async Task ULong0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.ULong().Should().Be(123);
            cr.ULong().Should().Be(14421);
            cr.ULong().Should().Be(9223372036854775808);
            cr.ULong().Should().Be(18446744073709551615);
        });


        [Fact(Timeout = 1000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            cr.Double().Should().Be(123.0);
            cr.Double().Should().Be(-14421.0);
            cr.Double().Should().Be(-123456789123456789123456789.0);
            cr.Double().Should().Be(123456789123456789123456789.0);
            cr.Double().Should().Be(-0.000123456);
            cr.Double().Should().Be(-.000123456);
            cr.Double().Should().Be(0.000123456);
            cr.Double().Should().Be(.000123456);
        });

        [Fact(Timeout = 1000)]
        public async Task DoubleImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            double r;
            r = cr;
            r.Should().Be(123.0);
            r = cr;
            r.Should().Be(-14421.0);
            r = cr;
            r.Should().Be(-123456789123456789123456789.0);
            r = cr;
            r.Should().Be(123456789123456789123456789.0);
            r = cr;
            r.Should().Be(-0.000123456);
            r = cr;
            r.Should().Be(-.000123456);
            r = cr;
            r.Should().Be(0.000123456);
            r = cr;
            r.Should().Be(.000123456);
        });

        [Fact(Timeout = 1000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            cr.Decimal().Should().Be(123.0m);
            cr.Decimal().Should().Be(-14421.0m);
            cr.Decimal().Should().Be(-123456789123456789123456789.0m);
            cr.Decimal().Should().Be(123456789123456789123456789.0m);
            cr.Decimal().Should().Be(-0.000123456m);
            cr.Decimal().Should().Be(-.000123456m);
            cr.Decimal().Should().Be(0.000123456m);
            cr.Decimal().Should().Be(.000123456m);
        });

        [Fact(Timeout = 1000)]
        public async Task DecimalImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            decimal r;
            r = cr;
            r.Should().Be(123.0m);
            r = cr;
            r.Should().Be(-14421.0m);
            r = cr;
            r.Should().Be(-123456789123456789123456789.0m);
            r = cr;
            r.Should().Be(123456789123456789123456789.0m);
            r = cr;
            r.Should().Be(-0.000123456m);
            r = cr;
            r.Should().Be(-.000123456m);
            r = cr;
            r.Should().Be(0.000123456m);
            r = cr;
            r.Should().Be(.000123456m);
        });

        [Fact(Timeout = 1000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
            cr.Ascii().Should().Be("abcdefg");
            cr.Ascii().Should().Be("hijklmnop");
            cr.Ascii().Should().Be("123");
            cr.Ascii().Should().Be("qrstuv");
            cr.Ascii().Should().Be("wxyz");
        });

        [Fact(Timeout = 1000)]
        public async Task AsciiImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
            string r;
            r = cr;
            r.Should().Be("abcdefg");
            r = cr;
            r.Should().Be("hijklmnop");
            r = cr;
            r.Should().Be("123");
            r = cr;
            r.Should().Be("qrstuv");
            r = cr;
            r.Should().Be("wxyz");
        });

        [Fact(Timeout = 1000)]
        public async Task String() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ
电脑😀
컴퓨터
");
            cr.String().Should().Be("abcdefg");
            cr.String().Should().Be("hijklmnop");
            cr.String().Should().Be("123");
            cr.String().Should().Be("qrstuv");
            cr.String().Should().Be("wxyz");
            cr.String().Should().Be("コンピュータ");
            cr.String().Should().Be("电脑😀");
            cr.String().Should().Be("컴퓨터");
        });

        [Fact(Timeout = 1000)]
        public async Task Mix() => await Task.Run(() =>
        {

            var cr = GetConsoleReader(@"

1 2 3 4 5 6.0
| a | b | b |
7 8 9
-10 -11 -12
abc def
");
            cr.Int().Should().Be(1);
            cr.Long().Should().Be(2);
            cr.Int0().Should().Be(2);
            cr.Long0().Should().Be(3);
            cr.Char().Should().Be('5');
            cr.Double().Should().Be(6);
            cr.Line().Should().Be("| a | b | b |");
            cr.Split().Int().Should().Equal(7, 8, 9);
            cr.Repeat(3).Long().Should().Equal(-10, -11, -12);
            cr.Ascii().Should().Be("abc");
            cr.String().Should().Be("def");
        });
    }
}
