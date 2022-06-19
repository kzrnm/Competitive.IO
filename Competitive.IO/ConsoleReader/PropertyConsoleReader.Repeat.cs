using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Kzrnm.Competitive.IO
{
    using _D = DebuggerBrowsableAttribute;
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
        /// Read <see cref="ConsoleReader.Ascii"/> array
        /// </summary>
        [_D(0)] public new string[] Ascii => Ascii();
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> array
        /// </summary>
        [_D(0)] public new int[] Int => Int();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> array
        /// </summary>
        [_D(0)] public new uint[] UInt => UInt();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> array
        /// </summary>
        [_D(0)] public new long[] Long => Long();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> array
        /// </summary>
        [_D(0)] public new ulong[] ULong => ULong();
        /// <summary>
        /// Read <see cref="ConsoleReader.Double"/> array
        /// </summary>
        [_D(0)] public new double[] Double => Double();

        /// <summary>
        /// Read <see cref="ConsoleReader.Decimal"/> array
        /// </summary>
        [_D(0)] public new decimal[] Decimal => Decimal();

        /// <summary>
        /// Read <see cref="ConsoleReader.Line"/> array
        /// </summary>
        [_D(0)] public new string[] Line => Line();
        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        [_D(0)] public new string[] String => String();

        /// <summary>
        /// Read <see cref="ConsoleReader.Int0"/> array
        /// </summary>
        [_D(0)] public new int[] Int0 => Int0();
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        [_D(0)] public new uint[] UInt0 => UInt0();
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        [_D(0)] public new long[] Long0 => Long0();
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        [_D(0)] public new ulong[] ULong0 => ULong0();
    }

    /// <summary>
    /// Defines extension
    /// </summary>
    public static class PRepeatEx
    {
        /// <summary>
        /// Get <see cref="RepeatReader{R}"/>
        /// </summary>
        [MethodImpl(256)] public static PropertyRepeatReader Repeat(this PropertyConsoleReader cr, int count) => new PropertyRepeatReader(cr, count);
    }
}
