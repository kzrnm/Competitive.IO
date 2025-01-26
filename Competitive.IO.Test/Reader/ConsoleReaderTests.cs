using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kzrnm.Competitive.IO.Reader
{
    public class ConsoleReaderTests
    {
        protected virtual ConsoleReader GetConsoleReader(string v)
            => Helpers.GetConsoleReader(v);
        protected virtual ConsoleReader GetConsoleReader(string v, int bufferSize)
            => Helpers.GetConsoleReader(v, bufferSize);

        [Fact(Timeout = 5000)]
        public async Task Line() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

1 2 3 4 5 6
| a | b | b |
");
            cr.Line().ShouldBe("1 2 3 4 5 6");
            cr.Line().ShouldBe("| a | b | b |");
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task LineChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

1 2 3 4 5 6
| a | b | b |
");
            cr.LineChars().ShouldBe("1 2 3 4 5 6".ToCharArray());
            cr.LineChars().ShouldBe("| a | b | b |".ToCharArray());
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Char() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

a b c
def
");
            cr.Char().ShouldBe('a');
            cr.Char().ShouldBe('b');
            cr.Char().ShouldBe('c');
            cr.Char().ShouldBe('d');
            cr.Char().ShouldBe('e');
            cr.Char().ShouldBe('f');
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            cr.Int().ShouldBe(123);
            cr.Int().ShouldBe(-14421);
            cr.Int().ShouldBe(-2147483647);
            cr.Int().ShouldBe(2147483647);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task IntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            int r;
            r = cr;
            r.ShouldBe(123);
            r = cr;
            r.ShouldBe(-14421);
            r = cr;
            r.ShouldBe(-2147483647);
            r = cr;
            r.ShouldBe(2147483647);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Int0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
            cr.Int0().ShouldBe(122);
            cr.Int0().ShouldBe(-14422);
            cr.Int0().ShouldBe(-2147483648);
            cr.Int0().ShouldBe(2147483646);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task UInt() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.UInt().ShouldBe(123U);
            cr.UInt().ShouldBe(14421U);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UIntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            uint r;
            r = cr;
            r.ShouldBe(123U);
            r = cr;
            r.ShouldBe(14421U);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UInt0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.UInt().ShouldBe(123U);
            cr.UInt().ShouldBe(14421U);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            cr.Long().ShouldBe(123);
            cr.Long().ShouldBe(-14421);
            cr.Long().ShouldBe(-9223372036854775808);
            cr.Long().ShouldBe(9223372036854775807);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task LongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            long r;
            r = cr;
            r.ShouldBe(123);
            r = cr;
            r.ShouldBe(-14421);
            r = cr;
            r.ShouldBe(-9223372036854775808);
            r = cr;
            r.ShouldBe(9223372036854775807);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
            cr.Long0().ShouldBe(122);
            cr.Long0().ShouldBe(-14422);
            cr.Long0().ShouldBe(9223372036854775807);
            cr.Long0().ShouldBe(9223372036854775806);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.ULong().ShouldBe(123u);
            cr.ULong().ShouldBe(14421u);
            cr.ULong().ShouldBe(9223372036854775808u);
            cr.ULong().ShouldBe(18446744073709551615u);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            ulong r;
            r = cr;
            r.ShouldBe(123u);
            r = cr;
            r.ShouldBe(14421u);
            r = cr;
            r.ShouldBe(9223372036854775808u);
            r = cr;
            r.ShouldBe(18446744073709551615u);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
            cr.ULong().ShouldBe(123u);
            cr.ULong().ShouldBe(14421u);
            cr.ULong().ShouldBe(9223372036854775808u);
            cr.ULong().ShouldBe(18446744073709551615u);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            cr.Double().ShouldBe(123.0);
            cr.Double().ShouldBe(-14421.0);
            cr.Double().ShouldBe(-123456789123456789123456789.0);
            cr.Double().ShouldBe(123456789123456789123456789.0);
            cr.Double().ShouldBe(-0.000123456);
            cr.Double().ShouldBe(-.000123456);
            cr.Double().ShouldBe(0.000123456);
            cr.Double().ShouldBe(.000123456);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
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
            r.ShouldBe(123.0);
            r = cr;
            r.ShouldBe(-14421.0);
            r = cr;
            r.ShouldBe(-123456789123456789123456789.0);
            r = cr;
            r.ShouldBe(123456789123456789123456789.0);
            r = cr;
            r.ShouldBe(-0.000123456);
            r = cr;
            r.ShouldBe(-.000123456);
            r = cr;
            r.ShouldBe(0.000123456);
            r = cr;
            r.ShouldBe(.000123456);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
            cr.Decimal().ShouldBe(123.0m);
            cr.Decimal().ShouldBe(-14421.0m);
            cr.Decimal().ShouldBe(-123456789123456789123456789.0m);
            cr.Decimal().ShouldBe(123456789123456789123456789.0m);
            cr.Decimal().ShouldBe(-0.000123456m);
            cr.Decimal().ShouldBe(-.000123456m);
            cr.Decimal().ShouldBe(0.000123456m);
            cr.Decimal().ShouldBe(.000123456m);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
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
            r.ShouldBe(123.0m);
            r = cr;
            r.ShouldBe(-14421.0m);
            r = cr;
            r.ShouldBe(-123456789123456789123456789.0m);
            r = cr;
            r.ShouldBe(123456789123456789123456789.0m);
            r = cr;
            r.ShouldBe(-0.000123456m);
            r = cr;
            r.ShouldBe(-.000123456m);
            r = cr;
            r.ShouldBe(0.000123456m);
            r = cr;
            r.ShouldBe(.000123456m);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
            cr.Ascii().ShouldBe("abcdefg");
            cr.Ascii().ShouldBe("hijklmnop");
            cr.Ascii().ShouldBe("123");
            cr.Ascii().ShouldBe("qrstuv");
            cr.Ascii().ShouldBe("wxyz");
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task AsciiImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
            Asciis r;
            r = cr;
            r.ShouldBe("abcdefg");
            r = cr;
            r.ShouldBe("hijklmnop");
            r = cr;
            r.ShouldBe("123");
            r = cr;
            r.ShouldBe("qrstuv");
            r = cr;
            r.ShouldBe("wxyz");
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task String() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ
电脑😀
컴퓨터
");
            cr.String().ShouldBe("abcdefg");
            cr.String().ShouldBe("hijklmnop");
            cr.String().ShouldBe("123");
            cr.String().ShouldBe("qrstuv");
            cr.String().ShouldBe("wxyz");
            cr.String().ShouldBe("コンピュータ");
            cr.String().ShouldBe("电脑😀");
            cr.String().ShouldBe("컴퓨터");
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task StringChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ
电脑😀
컴퓨터
");
            cr.StringChars().ShouldBe("abcdefg".ToCharArray());
            cr.StringChars().ShouldBe("hijklmnop".ToCharArray());
            cr.StringChars().ShouldBe("123".ToCharArray());
            cr.StringChars().ShouldBe("qrstuv".ToCharArray());
            cr.StringChars().ShouldBe("wxyz".ToCharArray());
            cr.StringChars().ShouldBe("コンピュータ".ToCharArray());
            cr.StringChars().ShouldBe("电脑😀".ToCharArray());
            cr.StringChars().ShouldBe("컴퓨터".ToCharArray());
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Mix() => await Task.Run(() =>
        {

            var cr = GetConsoleReader(@"

1 2 3 4 5 6.0 8 9 10.1
| a | b | b |
7 8 9
-10 -11 -12
abc def
");
            cr.Int().ShouldBe(1);
            cr.Long().ShouldBe(2);
            cr.UInt().ShouldBe(3u);
            cr.ULong().ShouldBe(4u);
            cr.Char().ShouldBe('5');
            cr.Double().ShouldBe(6);
            cr.Int0().ShouldBe(7);
            cr.Long0().ShouldBe(8);
            cr.Decimal().ShouldBe(10.1m);
            cr.Line().ShouldBe("| a | b | b |");
            cr.Line().ShouldBe("7 8 9");
            cr.Repeat(3).Long().ShouldBe([-10, -11, -12]);
            cr.Ascii().ShouldBe("abc");
            cr.String().ShouldBe("def");
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Read() => await Task.Run(() =>
        {

            var cr = GetConsoleReader(@"

1 2 3 4 5 6.0
abc
def
");
            cr.Read<int>().ShouldBe(1);
            cr.Read<long>().ShouldBe(2);
            cr.Read<uint>().ShouldBe(3u);
            cr.Read<ulong>().ShouldBe(4u);
            cr.Read<char>().ShouldBe('5');
            cr.Read<double>().ShouldBe(6);
            cr.Read<string>().ShouldBe("abc");
            cr.Read<char[]>().ShouldBe(['d', 'e', 'f']);
        }, TestContext.Current.CancellationToken);

        [Theory(Timeout = 5000)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async Task FillEntireNumber(int remaining) => await Task.Run(() =>
        {
            var str = new string('a', (1 << 12) - remaining);
            var cr = GetConsoleReader(str + " 12345");
            cr.Ascii().ShouldBe(str);
            cr.Long().ShouldBe(12345);
        }, TestContext.Current.CancellationToken);
    }
}
