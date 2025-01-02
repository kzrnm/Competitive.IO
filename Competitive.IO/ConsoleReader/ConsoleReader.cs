using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;
#if !NETSTANDARD2_0
using System.Buffers.Text;
#endif

namespace Kzrnm.Competitive.IO
{
#if !NETSTANDARD2_0
    using static Utf8Parser;
#endif
    using M = MethodImplAttribute;
    using R = ConsoleReader;

    /// <summary>
    /// Input Reader
    /// </summary>
    public class ConsoleReader
    {
        /// <summary>
        /// The size of buffer.
        /// </summary>
        protected internal const int BufSize = 1 << 12;
        /// <summary>
        /// The source stream.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Stream Input { get; }
        /// <summary>
        /// The encoding of <see cref="Input"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Encoding Encoding { get; }
        internal readonly byte[] buf;
        internal int pos;
        internal int len;

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        [M(256)] public ConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding, BufSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        [M(256)] public ConsoleReader(Stream input, Encoding encoding) : this(input, encoding, BufSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        /// <param name="bufferSize">Input buffer size</param>
        [M(256)]
        public ConsoleReader(Stream input, Encoding encoding, int bufferSize)
        {
            Input = input;
            Encoding = encoding;
            buf = new byte[bufferSize];
        }

#if !NETSTANDARD2_0
        /// <summary>
        /// Read entire numeric string
        /// </summary>
        [M(256)]
        private void FillEntireNumber()
        {
            if ((uint)pos >= (uint)buf.Length)
                FillNextBuffer();
            while (buf[pos] <= ' ')
                if (++pos >= len)
                    FillNextBuffer();
#if NET6_0_OR_GREATER
            if (pos + 21 >= buf.Length && buf[^1] > ' ')
#else
            if (pos + 21 >= buf.Length && buf[buf.Length - 1] > ' ')
#endif
                FillEntireNumberImpl();
        }
        private void FillEntireNumberImpl()
        {
            buf.AsSpan(pos, len - pos).CopyTo(buf);
            len -= pos;
            pos = 0;
            var numberOfBytes = Input.Read(buf, len, buf.Length - len);
            if (numberOfBytes == 0)
                buf[len++] = 10;
            else if (numberOfBytes + len < buf.Length)
#if NET6_0_OR_GREATER
                buf[^1] = 10;
#else
                buf[buf.Length - 1] = 10;
#endif
            len += numberOfBytes;
        }
#endif
        private void FillNextBuffer()
        {
            if ((len = Input.Read(buf, 0, buf.Length)) == 0)
            {
                buf[0] = 10;
                len = 1;
            }
            else if (len < buf.Length)
#if NET6_0_OR_GREATER
                buf[^1] = 10;
#else
                buf[buf.Length - 1] = 10;
#endif
            pos = 0;
        }

        /// <summary>
        /// Move to next positon
        /// </summary>
        [M(256)]
        internal byte ReadByte()
        {
            if (pos >= len)
                FillNextBuffer();
            return buf[pos++];
        }


        /// <summary>
        /// Parse value from stdin
        /// </summary>
        [M(256)]
        public T Read<T>()
        {
            if (typeof(T) == typeof(int)) return (T)(object)Int();
            if (typeof(T) == typeof(uint)) return (T)(object)UInt();
            if (typeof(T) == typeof(long)) return (T)(object)Long();
            if (typeof(T) == typeof(ulong)) return (T)(object)ULong();
            if (typeof(T) == typeof(double)) return (T)(object)Double();
            if (typeof(T) == typeof(decimal)) return (T)(object)Decimal();
            if (typeof(T) == typeof(char)) return (T)(object)Char();
            if (typeof(T) == typeof(string)) return (T)(object)Ascii();
            if (typeof(T) == typeof(char[])) return (T)(object)AsciiChars();
            return Throw<T>();
        }
        static T Throw<T>() => throw new NotSupportedException(typeof(T).Name);

        /// <summary>
        /// Parse <see cref="int"/> from stdin
        /// </summary>
        [M(256)]
        public int Int()
        {
#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out int v, out int bc);
            pos += bc;
            return v;
#else
            int res = 0;
            bool neg = false;
            byte b;
            do
            {
                b = ReadByte();
                if (b == '-')
                    neg = true;
            }
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ '0');
                b = ReadByte();
            } while ('0' <= b);
            return neg ? -res : res;
#endif
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        [M(256)]
        public uint UInt()
        {

#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out uint v, out int bc);
            pos += bc;
            return v;
#else
            uint res = 0;
            byte b;
            do b = ReadByte();
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ (uint)'0');
                b = ReadByte();
            } while ('0' <= b);
            return res;
#endif
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [M(256)]
        public long Long()
        {
#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out long v, out int bc);
            pos += bc;
            return v;
#else
            long res = 0;
            bool neg = false;
            byte b;
            do
            {
                b = ReadByte();
                if (b == '-')
                    neg = true;
            }
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ '0');
                b = ReadByte();
            } while ('0' <= b);
            return neg ? -res : res;
#endif
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [M(256)]
        public ulong ULong()
        {
#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out ulong v, out int bc);
            pos += bc;
            return v;
#else
            ulong res = 0;
            byte b;
            do b = ReadByte();
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ (ulong)'0');
                b = ReadByte();
            } while ('0' <= b);
            return res;
