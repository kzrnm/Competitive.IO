using System.Threading.Tasks;
using Xunit;
using static Kzrnm.Competitive.IO.Reader.Helpers;

namespace Kzrnm.Competitive.IO.Reader
{
    public class ConsoleReaderGridTests
    {
        [Fact(Timeout = 5000)]
        public async Task ConsoleReader() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid<int>(2, 6);
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([1, 2, 3, 4, 5, 6]);
            grid[1].ShouldBe([7, 8, 9, 10, 11, 12]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task PropertyConsoleReader() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid<int>(2, 6);
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([1, 2, 3, 4, 5, 6]);
            grid[1].ShouldBe([7, 8, 9, 10, 11, 12]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ConsoleReaderFunc() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid(2, 6, cr => cr.Int());
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([1, 2, 3, 4, 5, 6]);
            grid[1].ShouldBe([7, 8, 9, 10, 11, 12]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task PropertyConsoleReaderFunc() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid(2, 6, cr => cr.Int);
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([1, 2, 3, 4, 5, 6]);
            grid[1].ShouldBe([7, 8, 9, 10, 11, 12]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task ConsoleReaderFuncIndex() => await Task.Run(() =>
        {
            var cr = GetConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid(2, 6, (cr, h, w) => (Value: cr.Int(), h, w));
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([(1, 0, 0), (2, 0, 1), (3, 0, 2), (4, 0, 3), (5, 0, 4), (6, 0, 5)]);
            grid[1].ShouldBe([(7, 1, 0), (8, 1, 1), (9, 1, 2), (10, 1, 3), (11, 1, 4), (12, 1, 5)]);
        }, TestContext.Current.CancellationToken);

        [Fact(Timeout = 5000)]
        public async Task PropertyConsoleReaderFuncIndex() => await Task.Run(() =>
        {
            var cr = GetPropertyConsoleReader(@"
1 2 3 4 5 6
7 8 9 10 11 12
");
            var grid = cr.Grid(2, 6, (cr, h, w) => (Value: cr.Int, h, w));
            grid.Length.ShouldBe(2);
            grid[0].ShouldBe([(1, 0, 0), (2, 0, 1), (3, 0, 2), (4, 0, 3), (5, 0, 4), (6, 0, 5)]);
            grid[1].ShouldBe([(7, 1, 0), (8, 1, 1), (9, 1, 2), (10, 1, 3), (11, 1, 4), (12, 1, 5)]);
        }, TestContext.Current.CancellationToken);

    }
}
