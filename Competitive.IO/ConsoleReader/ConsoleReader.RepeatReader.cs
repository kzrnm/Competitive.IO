using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    /// <summary>
    /// Calls <see cref="ConsoleReader"/> several times
    /// </summary>
    public class RepeatReader<R> where R : ConsoleReader
    {
        internal readonly R cr;
        /// <summary>
        /// The instance of <typeparamref name="R"/>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public R ConsoleReader => cr;
        /// <summary>
        /// The count of read method invocations.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Count { get; }
        /// <summary>
        /// Initialize <see cref="RepeatReader{R}"/>
        /// </summary>
        [M(256)]
        public RepeatReader(R cr, int count)
        {
            this.cr = cr;
            Count = count;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.Ascii"/> array
        /// </summary>
        [M(256)]
        public string[] Ascii() => Read<string>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> array
        /// </summary>
        [M(256)]
        public int[] Int() => Read<int>();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> array
        /// </summary>
        [M(256)]
        public uint[] UInt() => Read<uint>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> array
        /// </summary>
        [M(256)]
        public long[] Long() => Read<long>();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> array
        /// </summary>
        [M(256)]
        public ulong[] ULong() => Read<ulong>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Double"/> array
        /// </summary>
        [M(256)]
        public double[] Double() => Read<double>();

        /// <summary>
        /// Read <see cref="ConsoleReader.Decimal"/> array
        /// </summary>
        [M(256)]
        public decimal[] Decimal() => Read<decimal>();

        /// <summary>
        /// Read <see cref="ConsoleReader.Line"/> array
        /// </summary>
        [M(256)]
        public string[] Line()
        {
            var arr = new string[Count];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = cr.Line();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        [M(256)]
        public string[] String()
        {
            var arr = new string[Count];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = cr.String();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.AsciiChars"/> array
        /// </summary>
        [M(256)]
        public char[][] AsciiChars()
        {
            var arr = new char[Count][];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = cr.AsciiChars();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.LineChars"/> array
        /// </summary>
        [M(256)]
        public char[][] LineChars()
        {
            var arr = new char[Count][];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = cr.LineChars();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.StringChars"/> array
        /// </summary>
        [M(256)]
        public char[][] StringChars()
        {
            var arr = new char[Count][];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = cr.StringChars();
            return arr;
        }

        /// <summary>
        /// Read <see cref="ConsoleReader.Int0"/> array
        /// </summary>
        [M(256)]
        public int[] Int0()
        {
            var arr = new int[Count];
            for (var i = 0; i < Count; i++)
                arr[i] = cr.Int0();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        [M(256)]
        public uint[] UInt0()
        {
            var arr = new uint[Count];
            for (var i = 0; i < Count; i++)
                arr[i] = cr.UInt0();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        [M(256)]
        public long[] Long0()
        {
            var arr = new long[Count];
            for (var i = 0; i < Count; i++)
                arr[i] = cr.Long0();
            return arr;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        [M(256)]
        public ulong[] ULong0()
        {
            var arr = new ulong[Count];
            for (var i = 0; i < Count; i++)
                arr[i] = cr.ULong0();
            return arr;
        }

        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator int[](RepeatReader<R> rr) => rr.Int();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator uint[](RepeatReader<R> rr) => rr.UInt();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator long[](RepeatReader<R> rr) => rr.Long();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator ulong[](RepeatReader<R> rr) => rr.ULong();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator double[](RepeatReader<R> rr) => rr.Double();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator decimal[](RepeatReader<R> rr) => rr.Decimal();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator string[](RepeatReader<R> rr) => rr.Ascii();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator char[][](RepeatReader<R> rr) => rr.AsciiChars();

        /// <summary>
        /// Get array of <typeparamref name="T"/>.
        /// </summary>
        public T[] Read<T>()
        {
            var arr = new T[Count];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = cr.Read<T>();
            return arr;
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, T> factory)
        {
            var arr = new T[Count];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = factory(cr);
            return arr;
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, int, T> factory)
        {
            var arr = new T[Count];
            for (var i = 0; i < arr.Length; i++)
                arr[i] = factory(cr, i);
            return arr;
        }
    }
    /// <summary>
    /// Defines extension
    /// </summary>
    public static class RepeatEx
    {
        /// <summary>
        /// Get <see cref="RepeatReader{R}"/>
        /// </summary>
        [M(256)]
        public static RepeatReader<ConsoleReader> Repeat(this ConsoleReader cr, int count)
#if NET6_0_OR_GREATER
            => new(cr, count);
#else
            => new RepeatReader<ConsoleReader>(cr, count);
#endif
    }
}
