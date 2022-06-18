namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;

    /// <summary>
    /// Calls <see cref="PropertyConsoleReader"/> several times
    /// </summary>
    public class PropertyRepeatReader : RepeatReader<PropertyConsoleReader>
    {
        /// <summary>
        /// Initialize <see cref="PropertyRepeatReader"/>
        /// </summary>
        [MI(256)]
        public PropertyRepeatReader(PropertyConsoleReader cr, int count) : base(cr, count) { }


        /// <summary>
        /// Read <see cref="ConsoleReader.Ascii"/> array
        /// </summary>
        public new string[] Ascii { [MI(256)] get => Ascii(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.Int"/> array
        /// </summary>
        public new int[] Int { [MI(256)] get => Int(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt"/> array
        /// </summary>
        public new uint[] UInt { [MI(256)] get => UInt(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.Long"/> array
        /// </summary>
        public new long[] Long { [MI(256)] get => Long(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong"/> array
        /// </summary>
        public new ulong[] ULong { [MI(256)] get => ULong(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.Double"/> array
        /// </summary>
        public new double[] Double { [MI(256)] get => Double(); }

        /// <summary>
        /// Read <see cref="ConsoleReader.Decimal"/> array
        /// </summary>
        public new decimal[] Decimal { [MI(256)] get => Decimal(); }

        /// <summary>
        /// Read <see cref="ConsoleReader.Line"/> array
        /// </summary>
        public new string[] Line { [MI(256)] get => Line(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.String"/> array
        /// </summary>
        public new string[] String { [MI(256)] get => String(); }

        /// <summary>
        /// Read <see cref="ConsoleReader.Int0"/> array
        /// </summary>
        public new int[] Int0 { [MI(256)] get => Int0(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.UInt0"/> array
        /// </summary>
        public new uint[] UInt0 { [MI(256)] get => UInt0(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.Long0"/> array
        /// </summary>
        public new long[] Long0 { [MI(256)] get => Long0(); }
        /// <summary>
        /// Read <see cref="ConsoleReader.ULong0"/> array
        /// </summary>
        public new ulong[] ULong0 { [MI(256)] get => ULong0(); }
    }

    /// <summary>
    /// Defines extension
    /// </summary>
    public static class PRepeatEx
    {
        /// <summary>
        /// Get <see cref="RepeatReader{R}"/>
        /// </summary>
        [MI(256)] public static PropertyRepeatReader Repeat(this PropertyConsoleReader cr, int count) => new PropertyRepeatReader(cr, count);
    }
}
