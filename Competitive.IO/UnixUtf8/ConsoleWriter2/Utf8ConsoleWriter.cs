using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static Utf8Formatter;
    using M = MethodImplAttribute;
    using W = UnixUtf8ConsoleWriter2;
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
    public sealed class UnixUtf8ConsoleWriter2
#else
    /// <summary>
    /// Output Writer
    /// </summary>
    public sealed class UnixUtf8ConsoleWriter2
#endif
    {
        internal static readonly UTF8Encoding Utf8NoBom =
#if NET6_0_OR_GREATER
            new(false);
#else
            new UTF8Encoding(false);
#endif

        /// <summary>
        /// Write <paramref name="v"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256 | 512)]
        public W Write<T>(T v)
        {
            void FormatFloat(double d, Span<byte> bf, out int b)
                => TryFormat(d, bf, out b, new StandardFormat('F', 20));

            void FormatDec(decimal d, Span<byte> bf, out int b)
                => TryFormat(d, bf, out b, new StandardFormat('F', 20));

            Span<byte> b = stackalloc byte[512];
            int bw;
            if (typeof(T) == typeof(int)) TryFormat((int)(object)v, b, out bw);
            else if (typeof(T) == typeof(long)) TryFormat((long)(object)v, b, out bw);
            else if (typeof(T) == typeof(uint)) TryFormat((uint)(object)v, b, out bw);
            else if (typeof(T) == typeof(ulong)) TryFormat((ulong)(object)v, b, out bw);
            else if (typeof(T) == typeof(short)) TryFormat((short)(object)v, b, out bw);
            else if (typeof(T) == typeof(ushort)) TryFormat((ushort)(object)v, b, out bw);
            else if (typeof(T) == typeof(byte)) TryFormat((byte)(object)v, b, out bw);
            else if (typeof(T) == typeof(sbyte)) TryFormat((sbyte)(object)v, b, out bw);
            else if (typeof(T) == typeof(float)) FormatFloat((float)(object)v, b, out bw);
            else if (typeof(T) == typeof(double)) FormatFloat((double)(object)v, b, out bw);
            else if (typeof(T) == typeof(decimal)) FormatDec((decimal)(object)v, b, out bw);
            else if (typeof(T) == typeof(char))
            {
                var c = (char)(object)v;
                if (c < 0x7f)
                {
                    b[0] = (byte)c;
                    bw = 1;
                }
                else
                {
#if NET8_0_OR_GREATER
                    bw = Utf8NoBom.GetBytes([c], b);
#else
                    Span<char> s = stackalloc char[1] { c };
                    bw = Utf8NoBom.GetBytes(s, b);
#endif
                }
            }
            else if (v is char[] charr)
                return Write(charr.AsSpan());
            else if (v is IUnixUtf8ConsoleWriter2Formatter)
            {
                ((IUnixUtf8ConsoleWriter2Formatter)v).Write(this);
                return this;
            }
            else
                return Write(v.ToString().AsSpan());
            Uu8.Write(b[..bw]);
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
            var mlen = Utf8NoBom.GetMaxByteCount(v.Length);
            byte[] bb = null;
            var b = mlen < 512 ? stackalloc byte[mlen] : bb = ArrayPool<byte>.Shared.Rent(mlen);

            var bw = Utf8NoBom.GetBytes(v, b);
            Uu8.Write(b[..bw]);
            if (bb != null)
                ArrayPool<byte>.Shared.Return(bb);
            return this;
        }
        /// <summary>
        /// Write <paramref name="v"/> to the output stream.
        /// </summary>
        /// <returns>this instance.</returns>
        [M(256)]
        public W Write(ReadOnlySpan<byte> v)
        {
            Uu8.Write(v);
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
    }
    /// <summary>
    /// Formatter
    /// </summary>
    public interface IUnixUtf8ConsoleWriter2Formatter
    {
        /// <summary>
        /// Write to <paramref name="cw"/>.
        /// </summary>
        void Write(W cw);
    }
}
