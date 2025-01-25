using System;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using C = ConsoleReader;
    /// <summary>
    /// Get grid extension class.
    /// </summary>
    public static class ReaderGrid
    {
        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MethodImpl(256)]
        public static T[][] Grid<T>(this C cr, int H, int W)
        {
            var a = new T[H][];
            for (var i = 0; i < H; i++)
            {
                a[i] = new T[W];
                for (var j = 0; j < W; j++)
                    a[i][j] = cr.Read<T>();
            }
            return a;
        }

        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MethodImpl(256)]
        public static T[][] Grid<R, T>(this R cr, int H, int W, Func<R, T> factory) where R : C
        {
            var a = new T[H][];
            for (var i = 0; i < H; i++)
            {
                a[i] = new T[W];
                for (var j = 0; j < W; j++)
                    a[i][j] = factory(cr);
            }
            return a;
        }
        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MethodImpl(256)]
        public static T[][] Grid<R, T>(this R cr, int H, int W, Func<R, int, int, T> factory) where R : C
        {
            var a = new T[H][];
            for (var i = 0; i < H; i++)
            {
                a[i] = new T[W];
                for (var j = 0; j < W; j++)
                    a[i][j] = factory(cr, i, j);
            }
            return a;
        }
    }
}
