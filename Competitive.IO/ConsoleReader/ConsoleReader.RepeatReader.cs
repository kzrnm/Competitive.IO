using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using C = ConsoleReader;
    using M = MethodImplAttribute;
    using P = RepeatReader;
    /// <summary>
    /// Calls <see cref="IO.ConsoleReader"/> several times
    /// </summary>
    public class RepeatReader
    {
        internal readonly C cr;
        /// <summary>
        /// The count of read method invocations.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Count { get; }
        /// <summary>
        /// Initialize <see cref="RepeatReader"/>
        /// </summary>
        [M(256)]
        public RepeatReader(C cr, int count)
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
            var a = new string[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.Line();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        [M(256)]
        public string[] String()
        {
            var a = new string[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.String();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.AsciiChars"/> array
        /// </summary>
        [M(256)]
        public char[][] AsciiChars()
        {
            var a = new char[Count][];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.AsciiChars();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.LineChars"/> array
        /// </summary>
        [M(256)]
        public char[][] LineChars()
        {
            var a = new char[Count][];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.LineChars();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.StringChars"/> array
        /// </summary>
        [M(256)]
        public char[][] StringChars()
        {
            var a = new char[Count][];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.StringChars();
            return a;
        }

        /// <summary>
        /// Read <see cref="ConsoleReader.Int0"/> array
        /// </summary>
        [M(256)]
        public int[] Int0()
        {
            var a = new int[Count];
            for (var i = 0; i < Count; i++)
                a[i] = cr.Int0();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        [M(256)]
        public uint[] UInt0()
        {
            var a = new uint[Count];
            for (var i = 0; i < Count; i++)
                a[i] = cr.UInt0();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        [M(256)]
        public long[] Long0()
        {
            var a = new long[Count];
            for (var i = 0; i < Count; i++)
                a[i] = cr.Long0();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        [M(256)]
        public ulong[] ULong0()
        {
            var a = new ulong[Count];
            for (var i = 0; i < Count; i++)
                a[i] = cr.ULong0();
            return a;
        }

        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator int[](P rr) => rr.Int();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator uint[](P rr) => rr.UInt();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator long[](P rr) => rr.Long();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator ulong[](P rr) => rr.ULong();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator double[](P rr) => rr.Double();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator decimal[](P rr) => rr.Decimal();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator string[](P rr) => rr.Ascii();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [M(256)] public static implicit operator char[][](P rr) => rr.AsciiChars();

        /// <summary>
        /// Get array of <typeparamref name="T"/>.
        /// </summary>
        public T[] Read<T>()
        {
            var a = new T[Count];
            for (int i = 0; i < a.Length; i++)
                a[i] = cr.Read<T>();
            return a;
        }
    }
    /// <summary>
    /// Calls <see cref="IO.ConsoleReader"/> several times
    /// </summary>
    public class RepeatReader<R> : P where R : C
    {
        /// <summary>
        /// The instance of <typeparamref name="R"/>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public R ConsoleReader => (R)cr;
        /// <summary>
        /// Initialize <see cref="RepeatReader{R}"/>
        /// </summary>
        [M(256)]
        public RepeatReader(R cr, int count) : base(cr, count) { }

#if NETSTANDARD2_0
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, T> factory)
        {
            var a = new T[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = factory((R)cr);
            return a;
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, int, T> factory)
        {
            var a = new T[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = factory((R)cr, i);
            return a;
        }
#else
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, T> factory)
        {
            var a = new T[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = factory((R)cr);
            return a;
        }

        /// <summary>
        /// Read and write into <paramref name="dst"/>.
        /// </summary>
        [M(256)]
        public void Select<T>(Span<T> dst, Func<R, T> factory)
        {
#if NET6_0_OR_GREATER
            foreach (ref var b in dst)
                b = factory((R)cr);
#else
            for (int i = 0; i < dst.Length; i++)
                dst[i] = factory((R)cr);
#endif
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public T[] Select<T>(Func<R, int, T> factory)
        {
            var a = new T[Count];
            Select(a, factory);
            return a;
        }

        /// <summary>
        /// Read and write into <paramref name="dst"/>.
        /// </summary>
        [M(256)]
        public void Select<T>(Span<T> dst, Func<R, int, T> factory)
        {
            for (var i = 0; i < dst.Length; i++)
                dst[i] = factory((R)cr, i);
        }
#endif
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
