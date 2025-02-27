﻿#if NETCOREAPP3_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Kzrnm.Competitive.IO.Writer
{
    public class Utf8ConsoleWriterRepeatTests
    {
        private const int BufSize = 1 << 10;
        private readonly byte[] buffer = new byte[BufSize];
        private readonly MemoryStream stream;
        private readonly Utf8ConsoleWriter cw;
        public Utf8ConsoleWriterRepeatTests()
        {
            stream = new MemoryStream(buffer);
            cw = new Utf8ConsoleWriter(stream, 8);
        }
        private static byte[] ToBytes(string str)
        {
            var rt = new byte[BufSize];
            Encoding.UTF8.GetBytes(str, rt);
            return rt;
        }

        public static IEnumerable<TheoryDataRow<int>> WriteRepeat_Data(int size)
        {
            for (int i = 1; i <= size; i++)
            {
                yield return i;
            }
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatAscii(int len)
        {
            cw.Write('a', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes(new string('a', len) + "$"));
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatAscii2(int len)
        {
            cw.Write("012");
            cw.Write('a', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes("012" + new string('a', len) + "$"));
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatGreek(int len)
        {
            cw.Write('ψ', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes(new string('ψ', len) + "$"));
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatGreek2(int len)
        {
            cw.Write("012");
            cw.Write('ψ', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes("012" + new string('ψ', len) + "$"));
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatHiragana(int len)
        {
            cw.Write('こ', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes(new string('こ', len) + "$"));
        }

        [Theory]
        [MemberData(nameof(WriteRepeat_Data), 100)]
        public void WriteRepeatHiragana2(int len)
        {
            cw.Write("012");
            cw.Write('こ', len);
            cw.Write('$').Flush();
            buffer.ShouldBe(ToBytes("012" + new string('こ', len) + "$"));
        }
    }
}
#endif
