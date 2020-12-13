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
        public ConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="output">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        public ConsoleReader(Stream input, Encoding encoding)
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
        [MethodImpl(AggressiveInlining)]
        public int Int()
        {
            int res = 0;
            bool neg = false;
            while (buffer[pos] < 48)
            {
                neg = buffer[pos] == 45; MoveNext();
            }
            do
            {
                res = checked(res * 10 + (buffer[pos] ^ 48)); MoveNext();
            } while (48 <= buffer[pos]);
            return neg ? -res : res;
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public int Int0() => Int() - 1;

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public long Long()
        {
            long res = 0;
            bool neg = false;
            while (buffer[pos] < 48) { neg = buffer[pos] == 45; MoveNext(); }
            do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
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
            while (buffer[pos] < 48) MoveNext();
            do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
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
            var sb = new List<byte>();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Add(buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
            return encoding.GetString(sb.ToArray());
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string Ascii()
        {
            var sb = new StringBuilder();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Append((char)buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
            return sb.ToString();
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public string Line()
        {
            var sb = new List<byte>();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Add(buffer[pos]); MoveNext(); } while (buffer[pos] != 10 && buffer[pos] != 13);
            return encoding.GetString(sb.ToArray());
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public char Char()
        {
            while (buffer[pos] <= 32) MoveNext();
            char res = (char)buffer[pos];
            MoveNext();
            return res;
        }

        /// <summary>
        /// <see cref="Ascii"/> and <see cref="double.Parse(string)"/>
        /// </summary>
        [MethodImpl(AggressiveInlining)]
        public double Double() => double.Parse(Ascii());
        /// <summary>
        /// Get <see cref="RepeatReader"/>
        /// </summary>
        public RepeatReader Repeat(int count) => new RepeatReader(this, count);
        /// <summary>
        /// Get <see cref="SplitReader"/>
        /// </summary>
        public SplitReader Split() => new SplitReader(this);
        public static implicit operator int(ConsoleReader cr) => cr.Int();
        public static implicit operator long(ConsoleReader cr) => cr.Long();
        public static implicit operator ulong(ConsoleReader cr) => cr.ULong();
        public static implicit operator double(ConsoleReader cr) => cr.Double();
        public static implicit operator string(ConsoleReader cr) => cr.Ascii();
    }

}
