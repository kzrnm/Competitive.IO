using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static Utf8Formatter;
    using M = MethodImplAttribute;
    using W = Utf8ConsoleWriter;
    /// <summary>
    /// Output Writer
    /// </summary>
    public sealed class Utf8ConsoleWriter : IDisposable
    {
        internal static readonly UTF8Encoding Utf8NoBom =
#if NET6_0_OR_GREATER
            new(false);
#else
            new UTF8Encoding(false);
#endif
        internal const int BufSize = 1 << 12;
        /// <summary>
        /// The desination stream.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Stream Output { get; }
        internal byte[] buf;
        internal int len;

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
            Output = output;
            buf = new byte[bufferSize];
        }

        /// <summary>
        /// Create <see cref="StreamWriter"/> instance.
        /// </summary>
        public StreamWriter ToStreamWriter()
        {
            Flush();
#if NET6_0_OR_GREATER
            return new(Output, Utf8NoBom);
#else
            return new StreamWriter(Output, Utf8NoBom);
#endif
        }

        /// <summary>
        /// Flush output stream.
        /// </summary>
        [M(256)]
        public void Flush() { Output.Write(buf, 0, len); len = 0; }

        /// <summary>
        /// Calls <see cref="Flush()"/>
        /// </summary>
        void IDisposable.Dispose() => Flush();

        /// <summary>
        /// Ensure buffer span
        /// </summary>
        [M(256)]
        private Span<byte> EnsureBuf(int size)
        {
            if (buf.Length - len < size)
            {
                Flush();
            }
            return buf.AsSpan(len, size);
        }

        /// <summary>
        /// Write <paramref name="v"/> to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256 | 512)]
        public W Write<T>(T v)
        {
            void FormatFloat(double d, out int b)
                => TryFormat(d, EnsureBuf(Math.Max(25 + (int)Math.Log10(Math.Abs(d)), 32)), out b, new StandardFormat('F', 20));

            void FormatDec(decimal d, out int b)
                => TryFormat(d, EnsureBuf(Math.Max(25 + (int)Math.Log10((double)Math.Abs(d)), 32)), out b, new StandardFormat('F', 20));

            int bw;
            if (typeof(T) == typeof(int)) TryFormat((int)(object)v, EnsureBuf(21), out bw);
            else if (typeof(T) == typeof(long)) TryFormat((long)(object)v, EnsureBuf(21), out bw);
            else if (typeof(T) == typeof(uint)) TryFormat((uint)(object)v, EnsureBuf(21), out bw);
            else if (typeof(T) == typeof(ulong)) TryFormat((ulong)(object)v, EnsureBuf(21), out bw);
            else if (typeof(T) == typeof(short)) TryFormat((short)(object)v, EnsureBuf(9), out bw);
            else if (typeof(T) == typeof(ushort)) TryFormat((ushort)(object)v, EnsureBuf(9), out bw);
            else if (typeof(T) == typeof(byte)) TryFormat((byte)(object)v, EnsureBuf(9), out bw);
            else if (typeof(T) == typeof(sbyte)) TryFormat((sbyte)(object)v, EnsureBuf(9), out bw);
            else if (typeof(T) == typeof(float)) FormatFloat((float)(object)v, out bw);
            else if (typeof(T) == typeof(double)) FormatFloat((double)(object)v, out bw);
            else if (typeof(T) == typeof(decimal)) FormatDec((decimal)(object)v, out bw);
            else if (typeof(T) == typeof(char))
            {
                var dst = EnsureBuf(6);
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
            else if (v is IUtf8ConsoleWriterFormatter)
            {
                ((IUtf8ConsoleWriterFormatter)v).Write(this);
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
        [M(256 | 512)]
        public W Write(ReadOnlySpan<char> v)
        {
            var mlen = Utf8NoBom.GetMaxByteCount(v.Length);
            if (mlen > buf.Length)
            {
                Flush();
                buf = new byte[mlen * 2];
            }
            else if (mlen > buf.Length - len)
            {
                Flush();
            }
            var bw = Utf8NoBom.GetBytes(v, buf.AsSpan(len));
            len += bw;
            return this;
        }
        /// <summary>
        /// Write empty line to output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine() => Write('\n');
        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine<T>(T v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write <paramref name="v"/> to output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine(ReadOnlySpan<char> v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write line each item of <paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLines<T>(IEnumerable<T> col) => WriteMany('\n', col);
        /// <summary>
        /// Write line each item of<paramref name="col"/>
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)] public W WriteLines<T>(T[] col) => WriteMany('\n', (ReadOnlySpan<T>)col);
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
        /// Write items separated by <paramref name="sep"/>
        /// </summary>
        /// <param name="sep">sparating charactor</param>
        /// <param name="col">output items</param>
        /// <returns></returns>
        [M(256)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public W WriteMany<T>(char sep, IEnumerable<T> col)
        {
            if (col is T[] a)
                return WriteMany(sep, (ReadOnlySpan<T>)a);

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
        [M(256)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public W WriteMany<T>(char sep, ReadOnlySpan<T> col)
        {
            if (col.Length > 0)
            {
                Write(col[0]);
#if NET6_0_OR_GREATER
                foreach (var c in col[1..])
#else
                foreach (var c in col.Slice(1))
#endif
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
        void Write(W cw);
    }
}
