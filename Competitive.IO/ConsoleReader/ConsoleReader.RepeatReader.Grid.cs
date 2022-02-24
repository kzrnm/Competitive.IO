using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// <see cref="RepeatReader"/>
    /// </summary>
    public static class RepeatReaderGrid
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>() <paramref name="width"/> times per line
        /// </summary>
        [MI(256)]
        public static T[][] Grid<T>(this RepeatReader r, int width, Func<ConsoleReader, T> factory)
        {
            var arr = new T[r.count][];
            for (var i = 0; i < r.count; i++)
            {
                arr[i] = new T[width];
                for (var j = 0; j < width; j++)
                    arr[i][j] = factory(r.cr);
            }
            return arr;
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>() <paramref name="width"/> times per line
        /// </summary>
        [MI(256)]
        public static T[][] Grid<T>(this RepeatReader r, int width, Func<ConsoleReader, int, int, T> factory)
        {
            var arr = new T[r.count][];
            for (var i = 0; i < r.count; i++)
            {
                arr[i] = new T[width];
                for (var j = 0; j < width; j++)
                    arr[i][j] = factory(r.cr, i, j);
            }
            return arr;
        }
    }
}
