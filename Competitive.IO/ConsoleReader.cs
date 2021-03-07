using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static MethodImplOptions;
    /// <summary>
    /// Input Reader
    /// </summary>
    public class ConsoleReader
    {
        private const int DefaultBufferSize = 1 << 12;
        private readonly Stream input;
        private readonly Encoding encoding;
        internal readonly byte[] buffer;
        internal int pos = 0;
        internal int len = 0;


        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        public ConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        public ConsoleReader(Stream input, Encoding encoding) : this(input, encoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        /// <param name="bufferSize">Input buffer size</param>
        public ConsoleReader(Stream input, Encoding encoding, int bufferSize)
        {
            this.input = input;
            this.encoding = encoding;
            buffer = new byte[bufferSize];
        }

        /// <summary>
        /// Move to next positon
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        protected internal byte Read()
        {
            if (++pos >= len)
            {
                if ((len = input.Read(buffer, 0, buffer.Length)) <= 0)
                {
                    buffer[0] = 10;
                }
                pos = 0;
            }
            return buffer[pos];
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public int Int()
        {
            int res = 0;
            bool neg = false;
            byte b;
            do
            {
                b = Read();
                if (b == '-')
                    neg = true;
            }
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ '0');
                b = Read();
            } while ('0' <= b);
            return neg ? -res : res;
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public int Int0() => Int() - 1;

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public uint UInt() => (uint)ULong();

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public uint UInt0() => UInt() - 1;

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public long Long()
        {
            long res = 0;
            bool neg = false;
            byte b;
            do
            {
                b = Read();
                if (b == '-')
                    neg = true;
            }
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ '0');
                b = Read();
            } while ('0' <= b);
            return neg ? -res : res;
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public long Long0() => Long() - 1;

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public ulong ULong()
        {
            ulong res = 0;
            byte b;
            do b = Read();
            while (b < '0');
            do
            {
                res = res * 10 + (b ^ (ulong)'0');
                b = Read();
            } while ('0' <= b);
            return res;
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public ulong ULong0() => ULong() - 1;

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string String()
        {
            var sb = new List<byte>(); ;
            byte b;
            do b = Read();
            while (b <= ' ');
            do
            {
                sb.Add(b);
                b = Read();
            } while (' ' < b);
            return encoding.GetString(sb.ToArray());
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string Ascii()
        {
            var sb = new StringBuilder();
            byte b;
            do b = Read();
            while (b <= ' ');
            do
            {
                sb.Append((char)b);
                b = Read();
            } while (' ' < b);
            return sb.ToString();
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string Line()
        {
            var sb = new List<byte>();
            byte b;
            do b = Read();
            while (b <= ' ');

            do
            {
                sb.Add(b);
                b = Read();
            } while (b != '\n' && b != '\r');
            return encoding.GetString(sb.ToArray());
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public char Char()
        {
            byte b;
            do b = Read();
            while (b <= ' ');
            return (char)b;
        }

        /// <summary>
        /// Read a <see cref="double"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public double Double() => double.Parse(Ascii());

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public decimal Decimal() => decimal.Parse(Ascii());

        /// <summary>
        /// implicit call <see cref="Int()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator int(ConsoleReader cr) => cr.Int();

        /// <summary>
        /// implicit call <see cref="UInt()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator uint(ConsoleReader cr) => cr.UInt();

        /// <summary>
        /// implicit call <see cref="Long()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator long(ConsoleReader cr) => cr.Long();

        /// <summary>
        /// implicit call <see cref="ULong()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator ulong(ConsoleReader cr) => cr.ULong();

        /// <summary>
        /// implicit call <see cref="Double()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator double(ConsoleReader cr) => cr.Double();

        /// <summary>
        /// implicit call <see cref="Decimal"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator decimal(ConsoleReader cr) => cr.Decimal();

        /// <summary>
        /// implicit call <see cref="Ascii()"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator string(ConsoleReader cr) => cr.Ascii();
    }
}
