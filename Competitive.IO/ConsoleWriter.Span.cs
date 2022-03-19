#if NETSTANDARD2_1
using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    public partial class ConsoleWriter
    {
        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLine(ReadOnlySpan<char> v) { sw.WriteLine(v); return this; }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T>(Span<T> col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T>(ReadOnlySpan<T> col) => WriteMany(' ', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLines<T>(Span<T> col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLines<T>(ReadOnlySpan<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [MI(256)]
        protected ConsoleWriter WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            var en = col.GetEnumerator();
            if (!en.MoveNext())
                return this;
            sw.Write(en.Current.ToString());
            while (en.MoveNext())
            {
                sw.Write(sep);
                sw.Write(en.Current.ToString());
            }
            sw.WriteLine();
            return this;
        }
    }
}
#endif
