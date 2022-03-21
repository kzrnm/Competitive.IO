#if NETSTANDARD2_1_OR_GREATER
using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using _W = Utf8ConsoleWriter;
    using static Utf8Formatter;
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Output Writer
    /// </summary>
    public sealed class Utf8ConsoleWriter : IDisposable
    {
        internal static readonly UTF8Encoding Utf8NoBom = new UTF8Encoding(false);
        private const int BufSize = 1 << 12;
        private readonly Stream output;
        private byte[] buf;
        private int len;

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// <para>Output stream: <see cref="Console.OpenStandardOutput()"/></para>
        /// <para>Output encoding: <see cref="Console.OutputEncoding"/></para>
        /// </summary>
        public Utf8ConsoleWriter() : this(Console.OpenStandardOutput()) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        public Utf8ConsoleWriter(Stream output) : this(output, BufSize) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        /// <param name="bufferSize">Output buffer size</param>
        public Utf8ConsoleWriter(Stream output, int bufferSize)
        {
            this.output = output;
            buf = new byte[bufferSize];
        }

        /// <summary>
        /// Create <see cref="StreamWriter"/> instance.
        /// </summary>
        public StreamWriter ToStreamWriter()
        {
            Flush();
            return new StreamWriter(output, Utf8NoBom);
        }

        /// <summary>
        /// Flush output stream.
        /// </summary>
        [MI(256)]
        public void Flush() => output.Write(buf, 0, len);

        /// <summary>
        /// Calls <see cref="Flush()"/>
        /// </summary>
        void IDisposable.Dispose() => Flush();

        /// <summary>
        /// Ensure buffer span
        /// </summary>
        [MI(256)]
        private Span<byte> EnsureBuf(int size)
        {
            if (buf.Length - len < size)
            {
                Flush();
                len = 0;
            }
            return buf.AsSpan(len, size);
        }

        /// <summary>
        /// Write <paramref name="v"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256 | 512)]
        public _W Write<T>(T v)
        {
            Span<byte> dst = EnsureBuf(32);
            int bw;
            if (typeof(T) == typeof(int)) TryFormat((int)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(long)) TryFormat((long)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(uint)) TryFormat((uint)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(ulong)) TryFormat((ulong)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(short)) TryFormat((short)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(ushort)) TryFormat((ushort)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(byte)) TryFormat((byte)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(sbyte)) TryFormat((sbyte)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(float)) TryFormat((float)(object)v, EnsureBuf(32), out bw, new StandardFormat('F', 16));
            else if (typeof(T) == typeof(double)) TryFormat((double)(object)v, EnsureBuf(32), out bw, new StandardFormat('F', 16));
            else if (typeof(T) == typeof(decimal)) TryFormat((decimal)(object)v, EnsureBuf(32), out bw);
            else if (typeof(T) == typeof(char))
            {
                var c = (char)(object)v;
                if (c < 0x007f)
                {
                    dst[0] = (byte)c;
                    bw = 1;
                }
                else
                {
                    Span<char> s = stackalloc char[1] { c };
                    bw = Utf8NoBom.GetBytes(s, dst);
                }
            }
            else if (v is char[] charr)
                return Write(charr.AsSpan());
            else if (v is IUtf8ConsoleWriterFormatter f)
            {
                f.Write(this);
                return this;
            }
            else
                return Write(v.ToString().AsSpan());
            len += bw;
            return this;
        }
        /// <summary>
        /// Write <paramref name="v"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256 | 512)]
        public _W Write(ReadOnlySpan<char> v)
        {
            var mlen = Utf8NoBom.GetMaxByteCount(v.Length);
            if (mlen > buf.Length)
            {
                Flush();
                buf = new byte[mlen * 2];
                len = 0;
            }
            else if (mlen > buf.Length - len)
            {
                Flush();
                len = 0;
            }
            var bw = Utf8NoBom.GetBytes(v, buf.AsSpan(len));
            len += bw;
            return this;
        }
        /// <summary>
        /// Write empty line to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLine() => Write('\n');
        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLine<T>(T v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLine(ReadOnlySpan<char> v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLineJoin<T>(IEnumerable<T> col) => WriteMany(' ', col);

        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2>((T1, T2) tuple) { Write(tuple.Item1); Write(' '); return WriteLine(tuple.Item2); }
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2, T3>((T1, T2, T3) tuple) { Write(tuple.Item1); Write(' '); Write(tuple.Item2); Write(' '); return WriteLine(tuple.Item3); }
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2, T3, T4>((T1, T2, T3, T4) tuple) { Write(tuple.Item1); Write(' '); Write(tuple.Item2); Write(' '); Write(tuple.Item3); Write(' '); return WriteLine(tuple.Item4); }
        /// <summary>
        /// Write joined <paramref name="tuple"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<TTuple>(TTuple tuple) where TTuple : ITuple
        {
            var col = new object[tuple.Length];
            for (int i = 0; i < col.Length; i++)
            {
                if (i != 0) Write(' ');
                Write(tuple[i]);
            }
            return WriteLine();
        }
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLineJoin(params object[] col) => WriteMany(' ', col);
        /// <summary>
        /// Write joined <paramref name="v1"/> and <paramref name="v2"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2>(T1 v1, T2 v2)
        { Write(v1); Write(' '); return WriteLine(v2); }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/> and <paramref name="v3"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
        { Write(v1); Write(' '); Write(v2); Write(' '); return WriteLine(v3); }
        /// <summary>
        /// Write joined <paramref name="v1"/>, <paramref name="v2"/>, <paramref name="v3"/> and <paramref name="v4"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteLineJoin<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
        { Write(v1); Write(' '); Write(v2); Write(' '); Write(v3); Write(' '); return WriteLine(v4); }

        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLineJoin<T>(params T[] col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLineJoin<T>(Span<T> col) => WriteMany(' ', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write joined <paramref name="col"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLineJoin<T>(ReadOnlySpan<T> col) => WriteMany(' ', col);


        /// <summary>
        /// Write line each item of <paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLines<T>(IEnumerable<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLines<T>(params T[] col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLines<T>(Span<T> col) => WriteMany('\n', (ReadOnlySpan<T>)col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)] public _W WriteLines<T>(ReadOnlySpan<T> col) => WriteMany('\n', col);


        /// <summary>
        /// Write lines separated by space
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteGrid<T>(IEnumerable<IEnumerable<T>> cols)
        {
            foreach (var col in cols)
                WriteLineJoin(col);
            return this;
        }
        /// <summary>
        /// Write line each item of <paramref name="tuples"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [MI(256)]
        public _W WriteGrid<TTuple>(IEnumerable<TTuple> tuples) where TTuple : ITuple
        {
            foreach (var tup in tuples)
                WriteLineJoin(tup);
            return this;
        }

        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [MI(256)]
        private _W WriteMany<T>(char sep, IEnumerable<T> col)
        {
            if (col is T[] array)
                return WriteMany(sep, (ReadOnlySpan<T>)array);

            var en = col.GetEnumerator();
            if (en.MoveNext())
            {
                Write(en.Current);
                while (en.MoveNext())
                {
                    Write(sep);
                    Write(en.Current);
                }
            }
            return WriteLine();
        }
        /// <summary>
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [MI(256)]
        private _W WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            if (col.Length > 0)
            {
                Write(col[0]);
                foreach (var c in col.Slice(1))
                {
                    Write(sep);
                    Write(c);
                }
            }
            return WriteLine();
        }
    }
    /// <summary>
    /// Formatter
    /// </summary>
    public interface IUtf8ConsoleWriterFormatter
    {
        /// <summary>
        /// Write to <paramref name="cw"/>.
        /// </summary>
        void Write(_W cw);
    }
}
#endif
