using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    using W = ConsoleWriter;
    /// <summary>
    /// Output Writer
    /// </summary>
    public sealed partial class ConsoleWriter : IDisposable
    {
        private const int BufSize = 1 << 12;
        private readonly StreamWriter sw;
        /// <summary>
        /// Implements writer
        /// </summary>
        public StreamWriter StreamWriter => sw;
        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// <para>Output stream: <see cref="Console.OpenStandardOutput()"/></para>
        /// <para>Output encoding: <see cref="Console.OutputEncoding"/></para>
        /// </summary>
        public ConsoleWriter() : this(Console.OpenStandardOutput(), Console.OutputEncoding) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="encoding">Output encoding</param>
        public ConsoleWriter(Stream output, Encoding encoding) : this(output, encoding, BufSize) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="encoding">Output encoding</param>
        /// <param name="bufferSize">Output buffer size</param>
        public ConsoleWriter(Stream output, Encoding encoding, int bufferSize)
        {
            sw = new StreamWriter(output, encoding, bufferSize);
        }

        /// <summary>
        /// Flush output stream.
        /// </summary>
        [M(256)] public void Flush() => sw.Flush();

        /// <summary>
        /// Calls <see cref="StreamWriter.Flush()"/>
        /// </summary>
        [M(256)] public void Dispose() => Flush();

        /// <summary>
        /// Write <paramref name="v"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W Write<T>(T v)
        {
            sw.Write(v.ToString());
            return this;
        }

        /// <summary>
        /// Write empty line to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine()
        {
            sw.WriteLine();
            return this;
        }

        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine<T>(T v)
        {
            sw.WriteLine(v.ToString());
            return this;
        }

        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin(params object[] col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="v1"/> and <paramref name="v2"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLineJoin<T1, T2>(T1 v1, T2 v2)
        {
            sw.Write(v1.ToString()); sw.Write(' ');
            sw.WriteLine(v2.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/> and <paramref name="v3"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLineJoin<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
        {
            sw.Write(v1.ToString()); sw.Write(' ');
            sw.Write(v2.ToString()); sw.Write(' ');
            sw.WriteLine(v3.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/>, <paramref name="v3"/> and <paramref name="v4"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLineJoin<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            sw.Write(v1.ToString()); sw.Write(' ');
            sw.Write(v2.ToString()); sw.Write(' ');
            sw.Write(v3.ToString()); sw.Write(' ');
            sw.WriteLine(v4.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T>(IEnumerable<T> col) => WriteMany(' ', col);

        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T1, T2>((T1, T2) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2);
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T1, T2, T3>((T1, T2, T3) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2, tuple.Item3);
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLineJoin<T1, T2, T3, T4>((T1, T2, T3, T4) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
#if NETSTANDARD2_1
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLineJoin<TTuple>(TTuple tuple) where TTuple : ITuple
        {
            var col = new object[tuple.Length];
            for (int i = 0; i < col.Length; i++)
                col[i] = tuple[i];
            return WriteLineJoin(col);
        }
#endif

        /// <summary>
        /// Write line each item of <paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLines<T>(IEnumerable<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteGrid<T>(IEnumerable<IEnumerable<T>> cols)
        {
            foreach (var col in cols)
                WriteLineJoin(col);
            return this;
        }
#if NETSTANDARD2_1
        /// <summary>
        /// Write line each item of <paramref name="tuples"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteGrid<TTuple>(IEnumerable<TTuple> tuples) where TTuple : ITuple
        {
            foreach (var tup in tuples)
                WriteLineJoin(tup);
            return this;
        }
#endif
        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [M(256)]
        private W WriteMany<T>(char sep, IEnumerable<T> col)
        {
            var en = col.GetEnumerator();
            if (en.MoveNext())
            {
                sw.Write(en.Current.ToString());
                while (en.MoveNext())
                {
                    sw.Write(sep);
                    sw.Write(en.Current.ToString());
                }
            }
            sw.WriteLine();
            return this;
        }
    }
}
