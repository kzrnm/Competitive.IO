using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using static MethodImplOptions;
    public class PropertyConsoleReader
    {
        private const int BufSize = 1 << 12;
        private readonly Stream input;
        private readonly Encoding encoding;
        internal readonly byte[] buffer = new byte[BufSize];
        internal int pos = 0;
        internal int len = 0;
        public PropertyConsoleReader(Stream input, Encoding encoding)
        {
            this.input = input; this.encoding = encoding;
        }
        public PropertyConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }
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
        public int Int0 => Int - 1;
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
        public long Long0 => Long - 1;
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
        public ulong ULong0 => ULong - 1;
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
        public double Double => double.Parse(Ascii);

        public PropertyRepeatReader Repeat(int count) => new PropertyRepeatReader(this, count);
        public PropertySplitReader Split => new PropertySplitReader(this);
        public static implicit operator int(PropertyConsoleReader cr) => cr.Int;
        public static implicit operator long(PropertyConsoleReader cr) => cr.Long;
        public static implicit operator ulong(PropertyConsoleReader cr) => cr.ULong;
        public static implicit operator double(PropertyConsoleReader cr) => cr.Double;
        public static implicit operator string(PropertyConsoleReader cr) => cr.Ascii;
    }

}
