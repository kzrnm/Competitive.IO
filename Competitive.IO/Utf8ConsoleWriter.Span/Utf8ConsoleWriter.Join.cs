using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    using W = Utf8ConsoleWriter;
    /// <summary>
    /// Output Writer
    /// </summary>
    public static class Utf8ConsoleWriterJoin
    {
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public static W WriteLineJoin(this W w, params object[] col) => w.WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public static W WriteLineJoin<T>(this W w, T[] col) => w.WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="v1"/> and <paramref name="v2"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2>(this W w, T1 v1, T2 v2) => w.Write(v1).Write(' ').WriteLine(v2);
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/> and <paramref name="v3"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2, T3>(this W w, T1 v1, T2 v2, T3 v3)
            => w.Write(v1).Write(' ').Write(v2).Write(' ').WriteLine(v3);
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/>, <paramref name="v3"/> and <paramref name="v4"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2, T3, T4>(this W w, T1 v1, T2 v2, T3 v3, T4 v4)
            => w.Write(v1).Write(' ').Write(v2).Write(' ').Write(v3).Write(' ').WriteLine(v4);
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public static W WriteLineJoin<T>(this W w, Span<T> col) => w.WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public static W WriteLineJoin<T>(this W w, ReadOnlySpan<T> col) => w.WriteMany(' ', col);

        /// <summary>
        /// Write joined <paramref name="col"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public static W WriteLineJoin<T>(this W w, IEnumerable<T> col) => w.WriteMany(' ', col);

        /// <summary>
        /// Write joined <paramref name="t"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2>(this W w, (T1, T2) t)
            => w.Write(t.Item1).Write(' ').WriteLine(t.Item2);
        /// <summary>
        /// Write joined <paramref name="t"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2, T3>(this W w, (T1, T2, T3) t)
            => w.Write(t.Item1).Write(' ').Write(t.Item2).Write(' ').WriteLine(t.Item3);
        /// <summary>
        /// Write joined <paramref name="t"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<T1, T2, T3, T4>(this W w, (T1, T2, T3, T4) t)
            => w.Write(t.Item1).Write(' ').Write(t.Item2).Write(' ').Write(t.Item3).Write(' ').WriteLine(t.Item4);
        /// <summary>
        /// Write joined <paramref name="t"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public static W WriteLineJoin<TTuple>(this W w, TTuple t) where TTuple : ITuple
        {
            var col = new object[t.Length];
            for (int i = 0; i < col.Length; i++)
            {
                if (i != 0) w.Write(' ');
                w.Write(t[i]);
            }
            return w.WriteLine();
        }
    }
}
