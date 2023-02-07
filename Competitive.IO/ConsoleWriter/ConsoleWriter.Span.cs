#if NETSTANDARD2_1
using System;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    public partial class ConsoleWriter
    {
        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public ConsoleWriter WriteLine(ReadOnlySpan<char> v) { sw.WriteLine(v); return this; }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public ConsoleWriter WriteLineJoin<T>(Span<T> col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public ConsoleWriter WriteLineJoin<T>(ReadOnlySpan<T> col) => WriteMany(' ', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public ConsoleWriter WriteLines<T>(Span<T> col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public ConsoleWriter WriteLines<T>(ReadOnlySpan<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [M(256)]
        private ConsoleWriter WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            if (col.Length > 0)
            {
                sw.Write(col[0].ToString());
                foreach (var c in col.Slice(1))
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
