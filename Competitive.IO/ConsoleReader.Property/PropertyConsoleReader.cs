using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
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
        [MI(256)] public PropertyConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        [MI(256)]
        public PropertyConsoleReader(Stream input, Encoding encoding)
        {
            this.input = input; this.encoding = encoding;
        }

        /// <summary>
        /// Move to next positon
        /// </summary>
        [MI(256)]
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
        [DebuggerBrowsable(0)]
        public int Int
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public int Int0
        {
            [MI(256)]
            get => Int - 1;
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        /// 
        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public uint UInt
        {
            [MI(256)]
            get => (uint)ULong;
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public uint UInt0
        {
            [MI(256)]
            get => UInt - 1;
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public long Long
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public long Long0
        {
            [MI(256)]
            get => Long - 1;
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public ulong ULong
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public ulong ULong0
        {
            [MI(256)]
            get => ULong - 1;
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(0)]
        public string String
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public string Ascii
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public string Line
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public char Char
        {
            [MI(256)]
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
        [DebuggerBrowsable(0)]
        public double Double
        {
            [MI(256)]
            get => double.Parse(Ascii);
        }

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public decimal Decimal
        {
            [MI(256)]
            get => decimal.Parse(Ascii);
        }

        /// <summary>
        /// implicit call <see cref="Int"/>
        /// </summary>
        [MI(256)]
        public static implicit operator int(PropertyConsoleReader cr) => cr.Int;

        /// <summary>
        /// implicit call <see cref="UInt"/>
        /// </summary>
        [MI(256)]
        public static implicit operator uint(PropertyConsoleReader cr) => cr.UInt;

        /// <summary>
        /// implicit call <see cref="Long"/>
        /// </summary>
        [MI(256)]
        public static implicit operator long(PropertyConsoleReader cr) => cr.Long;

        /// <summary>
        /// implicit call <see cref="ULong"/>
        /// </summary>
        [MI(256)]
        public static implicit operator ulong(PropertyConsoleReader cr) => cr.ULong;

        /// <summary>
        /// implicit call <see cref="Double"/>
        /// </summary>
        [MI(256)]
        public static implicit operator double(PropertyConsoleReader cr) => cr.Double;

        /// <summary>
        /// implicit call <see cref="Decimal"/>
        /// </summary>
        [MI(256)]
        public static implicit operator decimal(PropertyConsoleReader cr) => cr.Decimal;

        /// <summary>
        /// implicit call <see cref="Ascii"/>
        /// </summary>
        [MI(256)]
        public static implicit operator string(PropertyConsoleReader cr) => cr.Ascii;
    }
}
