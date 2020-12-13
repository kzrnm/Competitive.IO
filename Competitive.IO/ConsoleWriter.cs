using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// Output Writer
    /// </summary>
    public partial class ConsoleWriter : IDisposable
    {

        public StreamWriter StreamWriter { get; }
        /// <summary>
        /// <para>Output stream: <see cref="Console.OpenStandardOutput"/></para>
        /// <para>Output encoding: <see cref="Console.OutputEncoding"/></para>
        /// </summary>
        public ConsoleWriter() : this(Console.OpenStandardOutput(), Console.OutputEncoding) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="encoding">Output encoding</param>
        public ConsoleWriter(Stream output, Encoding encoding)
        {
            StreamWriter = new StreamWriter(output, encoding);
        }

        /// <summary>
        /// Flush output stream.
        /// </summary>
        public void Flush() => StreamWriter.Flush();

        /// <summary>
        /// Write <paramref name="obj"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLine<T>(T obj)
        {
            StreamWriter.WriteLine(obj.ToString());
            return this;
        }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T>(IEnumerable<T> col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T>(params T[] col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin(params object[] col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="v1"/> and <paramref name="v2"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T1, T2>(T1 v1, T2 v2)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v2.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/> and <paramref name="v3"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v2.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v3.ToString()); return this;
        }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/>, <paramref name="v3"/> and <paramref name="v4"/> to output stream.
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLineJoin<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            StreamWriter.Write(v1.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v2.ToString()); StreamWriter.Write(' ');
            StreamWriter.Write(v3.ToString()); StreamWriter.Write(' ');
            StreamWriter.WriteLine(v4.ToString()); return this;
        }
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns><see cref="this"/></returns>
        public ConsoleWriter WriteLines<T>(IEnumerable<T> col) => WriteMany('\n', col);
        public ConsoleWriter WriteLineGrid<T>(IEnumerable<IEnumerable<T>> cols)
        {
            foreach (var col in cols)
                WriteLineJoin(col);
            return this;
        }
        protected ConsoleWriter WriteMany<T>(char sep, IEnumerable<T> col)
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

        public void Dispose() => Flush();
    }
}
