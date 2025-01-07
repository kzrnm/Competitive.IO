#if !NETSTANDARD2_0
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    using W = ConsoleWriter;
    public partial class ConsoleWriter
    {
        /// <summary>
        /// Write <paramref name="v"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLine(ReadOnlySpan<char> v) { sw.WriteLine(v); return this; }
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T>(Span<T> col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T>(ReadOnlySpan<T> col) => WriteMany(' ', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLines<T>(Span<T> col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLines<T>(ReadOnlySpan<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteGrid<T>(T[,] cols)
        {
            var width = cols.GetLength(1);
#if NET6_0_OR_GREATER
            for (var s = MemoryMarshal.CreateReadOnlySpan(ref cols[0, 0], cols.Length); !s.IsEmpty; s = s[width..])
                WriteLineJoin(s[..width]);
#else
            for (var s = MemoryMarshal.CreateReadOnlySpan(ref cols[0, 0], cols.Length); !s.IsEmpty; s = s.Slice(width))
                WriteLineJoin(s.Slice(0, width));
#endif
            return this;
        }
        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [M(256)]
        private W WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            if (col.Length > 0)
            {
                sw.Write(col[0].ToString());
#if NET6_0_OR_GREATER
                foreach (var c in col[1..])
#else
                foreach (var c in col.Slice(1))
#endif
                {
                    sw.Write(sep);
                    sw.Write(c.ToString());
                }
            }
            sw.WriteLine();
            return this;
        }
    }
}
#endif
