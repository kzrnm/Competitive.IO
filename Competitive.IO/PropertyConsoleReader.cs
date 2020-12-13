using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
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
        internal int pos = 0;
        internal int len = 0;

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        public PropertyConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="output">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        public PropertyConsoleReader(Stream input, Encoding encoding)
        {
            this.input = input; this.encoding = encoding;
        }
        [MethodImpl(AggressiveInlining)]
        protected internal void MoveNext()
        {
            if (++pos >= len)
            {
                len = input.Read(buffer, 0, buffer.Length);
                if (len == 0)
                {
                    buffer[0] = 10;
                }
                pos = 0;
            }
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Int
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                int res = 0;
                bool neg = false;
                while (buffer[pos] < 48) { neg = buffer[pos] == 45; MoveNext(); }
                do { res = checked(res * 10 + (buffer[pos] ^ 48)); MoveNext(); } while (48 <= buffer[pos]);
                return neg ? -res : res;
            }
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Int0 => Int - 1;


        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long Long
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                long res = 0;
                bool neg = false;
                while (buffer[pos] < 48) { neg = buffer[pos] == 45; MoveNext(); }
                do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
                return neg ? -res : res;
            }
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public long Long0 => Long - 1;

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ulong ULong
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                ulong res = 0;
                while (buffer[pos] < 48) MoveNext();
                do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
                return res;
            }
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public ulong ULong0 => ULong - 1;

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string String
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var sb = new List<byte>();
                while (buffer[pos] <= 32) MoveNext();
                do { sb.Add(buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
                return encoding.GetString(sb.ToArray());
            }
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Ascii
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var sb = new StringBuilder();
                while (buffer[pos] <= 32) MoveNext();
                do { sb.Append((char)buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Line
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                var sb = new List<byte>();
                while (buffer[pos] <= 32) MoveNext();
                do { sb.Add(buffer[pos]); MoveNext(); } while (buffer[pos] != 10 && buffer[pos] != 13);
                return encoding.GetString(sb.ToArray());
            }
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public char Char
        {
            [MethodImpl(AggressiveInlining)]
            get
            {
                while (buffer[pos] <= 32) MoveNext();
                char res = (char)buffer[pos];
                MoveNext();
                return res;
            }
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public double Double => double.Parse(Ascii);

        /// <summary>
        /// Get <see cref="PropertyRepeatReader"/>
        /// </summary>
        public PropertyRepeatReader Repeat(int count) => new PropertyRepeatReader(this, count);
        /// <summary>
        /// Get <see cref="PropertySplitReader"/>
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public PropertySplitReader Split => new PropertySplitReader(this);
        public static implicit operator int(PropertyConsoleReader cr) => cr.Int;
        public static implicit operator long(PropertyConsoleReader cr) => cr.Long;
        public static implicit operator ulong(PropertyConsoleReader cr) => cr.ULong;
        public static implicit operator double(PropertyConsoleReader cr) => cr.Double;
        public static implicit operator string(PropertyConsoleReader cr) => cr.Ascii;
    }

}
