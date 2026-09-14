using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO.Reader;

public class ConsoleReaderTests
{
    protected virtual ConsoleReader GetConsoleReader(string v)
        => Helpers.GetConsoleReader(v);
    protected virtual ConsoleReader GetConsoleReader(string v, int bufferSize)
        => Helpers.GetConsoleReader(v, bufferSize);

    [Test]
    [Timeout(5000)]
    public async Task Line(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

1 2 3 4 5 6
| a | b | b |
");
        await Assert.That(cr.Line()).IsEqualTo("1 2 3 4 5 6");
        await Assert.That(cr.Line()).IsEqualTo("| a | b | b |");
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task LineChars(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

1 2 3 4 5 6
| a | b | b |
");
        await Assert.That(cr.LineChars()).IsStrictlyEquivalentTo("1 2 3 4 5 6".ToCharArray());
        await Assert.That(cr.LineChars()).IsStrictlyEquivalentTo("| a | b | b |".ToCharArray());
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Char(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

a b c
def
");
        await Assert.That(cr.Char()).IsEqualTo('a');
        await Assert.That(cr.Char()).IsEqualTo('b');
        await Assert.That(cr.Char()).IsEqualTo('c');
        await Assert.That(cr.Char()).IsEqualTo('d');
        await Assert.That(cr.Char()).IsEqualTo('e');
        await Assert.That(cr.Char()).IsEqualTo('f');
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Int(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
        await Assert.That(cr.Int()).IsEqualTo(123);
        await Assert.That(cr.Int()).IsEqualTo(-14421);
        await Assert.That(cr.Int()).IsEqualTo(-2147483647);
        await Assert.That(cr.Int()).IsEqualTo(2147483647);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task IntImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
        int r;
        r = cr;
        await Assert.That(r).IsEqualTo(123);
        r = cr;
        await Assert.That(r).IsEqualTo(-14421);
        r = cr;
        await Assert.That(r).IsEqualTo(-2147483647);
        r = cr;
        await Assert.That(r).IsEqualTo(2147483647);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Int0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-2147483647 2147483647
");
        await Assert.That(cr.Int0()).IsEqualTo(122);
        await Assert.That(cr.Int0()).IsEqualTo(-14422);
        await Assert.That(cr.Int0()).IsEqualTo(-2147483648);
        await Assert.That(cr.Int0()).IsEqualTo(2147483646);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task UInt(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        await Assert.That(cr.UInt()).IsEqualTo(123U);
        await Assert.That(cr.UInt()).IsEqualTo(14421U);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UIntImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        uint r;
        r = cr;
        await Assert.That(r).IsEqualTo(123U);
        r = cr;
        await Assert.That(r).IsEqualTo(14421U);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task UInt0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        await Assert.That(cr.UInt()).IsEqualTo(123U);
        await Assert.That(cr.UInt()).IsEqualTo(14421U);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
        await Assert.That(cr.Long()).IsEqualTo(123);
        await Assert.That(cr.Long()).IsEqualTo(-14421);
        await Assert.That(cr.Long()).IsEqualTo(-9223372036854775808);
        await Assert.That(cr.Long()).IsEqualTo(9223372036854775807);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task LongImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
        long r;
        r = cr;
        await Assert.That(r).IsEqualTo(123);
        r = cr;
        await Assert.That(r).IsEqualTo(-14421);
        r = cr;
        await Assert.That(r).IsEqualTo(-9223372036854775808);
        r = cr;
        await Assert.That(r).IsEqualTo(9223372036854775807);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Long0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-9223372036854775808 9223372036854775807
");
        await Assert.That(cr.Long0()).IsEqualTo(122);
        await Assert.That(cr.Long0()).IsEqualTo(-14422);
        await Assert.That(cr.Long0()).IsEqualTo(9223372036854775807);
        await Assert.That(cr.Long0()).IsEqualTo(9223372036854775806);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        await Assert.That(cr.ULong()).IsEqualTo(123u);
        await Assert.That(cr.ULong()).IsEqualTo(14421u);
        await Assert.That(cr.ULong()).IsEqualTo(9223372036854775808u);
        await Assert.That(cr.ULong()).IsEqualTo(18446744073709551615u);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULongImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        ulong r;
        r = cr;
        await Assert.That(r).IsEqualTo(123u);
        r = cr;
        await Assert.That(r).IsEqualTo(14421u);
        r = cr;
        await Assert.That(r).IsEqualTo(9223372036854775808u);
        r = cr;
        await Assert.That(r).IsEqualTo(18446744073709551615u);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ULong0(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 14421
9223372036854775808 18446744073709551615
");
        await Assert.That(cr.ULong()).IsEqualTo(123u);
        await Assert.That(cr.ULong()).IsEqualTo(14421u);
        await Assert.That(cr.ULong()).IsEqualTo(9223372036854775808u);
        await Assert.That(cr.ULong()).IsEqualTo(18446744073709551615u);
    }, cancellationToken);


    [Test]
    [Timeout(5000)]
    public async Task Double(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
        await Assert.That(cr.Double()).IsEqualTo(123.0);
        await Assert.That(cr.Double()).IsEqualTo(-14421.0);
        await Assert.That(cr.Double()).IsEqualTo(-123456789123456789123456789.0);
        await Assert.That(cr.Double()).IsEqualTo(123456789123456789123456789.0);
        await Assert.That(cr.Double()).IsEqualTo(-0.000123456);
        await Assert.That(cr.Double()).IsEqualTo(-.000123456);
        await Assert.That(cr.Double()).IsEqualTo(0.000123456);
        await Assert.That(cr.Double()).IsEqualTo(.000123456);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DoubleImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
        double r;
        r = cr;
        await Assert.That(r).IsEqualTo(123.0);
        r = cr;
        await Assert.That(r).IsEqualTo(-14421.0);
        r = cr;
        await Assert.That(r).IsEqualTo(-123456789123456789123456789.0);
        r = cr;
        await Assert.That(r).IsEqualTo(123456789123456789123456789.0);
        r = cr;
        await Assert.That(r).IsEqualTo(-0.000123456);
        r = cr;
        await Assert.That(r).IsEqualTo(-.000123456);
        r = cr;
        await Assert.That(r).IsEqualTo(0.000123456);
        r = cr;
        await Assert.That(r).IsEqualTo(.000123456);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Decimal(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
        await Assert.That(cr.Decimal()).IsEqualTo(123.0m);
        await Assert.That(cr.Decimal()).IsEqualTo(-14421.0m);
        await Assert.That(cr.Decimal()).IsEqualTo(-123456789123456789123456789.0m);
        await Assert.That(cr.Decimal()).IsEqualTo(123456789123456789123456789.0m);
        await Assert.That(cr.Decimal()).IsEqualTo(-0.000123456m);
        await Assert.That(cr.Decimal()).IsEqualTo(-.000123456m);
        await Assert.That(cr.Decimal()).IsEqualTo(0.000123456m);
        await Assert.That(cr.Decimal()).IsEqualTo(.000123456m);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task DecimalImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

123 -14421
-123456789123456789123456789 123456789123456789123456789
-0.000123456 -.000123456
0.000123456 .000123456
");
        decimal r;
        r = cr;
        await Assert.That(r).IsEqualTo(123.0m);
        r = cr;
        await Assert.That(r).IsEqualTo(-14421.0m);
        r = cr;
        await Assert.That(r).IsEqualTo(-123456789123456789123456789.0m);
        r = cr;
        await Assert.That(r).IsEqualTo(123456789123456789123456789.0m);
        r = cr;
        await Assert.That(r).IsEqualTo(-0.000123456m);
        r = cr;
        await Assert.That(r).IsEqualTo(-.000123456m);
        r = cr;
        await Assert.That(r).IsEqualTo(0.000123456m);
        r = cr;
        await Assert.That(r).IsEqualTo(.000123456m);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Ascii(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("abcdefg");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("hijklmnop");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("123");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("qrstuv");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("wxyz");
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task AsciiImplicit(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
");
        Asciis r;
        r = cr;
        await Assert.That(r).IsStrictlyEquivalentTo("abcdefg");
        r = cr;
        await Assert.That(r).IsStrictlyEquivalentTo("hijklmnop");
        r = cr;
        await Assert.That(r).IsStrictlyEquivalentTo("123");
        r = cr;
        await Assert.That(r).IsStrictlyEquivalentTo("qrstuv");
        r = cr;
        await Assert.That(r).IsStrictlyEquivalentTo("wxyz");
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task String(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ
电脑😀
컴퓨터
");
        await Assert.That(cr.String()).IsEqualTo("abcdefg");
        await Assert.That(cr.String()).IsEqualTo("hijklmnop");
        await Assert.That(cr.String()).IsEqualTo("123");
        await Assert.That(cr.String()).IsEqualTo("qrstuv");
        await Assert.That(cr.String()).IsEqualTo("wxyz");
        await Assert.That(cr.String()).IsEqualTo("コンピュータ");
        await Assert.That(cr.String()).IsEqualTo("电脑😀");
        await Assert.That(cr.String()).IsEqualTo("컴퓨터");
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task StringChars(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"

abcdefg hijklmnop 123
qrstuv wxyz
コンピュータ
电脑😀
컴퓨터
");
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("abcdefg".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("hijklmnop".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("123".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("qrstuv".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("wxyz".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("コンピュータ".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("电脑😀".ToCharArray());
        await Assert.That(cr.StringChars()).IsStrictlyEquivalentTo("컴퓨터".ToCharArray());
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Mix(CancellationToken cancellationToken) => await Task.Run(async () =>
    {

        var cr = GetConsoleReader(@"

1 2 3 4 5 6.0 8 9 10.1
| a | b | b |
7 8 9
-10 -11 -12
abc def
");
        await Assert.That(cr.Int()).IsEqualTo(1);
        await Assert.That(cr.Long()).IsEqualTo(2);
        await Assert.That(cr.UInt()).IsEqualTo(3u);
        await Assert.That(cr.ULong()).IsEqualTo(4u);
        await Assert.That(cr.Char()).IsEqualTo('5');
        await Assert.That(cr.Double()).IsEqualTo(6);
        await Assert.That(cr.Int0()).IsEqualTo(7);
        await Assert.That(cr.Long0()).IsEqualTo(8);
        await Assert.That(cr.Decimal()).IsEqualTo(10.1m);
        await Assert.That(cr.Line()).IsEqualTo("| a | b | b |");
        await Assert.That(cr.Line()).IsEqualTo("7 8 9");
        await Assert.That(cr.Repeat(3).Long()).IsStrictlyEquivalentTo((long[])[-10, -11, -12]);
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("abc");
        await Assert.That(cr.String()).IsEqualTo("def");
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Read(CancellationToken cancellationToken) => await Task.Run(async () =>
    {

        var cr = GetConsoleReader(@"

1 2 3 4 5 6.0
abc
def
");
        await Assert.That(cr.Read<int>()).IsEqualTo(1);
        await Assert.That(cr.Read<long>()).IsEqualTo(2);
        await Assert.That(cr.Read<uint>()).IsEqualTo(3u);
        await Assert.That(cr.Read<ulong>()).IsEqualTo(4u);
        await Assert.That(cr.Read<char>()).IsEqualTo('5');
        await Assert.That(cr.Read<double>()).IsEqualTo(6);
        await Assert.That(cr.Read<string>()).IsEqualTo("abc");
        await Assert.That(cr.Read<char[]>()).IsStrictlyEquivalentTo(['d', 'e', 'f']);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    [Arguments(4)]
    [Arguments(5)]
    [Arguments(6)]
    [Arguments(7)]
    [Arguments(8)]
    public async Task FillEntireNumber(int remaining, CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var str = new string('a', (1 << 12) - remaining);
        var cr = GetConsoleReader(str + " 12345");
        await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo(str);
        await Assert.That(cr.Long()).IsEqualTo(12345);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task Single(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        {
            var cr = GetConsoleReader("12345");
            await Assert.That(cr.Ascii()).IsStrictlyEquivalentTo("12345");
        }
        {
            var cr = GetConsoleReader("12345");
            await Assert.That(cr.String()).IsEqualTo("12345");
        }
        {
            var cr = GetConsoleReader("12345");
            await Assert.That(cr.Line()).IsEqualTo("12345");
        }
        {
            var cr = GetConsoleReader("12345");
            await Assert.That(cr.Long()).IsEqualTo(12345);
        }
    }, cancellationToken);
}
