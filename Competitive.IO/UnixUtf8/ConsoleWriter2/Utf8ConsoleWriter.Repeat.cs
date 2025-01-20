using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// Output Writer
    /// </summary>
    public static class UnixUtf8ConsoleWriter2Repeat
    {
        /// <summary>
        /// Write <paramref name="v"/> to the output stream <paramref name="count"/> times.
        /// </summary>
        /// <returns>this instance.</returns>
        [MethodImpl(256)]
        public static UnixUtf8ConsoleWriter2 Write(this UnixUtf8ConsoleWriter2 w, char v, int count)
        {
            Span<byte> d = stackalloc byte[8];
#if NET8_0_OR_GREATER
            int l = Encoding.UTF8.GetBytes([v], d);
#elif NET6_0_OR_GREATER
            int l = Encoding.UTF8.GetBytes(stackalloc char[1] { v }, d);
#else
            Span<char> vs = stackalloc char[1] { v };
            int l = Encoding.UTF8.GetBytes(vs, d);
#endif
            int sc = 256 / l;
            var size = (long)count * l;
            Span<byte> bl = stackalloc byte[sc * l];
            if (l == 1)
                bl.Fill(d[0]);
            else
            {
#if NETSTANDARD2_1
                d.Slice(0, l).CopyTo(bl);
#else
                d[..l].CopyTo(bl);
#endif
                for (int i = l; i < bl.Length; i++)
                    bl[i] = bl[i - l];
            }

            while (count > sc)
            {
                Uu8.Write(bl);
                count -= sc;
            }
            Uu8.Write(bl[..(count * l)]);
            return w;
        }
    }
}
