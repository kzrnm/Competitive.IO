using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Get grid extension class.
    /// </summary>
    public static class ReaderGrid
    {
        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MI(256)]
        public static T[][] Grid<T>(this ConsoleReader cr, int H, int W)
        {
            var arr = new T[H][];
            for (var i = 0; i < H; i++)
            {
                arr[i] = new T[W];
                for (var j = 0; j < W; j++)
                    arr[i][j] = cr.Read<T>();
            }
            return arr;
        }

        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MI(256)]
        public static T[][] Grid<R, T>(this R cr, int H, int W, Func<R, T> factory) where R : ConsoleReader
        {
            var arr = new T[H][];
            for (var i = 0; i < H; i++)
            {
                arr[i] = new T[W];
                for (var j = 0; j < W; j++)
                    arr[i][j] = factory(cr);
            }
            return arr;
        }
        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MI(256)]
        public static T[][] Grid<R, T>(this R cr, int H, int W, Func<R, int, int, T> factory) where R : ConsoleReader
        {
            var arr = new T[H][];
            for (var i = 0; i < H; i++)
            {
                arr[i] = new T[W];
                for (var j = 0; j < W; j++)
                    arr[i][j] = factory(cr, i, j);
            }
            return arr;
        }
    }
}
