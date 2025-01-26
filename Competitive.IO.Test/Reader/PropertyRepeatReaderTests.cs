using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kzrnm.Competitive.IO.Reader
{
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

        [Fact(Timeout = 5000)]
        public async Task SelectPropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int)>(c => (c.Int, c))
            .ShouldBe([(123, -14421), (-2147483647, 2147483647), (1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectWithIndexPropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int, int)>((c, i) => (i, c.Int, c))
            .ShouldBe([(0, 123, -14421), (1, -2147483647, 2147483647), (2, 1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task GridProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, c => c.Int);
            grid[0].ShouldBe([123, -14421]);
            grid[1].ShouldBe([-2147483647, 2147483647]);
            grid[2].ShouldBe([1, 2]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task GridWithIndexPropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, (c, i, j) => (i, j, c.Int));
            grid[0].ShouldBe([(0, 0, 123), (0, 1, -14421)]);
            grid[1].ShouldBe([(1, 0, -2147483647), (1, 1, 2147483647)]);
            grid[2].ShouldBe([(2, 0, 1), (2, 1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray2PropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
-1 1
-2 2
-3 3
");
            var (a, b) = cr.Repeat(3).SelectArray(c => (c.Int, c.Int));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray3PropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
-1 1 a
-2 2 b
-3 3 c
");
            var (a, b, c) = cr.Repeat(3).SelectArray(cc => (cc.Int, cc.Int, cr.Char));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
            c.ShouldBe(['a', 'b', 'c']);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray4PropProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
-1 1 a 0.5
-2 2 b 1.5
-3 3 c 1e8
");
            var (a, b, c, d) = cr.Repeat(3).SelectArray(cc => (cc.Int, cc.Int, cr.Char, cr.Double));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
            c.ShouldBe(['a', 'b', 'c']);
            d.ShouldBe([0.5, 1.5, 1e8]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task IntProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int.ShouldBe([123, -14421, -2147483647, 2147483647]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Int0Prop() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int0.ShouldBe([122, -14422, -2147483648, 2147483646]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task LongProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long.ShouldBe([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long0Prop() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long0.ShouldBe([122L, -14422L, 9223372036854775807L, 9223372036854775806L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULongProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong.ShouldBe([123, 14421, 9223372036854775808, 18446744073709551615]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong0Prop() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong0.ShouldBe([122, 14420, 9223372036854775807, 18446744073709551614]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task DoubleProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Double.ShouldBe([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, .000123456, .000123456]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task DecimalProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Decimal.ShouldBe([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task AsciiProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Ascii.ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task StringProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).String.ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task StringCharsProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).StringChars.Select(c => new string(c)).ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task LineCharsProp() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(4).LineChars.Select(c => new string(c)).ShouldBe(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
        }, TestContext.Current.CancellationToken);
    }
}
