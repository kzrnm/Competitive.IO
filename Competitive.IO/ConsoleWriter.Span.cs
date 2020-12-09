#if !NETSTANDARD1_3
using System;

namespace Kzrnm.Competitive.IO
{
    public partial class ConsoleWriter
    {
        /// <summary>
        /// Write <paramref name="obj"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLine(ReadOnlySpan<char> obj) { StreamWriter.WriteLine(obj); return this; }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T>(Span<T> col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T>(ReadOnlySpan<T> col) => WriteMany(' ', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLines<T>(Span<T> col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLines<T>(ReadOnlySpan<T> col) => WriteMany('\n', col);
        protected ConsoleWriter WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            var en = col.GetEnumerator();
            if (!en.MoveNext())
                return this;
            StreamWriter.Write(en.Current.ToString());
            while (en.MoveNext())
            {
                StreamWriter.Write(sep);
                StreamWriter.Write(en.Current.ToString());
            }
            StreamWriter.WriteLine();
            return this;
        }
    }
}
#endif
