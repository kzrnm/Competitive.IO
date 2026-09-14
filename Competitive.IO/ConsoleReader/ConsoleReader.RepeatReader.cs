using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#if NET7_0_OR_GREATER
using System.Numerics;
#endif

namespace Kzrnm.Competitive.IO
{
    using C = ConsoleReader;
    using P = RepeatReader;

#if NET8_0_OR_GREATER
    /// <summary>
    /// Calls <see cref="IO.ConsoleReader"/> several times
    /// </summary>
    public class RepeatReader(C cr, int count)
    {
        internal readonly C cr = cr;
        /// <summary>
        /// The count of read method invocations.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Count => count;
#else
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
        [MethodImpl(256)]
        public RepeatReader(C cr, int count)
        {
            this.cr = cr;
            Count = count;
        }
#endif

        /// <summary>
        /// Read <see cref="ConsoleReader.Ascii"/> array
        /// </summary>
        [MethodImpl(256)]
        public Asciis[] Ascii() => Read<Asciis>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> array
        /// </summary>
        [MethodImpl(256)]
        public int[] Int() => Read<int>();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> array
        /// </summary>
        [MethodImpl(256)]
        public uint[] UInt() => Read<uint>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> array
        /// </summary>
        [MethodImpl(256)]
        public long[] Long() => Read<long>();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> array
        /// </summary>
        [MethodImpl(256)]
        public ulong[] ULong() => Read<ulong>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Double"/> array
        /// </summary>
        [MethodImpl(256)]
        public double[] Double() => Read<double>();

        /// <summary>
        /// Read <see cref="ConsoleReader.Decimal"/> array
        /// </summary>
        [MethodImpl(256)]
        public decimal[] Decimal() => Read<decimal>();

        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        [MethodImpl(256)]
        public string[] String() => Read<string>();
        /// <summary>
        /// Read <see cref="ConsoleReader.Line"/> array
        /// </summary>
        [MethodImpl(256)]
        public string[] Line()
        {
            var a = new string[Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = cr.Line();
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.LineChars"/> array
        /// </summary>
        [MethodImpl(256)]
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
        [MethodImpl(256)]
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
        [MethodImpl(256)]
        public int[] Int0() => this - 1;
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        [MethodImpl(256)]
        public uint[] UInt0() => this - 1u;
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        [MethodImpl(256)]
        public long[] Long0() => this - 1L;
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        [MethodImpl(256)]
        public ulong[] ULong0() => this - 1UL;

#if NET7_0_OR_GREATER
        T[] AddInteger<T>(T v) where T : IAdditionOperators<T, T, T>
        {
            var a = new T[Count];
            for (int i = 0; i < a.Length; i++)
                a[i] = cr.Read<T>() + v;
            return a;
        }
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> and Add <paramref name="v"/>.
        /// </summary>
        public static int[] operator +(P rr, int v) => rr.AddInteger(v);

        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> and Add <paramref name="v"/>.
        /// </summary>
        public static uint[] operator +(P rr, uint v) => rr.AddInteger(v);

        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> and Add <paramref name="v"/>.
        /// </summary>
        public static long[] operator +(P rr, long v) => rr.AddInteger(v);

        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> and Add <paramref name="v"/>.
        /// </summary>
        public static ulong[] operator +(P rr, ulong v) => rr.AddInteger(v);
#else
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> and Add <paramref name="v"/>.
        /// </summary>
        public static int[] operator +(P rr, int v)
        {
            var a = new int[rr.Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = rr.cr + v;
            return a;
        }

        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> and Add <paramref name="v"/>.
        /// </summary>
        public static uint[] operator +(P rr, uint v)
        {
            var a = new uint[rr.Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = rr.cr + v;
            return a;
        }

        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> and Add <paramref name="v"/>.
        /// </summary>
        public static long[] operator +(P rr, long v)
        {
            var a = new long[rr.Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = rr.cr + v;
            return a;
        }

        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> and Add <paramref name="v"/>.
        /// </summary>
        public static ulong[] operator +(P rr, ulong v)
        {
            var a = new ulong[rr.Count];
            for (var i = 0; i < a.Length; i++)
                a[i] = rr.cr + v;
            return a;
        }
#endif
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> and Subtract <paramref name="v"/>.
        /// </summary>
        [MethodImpl(256)] public static int[] operator -(P rr, int v) => rr + (-v);

        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> and Subtract <paramref name="v"/>.
        /// </summary>
        [MethodImpl(256)] public static uint[] operator -(P rr, uint v) => rr + (0 - v);

        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> and Subtract <paramref name="v"/>.
        /// </summary>
        [MethodImpl(256)] public static long[] operator -(P rr, long v) => rr + (-v);

        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> and Subtract <paramref name="v"/>.
        /// </summary>
        [MethodImpl(256)] public static ulong[] operator -(P rr, ulong v) => rr + (0 - v);

        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator int[](P rr) => rr.Int();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator uint[](P rr) => rr.UInt();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator long[](P rr) => rr.Long();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator ulong[](P rr) => rr.ULong();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator double[](P rr) => rr.Double();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator decimal[](P rr) => rr.Decimal();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator Asciis[](P rr) => rr.Ascii();
        /// <summary>
        /// implicit call <see cref="Read"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator char[][](P rr) => rr.StringChars();

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

#if NET8_0_OR_GREATER
    /// <summary>
    /// Calls <see cref="IO.ConsoleReader"/> several times
    /// </summary>
    public class RepeatReader<R>(R r, int count) : P(r, count) where R : C
    {
        /// <summary>
        /// The instance of <typeparamref name="R"/>
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public R ConsoleReader => (R)cr;
#else
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
        [MethodImpl(256)]
        public RepeatReader(R cr, int count) : base(cr, count) { }
#endif

#if NETSTANDARD2_0
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
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
        [MethodImpl(256)]
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
        [MethodImpl(256)]
        public T[] Select<T>(Func<R, T> factory)
        {
            var a = new T[Count];
            Select(a, factory);
            return a;
        }

        /// <summary>
        /// Read and write into <paramref name="dst"/>.
        /// </summary>
        [MethodImpl(256)]
        public void Select<T>(Span<T> dst, Func<R, T> factory)
        {
            dst = dst.Slice(0, Count);
            for (int i = 0; i < dst.Length; i++)
                dst[i] = factory((R)cr);
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public T[] Select<T>(Func<R, int, T> factory)
        {
            var a = new T[Count];
            Select(a, factory);
            return a;
        }

        /// <summary>
        /// Read and write into <paramref name="dst"/>.
        /// </summary>
        [MethodImpl(256)]
        public void Select<T>(Span<T> dst, Func<R, int, T> factory)
        {
            dst = dst.Slice(0, Count);
            for (int i = 0; i < dst.Length; i++)
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
        [MethodImpl(256)]
        public static RepeatReader<C> Repeat(this C cr, int count)
#if NET6_0_OR_GREATER
            => new(cr, count);
#else
            => new RepeatReader<C>(cr, count);
#endif
    }
}
