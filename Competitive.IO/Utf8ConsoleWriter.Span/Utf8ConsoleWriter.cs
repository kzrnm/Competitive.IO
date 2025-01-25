using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static Utf8Formatter;
    using M = MethodImplAttribute;
    using W = Utf8ConsoleWriter;
#if NET9_0_OR_GREATER
    using O = OverloadResolutionPriorityAttribute;
#endif

#if NET8_0_OR_GREATER
    /// <summary>
    /// Output Writer
    /// </summary>
    /// <remarks>
    /// <para>Wrapper of stdin</para>
    /// </remarks>
    /// <param name="output">Output stream</param>
    /// <param name="bufferSize">Output buffer size</param>
    public sealed class Utf8ConsoleWriter(Stream output, int bufferSize)
#else
    /// <summary>
    /// Output Writer
    /// </summary>
    public sealed class Utf8ConsoleWriter
#endif
         : IDisposable, IBufferWriter<byte>
    {
        internal static readonly UTF8Encoding Utf8NoBom =
#if NET6_0_OR_GREATER
            new(false);
#else
            new UTF8Encoding(false);
#endif
        internal int len;

#if NET8_0_OR_GREATER
        /// <summary>
        /// The desination stream.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Stream Output { get; } = output;
        internal byte[] buf = new byte[bufferSize];
#else
        /// <summary>
        /// The desination stream.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Stream Output { get; }
        internal byte[] buf;

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
#endif

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// <para>Output stream: <see cref="Console.OpenStandardOutput()"/></para>
        /// <para>Output encoding: <see cref="Console.OutputEncoding"/></para>
        /// </summary>R
        public Utf8ConsoleWriter() : this(Console.OpenStandardOutput()) { }

        /// <summary>
        /// <para>Wrapper of stdout</para>
        /// </summary>
        /// <param name="output">Output stream</param>
        public Utf8ConsoleWriter(Stream output) : this(output, 1 << 12) { }

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
        /// Ensure buffer span. <paramref name="size"/> must be less than or equal to buf.Length.
        /// </summary>
        [M(256)]
        internal Span<byte> EnsureBuf(int size)
        {
            Debug.Assert(size <= buf.Length);
            if (buf.Length - len < size)
            {
                Flush();
            }
            return buf.AsSpan(len);
        }

        /// <summary>
        /// Ensure buffer span.
        /// </summary>
        [M(256)]
        internal Span<byte> MustEnsureBuf(int size)
        {
            if (size == 0)
            {
                if (len == buf.Length)
                    Flush();
                return buf.AsSpan(len);
            }
            if (size > buf.Length)
            {
                Flush();
                return buf = new byte[size];
            }
            else
                return EnsureBuf(size);
        }

        /// <summary>
        /// Write <paramref name="v"/> to the output stream.
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
                if (c < 127)
                {
                    dst[0] = (byte)c;
                    bw = 1;
                }
                else
                {
#if NET8_0_OR_GREATER
                    bw = Utf8NoBom.GetBytes([c], dst);
#else
                    Span<char> s = stackalloc char[1] { c };
                    bw = Utf8NoBom.GetBytes(s, dst);
#endif
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
        /// Write <paramref name="v"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256 | 512)]
#if NET9_0_OR_GREATER
        [O(1)]
#endif
        public W Write(ReadOnlySpan<char> v)
        {
            var s = Utf8NoBom.GetMaxByteCount(v.Length);
            var bw = Utf8NoBom.GetBytes(v, MustEnsureBuf(s));
            len += bw;
            return this;
        }
        /// <summary>
        /// Write <paramref name="v"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W Write(ReadOnlySpan<byte> v)
        {
            if (v.Length < (buf.Length << 1))
            {
                v.CopyTo(EnsureBuf(v.Length));
                len += v.Length;
            }
            else
            {
                Flush();
                Output.Write(v);
            }
            return this;
        }
        /// <summary>
        /// Write empty line to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine() => Write('\n');
        /// <summary>
        /// Write <paramref name="v"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine<T>(T v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write <paramref name="v"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
#if NET9_0_OR_GREATER
        [O(1)]
#endif
        public W WriteLine(ReadOnlySpan<char> v) { Write(v); return Write('\n'); }

        /// <summary>
        /// Write <paramref name="v"/> to the output stream with end of line.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W WriteLine(ReadOnlySpan<byte> v) { Write(v); return Write('\n'); }

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
#if NET9_0_OR_GREATER
        [O(1)]
#endif
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
#if NET9_0_OR_GREATER
        [O(1)]
#endif
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

        Memory<byte> IBufferWriter<byte>.GetMemory(int s)
        {
            MustEnsureBuf(s);
            return buf;
        }

        /// <inheritdoc/>
        [M(256)]
        public void Advance(int count) => len += count;
        /// <inheritdoc/>
        [M(256)]
        public Span<byte> GetSpan(int sizeHint) => MustEnsureBuf(sizeHint);
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
