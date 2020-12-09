using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    public class ConsoleReader
    {
        private const int BufSize = 1 << 12;
        private readonly Stream input;
        private readonly Encoding encoding;
        internal readonly byte[] buffer = new byte[BufSize];
        internal int pos = 0;
        internal int len = 0;
        public ConsoleReader(Stream input, Encoding encoding)
        {
            this.input = input; this.encoding = encoding;
        }
        public ConsoleReader() : this(Console.OpenStandardInput(), Console.InputEncoding) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected internal void MoveNext() { if (++pos >= len) { len = input.Read(buffer, 0, buffer.Length); if (len == 0) { buffer[0] = 10; } pos = 0; } }

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
        public int Int0() => Int() - 1;
        public long Long()
        {
            long res = 0;
            bool neg = false;
            while (buffer[pos] < 48) { neg = buffer[pos] == 45; MoveNext(); }
            do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
            return neg ? -res : res;
        }
        public long Long0() => Long() - 1;
        public ulong ULong()
        {
            ulong res = 0;
            while (buffer[pos] < 48) MoveNext();
            do { res = res * 10 + (buffer[pos] ^ 48U); MoveNext(); } while (48 <= buffer[pos]);
            return res;
        }
        public ulong ULong0() => ULong() - 1;
        public string String()
        {
            var sb = new List<byte>();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Add(buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
            return encoding.GetString(sb.ToArray());
        }
        public string Ascii()
        {
            var sb = new StringBuilder();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Append((char)buffer[pos]); MoveNext(); } while (32 < buffer[pos]);
            return sb.ToString();
        }
        public string Line()
        {
            var sb = new List<byte>();
            while (buffer[pos] <= 32) MoveNext();
            do { sb.Add(buffer[pos]); MoveNext(); } while (buffer[pos] != 10 && buffer[pos] != 13);
            return encoding.GetString(sb.ToArray());
        }
        public char Char()
        {
            while (buffer[pos] <= 32) MoveNext();
            char res = (char)buffer[pos];
            MoveNext();
            return res;
        }
        public double Double() => double.Parse(Ascii());

        public RepeatReader Repeat(int count) => new RepeatReader(this, count);
        public SplitReader Split() => new SplitReader(this);
        public static implicit operator int(ConsoleReader cr) => cr.Int();
        public static implicit operator long(ConsoleReader cr) => cr.Long();
        public static implicit operator ulong(ConsoleReader cr) => cr.ULong();
        public static implicit operator double(ConsoleReader cr) => cr.Double();
        public static implicit operator string(ConsoleReader cr) => cr.Ascii();
    }

}
