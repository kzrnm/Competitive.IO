using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using D = DebuggerBrowsableAttribute;
    /// <summary>
    /// Calls <see cref="PropertyConsoleReader"/> several times
    /// </summary>
    public class PropertyRepeatReader : RepeatReader<PropertyConsoleReader>
    {
        /// <summary>
        /// Initialize <see cref="PropertyRepeatReader"/>
        /// </summary>
        public PropertyRepeatReader(PropertyConsoleReader cr, int count) : base(cr, count) { }


        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> array
        /// </summary>
        [D(0)] public new int[] Int => Int();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> array
        /// </summary>
        [D(0)] public new uint[] UInt => UInt();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> array
        /// </summary>
        [D(0)] public new long[] Long => Long();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> array
        /// </summary>
        [D(0)] public new ulong[] ULong => ULong();
        /// <summary>
        /// Read <see cref="ConsoleReader.Double"/> array
        /// </summary>
        [D(0)] public new double[] Double => Double();
        /// <summary>
        /// Read <see cref="ConsoleReader.Decimal"/> array
        /// </summary>
        [D(0)] public new decimal[] Decimal => Decimal();

        /// <summary>
        /// Read <see cref="ConsoleReader.Ascii"/> array
        /// </summary>
        [D(0)] public new string[] Ascii => Ascii();
        /// <summary>
        /// Read <see cref="ConsoleReader.Line"/> array
        /// </summary>
        [D(0)] public new string[] Line => Line();
        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        [D(0)] public new string[] String => String();
        /// <summary>
        /// Read <see cref="ConsoleReader.AsciiChars"/> array
        /// </summary>
        [D(0)] public new char[][] AsciiChars => AsciiChars();
        /// <summary>
        /// Read <see cref="ConsoleReader.LineChars"/> array
        /// </summary>
        [D(0)] public new char[][] LineChars => LineChars();
        /// <summary>
        /// Read <see cref="ConsoleReader.StringChars"/> array
        /// </summary>
        [D(0)] public new char[][] StringChars => StringChars();

        /// <summary>
        /// Read <see cref="ConsoleReader.Int0"/> array
        /// </summary>
        [D(0)] public new int[] Int0 => Int0();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        [D(0)] public new uint[] UInt0 => UInt0();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        [D(0)] public new long[] Long0 => Long0();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        [D(0)] public new ulong[] ULong0 => ULong0();
    }

    /// <summary>
    /// Defines extension
    /// </summary>
    public static class PRepeatEx
    {
        /// <summary>
        /// Get <see cref="RepeatReader{R}"/>
        /// </summary>
        [MethodImpl(256)]
        public static PropertyRepeatReader Repeat(this PropertyConsoleReader cr, int count)
#if NET6_0_OR_GREATER
            => new(cr, count);
#else
            => new PropertyRepeatReader(cr, count);
#endif
    }
}
