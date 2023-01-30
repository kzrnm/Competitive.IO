using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
#if NETSTANDARD2_1_OR_GREATER
using System.Buffers.Text;
#endif

namespace Kzrnm.Competitive.IO
{
#if NETSTANDARD2_1_OR_GREATER
    using static Utf8Parser;
#endif
    using M = MethodImplAttribute;
    using R = ConsoleReader;

    /// <summary>
    /// Input Reader
    /// </summary>
    public class ConsoleReader
    {
        internal const int BufSize = 1 << 12;
        internal readonly Stream input;
        internal readonly Encoding encoding;
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
            this.input = input;
            this.encoding = encoding;
            buf = new byte[bufferSize];
        }

#if NETSTANDARD2_1_OR_GREATER
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
            if (pos + 21 >= buf.Length && buf[buf.Length - 1] > ' ')
                FillEntireNumberImpl();
        }
        private void FillEntireNumberImpl()
        {
            buf.AsSpan(pos, len - pos).CopyTo(buf);
            len -= pos;
            pos = 0;
            var numberOfBytes = input.Read(buf, len, buf.Length - len);
            if (numberOfBytes == 0)
                buf[len++] = 10;
            else if (numberOfBytes + len < buf.Length)
                buf[buf.Length - 1] = 10;
            len += numberOfBytes;
        }
#endif
        private void FillNextBuffer()
        {
            if ((len = input.Read(buf, 0, buf.Length)) == 0)
            {
                buf[0] = 10;
                len = 1;
            }
            else if (len < buf.Length)
                buf[buf.Length - 1] = 10;
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
#if NETSTANDARD2_1_OR_GREATER
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

#if NETSTANDARD2_1_OR_GREATER
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
#if NETSTANDARD2_1_OR_GREATER
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
#if NETSTANDARD2_1_OR_GREATER
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
#if NETSTANDARD2_1_OR_GREATER
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
#if NETSTANDARD2_1_OR_GREATER
            FillEntireNumber();
            TryParse(buf.AsSpan(pos), out decimal v, out int bc);
            pos += bc;
            return v;
#else
            return decimal.Parse(Ascii());
#endif
        }

        private interface IBlock
        {
            bool Ok(byte b);
        }
        private struct AC : IBlock { [M(256)] public bool Ok(byte b) => ' ' < b; }
        private struct LB : IBlock { [M(256)] public bool Ok(byte b) => b != '\n' && b != '\r'; }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [M(256)]
        private (byte[], int) InnerBlock<T>() where T : struct, IBlock
        {
            var bk = new T();
            var sb = new byte[32];
            int c = 0;
            byte b;
            do b = ReadByte();
            while (b <= ' ');
            do
            {
                sb[c++] = b;
                if (c >= sb.Length)
                    Array.Resize(ref sb, sb.Length << 1);
                b = ReadByte();
            } while (bk.Ok(b));
            return (sb, c);
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [M(256)]
        public string String()
        {
            var (sb, c) = InnerBlock<AC>();
            return encoding.GetString(sb, 0, c);
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [M(256)]
        public string Line()
        {
            var (sb, c) = InnerBlock<LB>();
            return encoding.GetString(sb, 0, c);
        }

        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin with encoding
        /// </summary>
        [M(256)]
        public char[] StringChars()
        {
            var (sb, c) = InnerBlock<AC>();
            return encoding.GetChars(sb, 0, c);
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [M(256)]
        public char[] LineChars()
        {
            var (sb, c) = InnerBlock<LB>();
            return encoding.GetChars(sb, 0, c);
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public string Ascii()
            => new string(AsciiChars());

        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin as ascii
        /// </summary>
        [M(256)]
        public char[] AsciiChars()
        {
            var sb = new char[32];
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
        /// implicit call <see cref="AsciiChars()"/>
        /// </summary>
        [M(256)] public static implicit operator char[](R cr) => cr.AsciiChars();

        /// <summary>
        /// Get array of <typeparamref name="T"/>.
        /// </summary>
        public T[] Repeat<T>(int count)
        {
            var arr = new T[count];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = Read<T>();
            return arr;
        }
    }
}
