using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static DebuggerBrowsableState;
    using static MethodImplOptions;
    /// <summary>
    /// Input Reader
    /// </summary>
    public class PropertyConsoleReader
    {
        private const int BufSize = 1 << 12;
        private readonly Stream input;
        private readonly Encoding encoding;
        internal readonly byte[] buffer = new byte[BufSize];
        internal int pos;
        internal int len;

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        public PropertyConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        public PropertyConsoleReader(Stream input, Encoding encoding)
        {
            this.input = input; this.encoding = encoding;
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
        [DebuggerBrowsable(Never)]
        public int Int
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(Never)]
        public int Int0
        {
            [MethodImpl(AggressiveInlining)]
#if NETSTANDARD1_3
            get { return Int - 1; }
#else
            get => Int - 1;
#endif
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        /// 
        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(Never)]
        public uint UInt
        {
            [MethodImpl(AggressiveInlining)]
#if NETSTANDARD1_3
            get { return (uint)ULong; }
#else
            get => (uint)ULong;
#endif
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(Never)]
        public uint UInt0
        {
            [MethodImpl(AggressiveInlining)]
#if NETSTANDARD1_3
            get { return UInt - 1; }
#else
            get => UInt - 1;
#endif
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public long Long
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(Never)]
        public long Long0
        {
            [MethodImpl(AggressiveInlining)]
#if NETSTANDARD1_3
            get { return Long - 1; }
#else
            get => Long - 1;
#endif
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public ulong ULong
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(Never)]
        public ulong ULong0
        {
            [MethodImpl(AggressiveInlining)]
#if NETSTANDARD1_3
            get { return ULong - 1; }
#else
            get => ULong - 1;
#endif
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(Never)]
        public string String
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(Never)]
        public string Ascii
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public string Line
        {
            [MethodImpl(AggressiveInlining)]
            get
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
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public char Char
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                byte b;
                do b = Read();
                while (b <= ' ');
                return (char)b;
            }
        }

        /// <summary>
        /// Read a <see cref="double"/> from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public double Double
        {
            [MethodImpl(AggressiveInlining)]
            get { return double.Parse(Ascii); }
        }

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [DebuggerBrowsable(Never)]
        public decimal Decimal
        {
            [MethodImpl(AggressiveInlining)]
            get { return decimal.Parse(Ascii); }
        }

        /// <summary>
        /// implicit call <see cref="Int"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator int(PropertyConsoleReader cr) => cr.Int;

        /// <summary>
        /// implicit call <see cref="UInt"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator uint(PropertyConsoleReader cr) => cr.UInt;

        /// <summary>
        /// implicit call <see cref="Long"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator long(PropertyConsoleReader cr) => cr.Long;

        /// <summary>
        /// implicit call <see cref="ULong"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator ulong(PropertyConsoleReader cr) => cr.ULong;

        /// <summary>
        /// implicit call <see cref="Double"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator double(PropertyConsoleReader cr) => cr.Double;

        /// <summary>
        /// implicit call <see cref="Decimal"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator decimal(PropertyConsoleReader cr) => cr.Decimal;

        /// <summary>
        /// implicit call <see cref="Ascii"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public static implicit operator string(PropertyConsoleReader cr) => cr.Ascii;
    }
}
