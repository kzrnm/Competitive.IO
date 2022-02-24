using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// <see cref="PropertyRepeatReader"/>
    /// </summary>
    public static class PropertyRepeatReaderGrid
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>() <paramref name="width"/> times per line
        /// </summary>
        [MI(256)]
        public static T[][] Grid<T>(this PropertyRepeatReader r, int width, Func<PropertyConsoleReader, T> factory)
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
        public static T[][] Grid<T>(this PropertyRepeatReader r, int width, Func<PropertyConsoleReader, int, int, T> factory)
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
