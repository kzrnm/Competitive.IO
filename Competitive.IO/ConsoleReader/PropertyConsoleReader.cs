using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// Input Reader
    /// </summary>
    public sealed class PropertyConsoleReader : ConsoleReader
    {
        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// <para>Input stream: <see cref="Console.OpenStandardInput()"/></para>
        /// <para>Input encoding: <see cref="Console.InputEncoding"/></para>
        /// </summary>
        public PropertyConsoleReader() : base() { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        public PropertyConsoleReader(Stream input, Encoding encoding) : base(input, encoding) { }

        /// <summary>
        /// <para>Wrapper of stdin</para>
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="encoding">Input encoding</param>
        /// <param name="bufferSize">Input buffer size</param>
        public PropertyConsoleReader(Stream input, Encoding encoding, int bufferSize) : base(input, encoding, bufferSize) { }

        /// <summary>
        /// Parse <see cref="int"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new int Int
        {
            [MI(256)]
            get => Int();
        }

        /// <summary>
        /// Parse <see cref="int"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public new int Int0
        {
            [MI(256)]
            get => Int0();
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin
        /// </summary>
        /// 
        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public new uint UInt
        {
            [MI(256)]
            get => UInt();
        }

        /// <summary>
        /// Parse <see cref="uint"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public new uint UInt0
        {
            [MI(256)]
            get => UInt0();
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new long Long
        {
            [MI(256)]
            get => Long();
        }

        /// <summary>
        /// Parse <see cref="long"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public new long Long0
        {
            [MI(256)]
            get => Long0();
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new ulong ULong
        {
            [MI(256)]
            get => ULong();
        }

        /// <summary>
        /// Parse <see cref="ulong"/> from stdin and decrement
        /// </summary>
        [DebuggerBrowsable(0)]
        public new ulong ULong0
        {
            [MI(256)]
            get => ULong0();
        }

        /// <summary>
        /// Read a <see cref="double"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new double Double
        {
            [MI(256)]
            get => Double();
        }

        /// <summary>
        /// Read a <see cref="decimal"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new decimal Decimal
        {
            [MI(256)]
            get => Decimal();
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin with encoding
        /// </summary>
        [DebuggerBrowsable(0)]
        public new string String
        {
            [MI(256)]
            get => String();
        }

        /// <summary>
        /// Read <see cref="string"/> from stdin as ascii
        /// </summary>
        [DebuggerBrowsable(0)]
        public new string Ascii
        {
            [MI(256)]
            get => Ascii();
        }

        /// <summary>
        /// Read line from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new string Line
        {
            [MI(256)]
            get => Line();
        }

        /// <summary>
        /// Read a <see cref="char"/> from stdin
        /// </summary>
        [DebuggerBrowsable(0)]
        public new char Char
        {
            [MI(256)]
            get => Char();
        }
    }
}
