using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Get grid extension class.
    /// </summary>
    public static class PropertyReaderGrid
    {
        /// <summary>
        /// Get <paramref name="H"/> × <paramref name="W"/> grid.
        /// </summary>
        [MI(256)]
        public static T[][] Grid<T>(this PropertyConsoleReader cr, int H, int W, Func<PropertyConsoleReader, T> factory)
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
        public static T[][] Grid<T>(this PropertyConsoleReader cr, int H, int W, Func<PropertyConsoleReader, int, int, T> factory)
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