#endif
        }

        /// <summary>
        /// Read a <see cref="double"/> from stdin
        /// </summary>
        [M(256)]
        public double Double()
        {
#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out double v, out int bc);
            pos += bc;
            return v;
#else
            return double.Parse(Ascii());
#endif
        }

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [M(256)]
        public decimal Decimal()
        {
#if !NETSTANDARD2_0
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out decimal v, out int bc);
            pos += bc;
            return v;
#else
            return decimal.Parse(Ascii());
#endif
        }

        interface IBlock
        {
            bool Ok(byte b);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0079")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251")]
        struct AC : IBlock { [M(256)] public bool Ok(byte b) => ' ' < b; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0079")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0251")]
        struct LB : IBlock { [M(256)] public bool Ok(byte b) => b != '\n' && b != '\r'; }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [M(256)]
        (byte[], int) InnerBlock<T>() where T : struct, IBlock
        {
            var sb = new byte[32];
            int c = 0;
            byte b;
            do b = ReadByte();
            while (!new T().Ok(b));
            do
            {
                sb[c++] = b;
                if (c >= sb.Length)
                    Array.Resize(ref sb, sb.Length << 1);
                b = ReadByte();
            } while (new T().Ok(b));
            return (sb, c);
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [M(256)]
        public string String()
        {
            var (sb, c) = InnerBlock<AC>();
            return Encoding.GetString(sb, 0, c);
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [M(256)]
        public string Line()
        {
            var (sb, c) = InnerBlock<LB>();
            return Encoding.GetString(sb, 0, c);
        }

        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin with encoding
        /// </summary>
        [M(256)]
        public char[] StringChars()
        {
            var (sb, c) = InnerBlock<AC>();
            return Encoding.GetChars(sb, 0, c);
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [M(256)]
        public char[] LineChars()
        {
            var (sb, c) = InnerBlock<LB>();
            return Encoding.GetChars(sb, 0, c);
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public string Ascii()
#if NETSTANDARD2_0
            => new string(AsciiChars());
#elif !NET6_0_OR_GREATER
            => new string(AsciiSpan());
#else
            => new(AsciiSpan());
#endif

#if NETSTANDARD2_0
        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public char[] AsciiChars(int defaultBuf = 32)
        {
            var sb = new char[defaultBuf];
            int c = 0;
            byte b;
            do b = ReadByte();
            while (b <= ' ');
            do
            {
                sb[c++] = (char)b;
                if (c >= sb.Length)
                    Array.Resize(ref sb, sb.Length << 1);
                b = ReadByte();
            } while (' ' < b);
            Array.Resize(ref sb, c);
            return sb;
        }
#else
        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public char[] AsciiChars(int defaultBuf = 32) => AsciiSpan(defaultBuf).ToArray();

        /// <summary>
        /// Read <see cref="T:Span&lt;char&gt;"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public Span<char> AsciiSpan(int defaultBuf = 32)
        {
            var sb = new char[defaultBuf];
            int c = 0;
            byte b;
            do b = ReadByte();
            while (b <= ' ');
            do
            {
                sb[c++] = (char)b;
                if (c >= sb.Length)
                    Array.Resize(ref sb, sb.Length << 1);
                b = ReadByte();
            } while (' ' < b);
            return sb.AsSpan(0, c);
        }
#endif

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [M(256)]
        public char Char()
        {
            byte b;
            do b = ReadByte();
            while (b <= ' ');
            return (char)b;
        }


        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [M(256)] public int Int0() => Int() - 1;
        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [M(256)] public uint UInt0() => UInt() - 1;

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [M(256)] public long Long0() => Long() - 1;
        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [M(256)] public ulong ULong0() => ULong() - 1;


        /// <summary>
        /// implicit call <see cref="Int()"/>
        /// </summary>
        [M(256)] public static implicit operator int(R cr) => cr.Int();

        /// <summary>
        /// implicit call <see cref="UInt()"/>
        /// </summary>
        [M(256)] public static implicit operator uint(R cr) => cr.UInt();

        /// <summary>
        /// implicit call <see cref="Long()"/>
        /// </summary>
        [M(256)] public static implicit operator long(R cr) => cr.Long();

        /// <summary>
        /// implicit call <see cref="ULong()"/>
        /// </summary>
        [M(256)] public static implicit operator ulong(R cr) => cr.ULong();

        /// <summary>
        /// implicit call <see cref="Double()"/>
        /// </summary>
        [M(256)] public static implicit operator double(R cr) => cr.Double();

        /// <summary>
        /// implicit call <see cref="Decimal"/>
        /// </summary>
        [M(256)] public static implicit operator decimal(R cr) => cr.Decimal();

        /// <summary>
        /// implicit call <see cref="Ascii()"/>
        /// </summary>
        [M(256)] public static implicit operator string(R cr) => cr.Ascii();

        /// <summary>
        /// implicit call <see cref="AsciiChars(int)"/>
        /// </summary>
        [M(256)] public static implicit operator char[](R cr) => cr.AsciiChars();

        /// <summary>
        /// Get array of <typeparamref name="T"/>.
        /// </summary>
        [M(256)]
        public T[] Repeat<T>(int count)
        {
            var a = new T[count];
            for (int i = 0; i < a.Length; i++)
                a[i] = Read<T>();
            return a;
        }

#if !NETSTANDARD2_0
        /// <summary>
        /// Read and write into <paramref name="dst"/>.
        /// </summary>
        [M(256)]
        public void Repeat<T>(Span<T> dst)
        {
#if NET6_0_OR_GREATER
            foreach (ref var b in dst)
                b = Read<T>();
#else
            for (int i = 0; i < dst.Length; i++)
                dst[i] = Read<T>();
#endif
        }
#endif
    }
}
