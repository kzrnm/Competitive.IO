using System.Threading;
using System.Threading.Tasks;
using static Kzrnm.Competitive.IO.Reader.Helpers;

namespace Kzrnm.Competitive.IO.Reader;

public class ConsoleReaderGridTests
{
    [Test]
    [Timeout(5000)]
    public async Task ConsoleReader(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid<int>(2, 6);
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([1, 2, 3, 4, 5, 6]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([7, 8, 9, 10, 11, 12]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task PropertyConsoleReader(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid<int>(2, 6);
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([1, 2, 3, 4, 5, 6]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([7, 8, 9, 10, 11, 12]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ConsoleReaderFunc(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid(2, 6, cr => cr.Int());
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([1, 2, 3, 4, 5, 6]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([7, 8, 9, 10, 11, 12]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task PropertyConsoleReaderFunc(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid(2, 6, cr => cr.Int);
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([1, 2, 3, 4, 5, 6]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([7, 8, 9, 10, 11, 12]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task ConsoleReaderFuncIndex(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid(2, 6, (cr, h, w) => (Value: cr.Int(), h, w));
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([(1, 0, 0), (2, 0, 1), (3, 0, 2), (4, 0, 3), (5, 0, 4), (6, 0, 5)]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([(7, 1, 0), (8, 1, 1), (9, 1, 2), (10, 1, 3), (11, 1, 4), (12, 1, 5)]);
    }, cancellationToken);

    [Test]
    [Timeout(5000)]
    public async Task PropertyConsoleReaderFuncIndex(CancellationToken cancellationToken) => await Task.Run(async () =>
    {
        var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
        var grid = cr.Grid(2, 6, (cr, h, w) => (Value: cr.Int, h, w));
        await Assert.That(grid).Count().IsEqualTo(2);
        await Assert.That(grid[0]).IsStrictlyEquivalentTo([(1, 0, 0), (2, 0, 1), (3, 0, 2), (4, 0, 3), (5, 0, 4), (6, 0, 5)]);
        await Assert.That(grid[1]).IsStrictlyEquivalentTo([(7, 1, 0), (8, 1, 1), (9, 1, 2), (10, 1, 3), (11, 1, 4), (12, 1, 5)]);
    }, cancellationToken);

}
