using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Kzrnm.Competitive.IO
{
    internal static partial class Uu8
    {
        public static int Write(ReadOnlySpan<byte> buffer)
        {
            if (_init)
            {
                SystemNative_InitializeTerminalAndSignalHandling();
                _init = false;
            }
            return SystemNative_Write(OUT, buffer, buffer.Length);
        }
        public static int Read(Span<byte> buffer)
            => SystemNative_Read(IN, buffer, buffer.Length);

        static bool _init = true;
        const string libNative = "libSystem.Native";
        static readonly SafeFileHandle IN = new(0, false);
        static readonly SafeFileHandle OUT = new(1, false);
        [LibraryImport(libNative)]
        private static partial int SystemNative_Read(SafeHandle fd, Span<byte> buffer, int count);
        [LibraryImport(libNative)]
        private static partial int SystemNative_Write(SafeHandle fd, ReadOnlySpan<byte> buffer, int count);
        [LibraryImport(libNative)]
        private static partial int SystemNative_InitializeTerminalAndSignalHandling();
    }
}
