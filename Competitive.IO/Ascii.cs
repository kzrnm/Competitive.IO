using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;
using System.Collections;


#if !NETSTANDARD2_0
using System.Runtime.InteropServices;
#endif

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// ascii string.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
#pragma warning disable IDE0079
#pragma warning disable CA2231
    public

#if NET8_0_OR_GREATER
        class Asciis(byte[] d) : IEquatable<Asciis>, IEnumerable<Ascii>
#pragma warning restore CA2231
#pragma warning restore IDE0079
    {
        /// <summary>
        /// entity
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public readonly byte[] d = d;
#else
        class Asciis : IEquatable<Asciis>, IEnumerable<Ascii>
#pragma warning restore CA2231
#pragma warning restore IDE0079
    {
        /// <summary>
        /// Creates <see cref="Asciis"/>.
        /// </summary>
        /// <param name="b"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Asciis(byte[] b) { d = b; }
        /// <summary>
        /// entity
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public readonly byte[] d;
#endif

        /// <summary>
        /// Get a number of charactors.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int Length => d.Length;
        /// <summary>
        /// Returns charactor.
        /// </summary>
        public Ascii this[int i]
        {
            get => d[i];
            set => d[i] = value;
        }

        /// <inheritdoc />
        public override string ToString() => Encoding.ASCII.GetString(d);
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is Asciis a && Equals(a);
#if NETSTANDARD2_0
        /// <inheritdoc />
        public bool Equals(Asciis other)
        {
            if (other == null) return false;
            var e = other.d;
            if (e.Length != d.Length) return false;
            for (int i = d.Length - 1; i >= 0; i--)
                if (d[i] != e[i])
                    return false;
            return true;
        }
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            int v = 0;
            for (int i = d.Length - 1; i >= 0; i--)
            {
                v |= (v << 7) | d[i];
                v ^= d[i];
            }
            return v;
        }
#else
        /// <inheritdoc />
        public bool Equals(Asciis other) => other != null && AsSpan().SequenceEqual(other.d);
        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            var h = new HashCode();
#if NETSTANDARD2_1
            for (int i = d.Length - 1; i >= 0; i--)
            {
                h.Add(d[i]);
            }
#else
            h.AddBytes(d);
#endif
            return h.ToHashCode();
        }

        /// <summary>
        /// Creates a new span.
        /// </summary>
        public ReadOnlySpan<byte> AsSpan() => d.AsSpan();

#pragma warning disable IDE0051
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        [SourceExpander.NotEmbeddingSource]
        private ReadOnlySpan<Ascii> DebugView => MemoryMarshal.Cast<byte, Ascii>(d);
#pragma warning restore IDE0051

#endif


#if NETCOREAPP3_1_OR_GREATER
        IEnumerator<Ascii> IEnumerable<Ascii>.GetEnumerator() => new Enumerator(d);
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(d);
        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        public Span<Ascii>.Enumerator GetEnumerator() => MemoryMarshal.Cast<byte, Ascii>(d).GetEnumerator();
#else
        /// <inheritdoc />
        public IEnumerator<Ascii> GetEnumerator() => new Enumerator(d);
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(d);
#endif


#if NET8_0_OR_GREATER
        /// <inheritdoc />
        /// <summary>
        /// Creates new enumerator.
        /// </summary>
        class Enumerator(byte[] b) : IEnumerator<Ascii>
        {
            private int ix = -1;
#else
        /// <inheritdoc />
        class Enumerator : IEnumerator<Ascii>
        {
            /// <summary>
            /// Creates new enumerator.
            /// </summary>
            public Enumerator(byte[] d)
            {
                b = d;
                ix = -1;
            }
            private byte[] b;
            private int ix;
#endif
            /// <inheritdoc />
            public Ascii Current => b[ix];
            /// <inheritdoc />
            object IEnumerator.Current => Current;

            /// <inheritdoc />
            public void Dispose() { }
            /// <inheritdoc />
            public bool MoveNext() => ++ix < b.Length;
            /// <inheritdoc />
            public void Reset() { ix = -1; }
        }
    }


#if NET6_0_OR_GREATER
    /// <summary>
    /// ascii code charactor.
    /// </summary>
    /// <param name="C">code point</param>
    [EditorBrowsable(EditorBrowsableState.Never)]

    [DebuggerDisplay("{(char)C}")]
    public readonly record struct Ascii(
        [property: DebuggerBrowsable(DebuggerBrowsableState.Never)] byte C)
    {
#else
    /// <summary>
    /// ascii code
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerDisplay("{(char)C}")]
    public
#if !NETSTANDARD2_0
        readonly
#endif
        struct Ascii : IEquatable<Ascii>
    {
        /// <summary>
        /// ascii code
        /// </summary>
        /// <param name="code">code point</param>
        public Ascii(byte code) { C = code; }
        /// <summary>
        /// code point
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public byte C { get; }
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is Ascii a && Equals(a);
        /// <inheritdoc/>
        [MethodImpl(256)] public bool Equals(Ascii other) => C == other.C;
#endif


        /// <inheritdoc/>
        public override string ToString() => ((char)C).ToString();
        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => C.GetHashCode();

        /// <summary>
        /// to <see cref="char"/>
        /// </summary>
        /// <param name="a"><see cref="Ascii"/></param>
        [MethodImpl(256)] public static implicit operator char(Ascii a) => (char)a.C;
        /// <summary>
        /// to <see cref="byte"/>
        /// </summary>
        /// <param name="a"><see cref="Ascii"/></param>
        [MethodImpl(256)] public static implicit operator byte(Ascii a) => a.C;
        /// <summary>
        /// to <see cref="int"/>
        /// </summary>
        /// <param name="a"><see cref="Ascii"/></param>
        [MethodImpl(256)] public static implicit operator int(Ascii a) => a.C;
#if NET6_0_OR_GREATER
        /// <summary>
        /// from <see cref="char"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator Ascii(char n) => new((byte)n);
        /// <summary>
        /// from <see cref="byte"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator Ascii(byte n) => new(n);
#else
        /// <summary>
        /// from <see cref="char"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator Ascii(char n) => new Ascii((byte)n);
        /// <summary>
        /// from <see cref="byte"/>
        /// </summary>
        [MethodImpl(256)] public static implicit operator Ascii(byte n) => new Ascii(n);
#endif
    }
}
