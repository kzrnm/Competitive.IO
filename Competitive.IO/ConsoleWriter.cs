using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Output Writer
    /// </summary>
    public partial class ConsoleWriter : IDisposable
    {
        private const int DefaultBufferSize = 1 << 12;
        /// <summary>
        /// Implements writer
        /// </summary>
        public StreamWriter StreamWriter { get; }
        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// <para>Output stream: <see cref="Console.OpenStandardOutput()"/></para>
        /// <para>Output encoding: <see cref="Console.OutputEncoding"/></para>
        /// </summary>
        [MI(256)] public ConsoleWriter() : this(Console.OpenStandardOutput(), Console.OutputEncoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="encoding">Output encoding</param>
        [MI(256)] public ConsoleWriter(Stream output, Encoding encoding) : this(output, encoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="encoding">Output encoding</param>
        /// <param name="bufferSize">Output buffer size</param>
        [MI(256)]
        public ConsoleWriter(Stream output, Encoding encoding, int bufferSize)
        {
            StreamWriter = new StreamWriter(output, encoding, bufferSize);
        }

        /// <summary>
        /// Flush output stream.
        /// </summary>
        [MI(256)] public void Flush() => StreamWriter.Flush();

        /// <summary>
        /// Write empty line to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLine()
        {
            StreamWriter.WriteLine();
            return this;
        }

        /// <summary>
        /// Write <paramref name="obj"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLine<T>(T obj)
        {
            StreamWriter.WriteLine(obj.ToString());
            return this;
        }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T>(IEnumerable<T> col) => WriteMany(' ', col);

        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T1, T2>((T1, T2) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2);
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T1, T2, T3>((T1, T2, T3) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2, tuple.Item3);
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin<T1, T2, T3, T4>((T1, T2, T3, T4) tuple) => WriteLineJoin(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
#if NETSTANDARD2_1
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLineJoin<TTuple>(TTuple tuple) where TTuple : System.Runtime.CompilerServices.ITuple
        {
            var col = new object[tuple.Length];
            for (int i = 0; i < col.Length; i++)
                col[i] = tuple[i];
            return WriteLineJoin(col);
        }
#endif
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLineJoin(params object[] col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="v1"/> and <paramref name="v2"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLineJoin<T1, T2>(T1 v1, T2 v2)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v2.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/> and <paramref name="v3"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLineJoin<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v2.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v3.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/>, <paramref name="v3"/> and <paramref name="v4"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteLineJoin<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v2.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v3.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v4.ToString()); return this;
        }
        /// <summary>
        /// Write line each item of <paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public ConsoleWriter WriteLines<T>(IEnumerable<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public ConsoleWriter WriteGrid<T>(IEnumerable<IEnumerable<T>> cols)
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
        [MI(256)]
        public ConsoleWriter WriteGrid<TTuple>(IEnumerable<TTuple> tuples) where TTuple : System.Runtime.CompilerServices.ITuple
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
        [MI(256)]
        protected ConsoleWriter WriteMany<T>(char sep, IEnumerable<T> col)
        {
            var en = col.GetEnumerator();
            if (!en.MoveNext())
                goto End;
            StreamWriter.Write(en.Current.ToString());
            while (en.MoveNext())
            {
                StreamWriter.Write(sep);
                StreamWriter.Write(en.Current.ToString());
            }
        End: StreamWriter.WriteLine();
            return this;
        }

        /// <summary>
        /// Calls <see cref="StreamWriter.Flush()"/>
        /// </summary>
        [MI(256)] public void Dispose() => Flush();
    }
}
