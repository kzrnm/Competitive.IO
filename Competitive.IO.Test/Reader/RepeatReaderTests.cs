using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kzrnm.Competitive.IO.Reader;
using Xunit;

namespace Kzrnm.Competitive.IO.Reader
{
    public class RepeatReaderTests
    {
        protected virtual ConsoleReader GetConsoleReader(string v)
            => Helpers.GetConsoleReader(v);
        protected virtual ConsoleReader GetConsoleReader(string v, int bufferSize)
            => Helpers.GetConsoleReader(v, bufferSize);

        [Fact(Timeout = 5000)]
        public async Task Select() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int)>(c => (c.Int(), c))
            .ShouldBe([(123, -14421), (-2147483647, 2147483647), (1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectWithIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            cr.Repeat(3).Select<(int, int, int)>((c, i) => (i, c.Int(), c))
            .ShouldBe([(0, 123, -14421), (1, -2147483647, 2147483647), (2, 1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Grid() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, c => c.Int());
            grid[0].ShouldBe([123, -14421]);
            grid[1].ShouldBe([-2147483647, 2147483647]);
            grid[2].ShouldBe([1, 2]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task GridWithIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1 2
");
            var grid = cr.Grid(3, 2, (c, i, j) => (i, j, c.Int()));
            grid[0].ShouldBe([(0, 0, 123), (0, 1, -14421)]);
            grid[1].ShouldBe([(1, 0, -2147483647), (1, 1, 2147483647)]);
            grid[2].ShouldBe([(2, 0, 1), (2, 1, 2)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray2() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1
-2 2
-3 3
");
            var (a, b) = cr.Repeat(3).SelectArray(c => (c.Int(), c.Int()));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray3() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1 a
-2 2 b
-3 3 c
");
            var (a, b, c) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char()));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
            c.ShouldBe(['a', 'b', 'c']);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task SelectArray4() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
-1 1 a 0.5
-2 2 b 1.5
-3 3 c 1e8
");
            var (a, b, c, d) = cr.Repeat(3).SelectArray(cc => (cc.Int(), cc.Int(), cr.Char(), cr.Double()));
            a.ShouldBe([-1, -2, -3]);
            b.ShouldBe([1, 2, 3]);
            c.ShouldBe(['a', 'b', 'c']);
            d.ShouldBe([0.5, 1.5, 1e8]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Int() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int().ShouldBe([123, -14421, -2147483647, 2147483647]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task IntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            int[] r = cr.Repeat(4);
            r.ShouldBe([123, -14421, -2147483647, 2147483647]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Int0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
1
");
            cr.Repeat(4).Int0().ShouldBe([122, -14422, -2147483648, 2147483646]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UInt() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).UInt().ShouldBe([123, 14421, 0, 4294967295]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UIntImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            uint[] r = cr.Repeat(4);
            r.ShouldBe([123, 14421, 0, 4294967295]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task UInt0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
0 4294967295
1
");
            cr.Repeat(4).UInt0().ShouldBe([122, 14420, 4294967295, 4294967294]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long().ShouldBe([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task LongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            long[] r = cr.Repeat(4);
            r.ShouldBe([123L, -14421L, -9223372036854775808L, 9223372036854775807L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Long0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"


123 -14421
-9223372036854775808 9223372036854775807
1
");
            cr.Repeat(4).Long0().ShouldBe([122L, -14422L, 9223372036854775807L, 9223372036854775806L]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong().ShouldBe([123, 14421, 9223372036854775808, 18446744073709551615]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULongImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            ulong[] r = cr.Repeat(4);
            r.ShouldBe([123, 14421, 9223372036854775808, 18446744073709551615]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ULong0() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615 456789
");
            cr.Repeat(4).ULong0().ShouldBe([122, 14420, 9223372036854775807, 18446744073709551614]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Double() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Double().ShouldBe([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task DoubleImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            double[] r = cr.Repeat(8);
            r.ShouldBe([123, -14421, -123456789123456789123456789.0, 123456789123456789123456789.0, -0.000123456, -.000123456, 0.000123456, .000123456]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Decimal() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            cr.Repeat(8).Decimal().ShouldBe([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task DecimalImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

123 -14421 -123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456 0.000123456 .000123456
1.0
");
            decimal[] r = cr.Repeat(8);
            r.ShouldBe([123m, -14421m, -123456789123456789123456789.0m, 123456789123456789123456789.0m, -0.000123456m, -.000123456m, 0.000123456m, .000123456m]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task Ascii() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).Ascii().ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task AsciiImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
            string[] r = cr.Repeat(5);
            r.ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task String() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).String().ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task Line() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(4).Line().ShouldBe(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task AsciiChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
-----
");
            cr.Repeat(5).AsciiChars().Select(c => new string(c)).ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task AsciiCharsImplicit() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz

");
            char[][] r = cr.Repeat(5);
            r.Select(c => new string(c)).ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task StringChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(8).StringChars().Select(c => new string(c)).ShouldBe(["abcdefg", "hijklmnop", "123", "qrstuv", "wxyz", "コンピュータ", "电脑😀", "컴퓨터"]);
        }, TestContext.Current.CancellationToken);


        [Fact(Timeout = 5000)]
        public async Task LineChars() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ 电脑😀 컴퓨터
-------
");
            cr.Repeat(4).LineChars().Select(c => new string(c)).ShouldBe(["abcdefg hijklmnop 123", "qrstuv wxyz", "コンピュータ 电脑😀 컴퓨터", "-------"]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 10000)]
        public async Task RandomLong() => await Task.Run(() =>
        {
            var rnd = new Random(GetType().GetHashCode());
            for (int q = 0; q < 1000; q++)
            {
                var list = new List<long>();
                var sb = new StringBuilder();
                for (int s = rnd.Next(100, 1000); s >= 0; s--)
                {
                    sb.Append(rnd.Next(100) switch
                    {
                        < 10 => "\n",
                        < 30 => "  ",
                        _ => " ",
                    });
                    var value = unchecked(((long)rnd.Next() << 33) | (long)rnd.Next());
                    sb.Append(value);
                    list.Add(value);
                }
                var cr = GetConsoleReader(sb.ToString(), 50);
                cr.Repeat(list.Count).Long().ShouldBe(list);
            }
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 10000)]
        public async Task RandomString() => await Task.Run(() =>
        {
            var rnd = new Random(GetType().GetHashCode());
            for (int q = 0; q < 1000; q++)
            {
                var list = new List<string>();
                var sb = new StringBuilder();
                for (int s = rnd.Next(100, 1000); s >= 0; s--)
                {
                    sb.Append(rnd.Next(100) switch
                    {
                        < 10 => "\n",
                        < 30 => "  ",
                        _ => " ",
                    });
                    var value = new string(Enumerable.Repeat(rnd, rnd.Next(10, 60)).Select(rnd => (char)rnd.Next('a', 'z')).ToArray());
                    sb.Append(value);
                    list.Add(value);
                }
                var cr = GetConsoleReader(sb.ToString(), 50);
                cr.Repeat(list.Count).String().ShouldBe(list);
            }
        }, TestContext.Current.CancellationToken);
    }
}
