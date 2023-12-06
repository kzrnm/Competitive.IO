using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    using W = Utf8ConsoleWriter;
    /// <summary>
    /// Output Writer
    /// </summary>
    public static class Utf8ConsoleWriterGrid
    {
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteGrid<T>(this W w, IEnumerable<IEnumerable<T>> cols)
        {
            foreach (var col in cols)
                w.WriteLineJoin(col);
            return w;
        }
        /// <summary>
        /// Write line each item of <paramref name="tuples"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteGrid<TTuple>(this W w, IEnumerable<TTuple> tuples) where TTuple : ITuple
        {
            foreach (var tup in tuples)
                w.WriteLineJoin(tup);
            return w;
        }
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteGrid<T>(this W w, T[,] cols)
            => WriteGrid(w, MemoryMarshal.CreateReadOnlySpan(ref cols[0, 0], cols.Length), cols.GetLength(1));
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteGrid<T>(this W w, ReadOnlySpan<T> s, int width)
        {
            do
            {
                if (s.Length <= width)
                {
                    w.WriteLineJoin(s);
                    return w;
                }
#if NET6_0_OR_GREATER
                w.WriteLineJoin(s[..width]);
                s = s[width..];
#else
                w.WriteLineJoin(s.Slice(0, width));
                s = s.Slice(width);
#endif
            } while (true);
        }
    }
}
