using System;
using System.Diagnostics;

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// Input Reader
    /// </summary>
    public sealed class PropertyUnixUtf8ConsoleReader : UnixUtf8ConsoleReader
    {
        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        public PropertyUnixUtf8ConsoleReader() : base() { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="bufferSize">Input buffer size</param>
        public PropertyUnixUtf8ConsoleReader(int bufferSize) : base(bufferSize) { }

        /// <summary>
        /// Parse <see cref="int"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new int Int => Int();

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)] public new int Int0 => Int0();

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        /// 
        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)] public new uint UInt => UInt();

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)] public new uint UInt0 => UInt0();

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new long Long => Long();

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)] public new long Long0 => Long0();

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new ulong ULong => ULong();

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)] public new ulong ULong0 => ULong0();

        /// <summary>
        /// Read a <see cref="double"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new double Double => Double();

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new decimal Decimal => Decimal();

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(0)] public new string String => String();

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new string Line => Line();

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(0)] public new string Ascii => Ascii();


        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(0)] public new char[] StringChars => StringChars();

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new char[] LineChars => LineChars();

        /// <summary>
        /// Read <see cref="T:char[]"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(0)] public new char[] AsciiChars => AsciiChars();

#if !NETSTANDARD2_0
        /// <summary>
        /// Read <see cref="T:Span&lt;char&gt;"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(0)] public new Span<char> AsciiSpan => AsciiSpan();
#endif

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)] public new char Char => Char();
    }
}
