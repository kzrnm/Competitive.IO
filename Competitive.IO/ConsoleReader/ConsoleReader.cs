using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Input Reader
    /// </summary>
    public class ConsoleReader
    {
        private const int DefaultBufferSize = 1 << 12;
        private readonly Stream input;
        private readonly Encoding encoding;
        internal readonly byte[] buffer;
        internal int pos;
        internal int len;


        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public ConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public ConsoleReader(Stream input, Encoding encoding) : this(input, encoding, DefaultBufferSize) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        /// <param name="bufferSize">Input buffer size</param>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public ConsoleReader(Stream input, Encoding encoding, int bufferSize)
        {
            this.input = input;
            this.encoding = encoding;
            buffer = new byte[bufferSize];
        }

        /// <summary>
        /// Move to next positon
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public int Int0() => Int() - 1;

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public uint UInt() => (uint)ULong();

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public uint UInt0() => UInt() - 1;

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public long Long0() => Long() - 1;

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public ulong ULong0() => ULong() - 1;

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public double Double() => double.Parse(Ascii());

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public decimal Decimal() => decimal.Parse(Ascii());

        /// <summary>
        /// implicit call <see cref="Int()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator int(ConsoleReader cr) => cr.Int();

        /// <summary>
        /// implicit call <see cref="UInt()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator uint(ConsoleReader cr) => cr.UInt();

        /// <summary>
        /// implicit call <see cref="Long()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator long(ConsoleReader cr) => cr.Long();

        /// <summary>
        /// implicit call <see cref="ULong()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator ulong(ConsoleReader cr) => cr.ULong();

        /// <summary>
        /// implicit call <see cref="Double()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator double(ConsoleReader cr) => cr.Double();

        /// <summary>
        /// implicit call <see cref="Decimal"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator decimal(ConsoleReader cr) => cr.Decimal();

        /// <summary>
        /// implicit call <see cref="Ascii()"/>
        /// </summary>
        [MI(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)] public static implicit operator string(ConsoleReader cr) => cr.Ascii();
    }
}
