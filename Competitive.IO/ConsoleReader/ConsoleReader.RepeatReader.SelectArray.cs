﻿using System;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// <see cref="RepeatReader{R}"/>
    /// </summary>
    public static class RepeatReaderSelectArray
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[]) SelectArray<R, T1, T2>(this RepeatReader<R> r,
            Func<R, (T1, T2)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i]) = factory((R)cr);
            return (a1, a2);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[]) SelectArray<R, T1, T2>(this RepeatReader<R> r,
            Func<R, int, (T1, T2)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i]) = factory((R)cr, i);
            return (a1, a2);
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[], T3[]) SelectArray<R, T1, T2, T3>(this RepeatReader<R> r,
            Func<R, (T1, T2, T3)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            var a3 = new T3[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i], a3[i]) = factory((R)cr);
            return (a1, a2, a3);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[], T3[]) SelectArray<R, T1, T2, T3>(this RepeatReader<R> r,
            Func<R, int, (T1, T2, T3)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            var a3 = new T3[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i], a3[i]) = factory((R)cr, i);
            return (a1, a2, a3);
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[], T3[], T4[]) SelectArray<R, T1, T2, T3, T4>(this RepeatReader<R> r,
            Func<R, (T1, T2, T3, T4)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            var a3 = new T3[c];
            var a4 = new T4[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i], a3[i], a4[i]) = factory((R)cr);
            return (a1, a2, a3, a4);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MethodImpl(256)]
        public static (T1[], T2[], T3[], T4[]) SelectArray<R, T1, T2, T3, T4>(this RepeatReader<R> r,
            Func<R, int, (T1, T2, T3, T4)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var a1 = new T1[c];
            var a2 = new T2[c];
            var a3 = new T3[c];
            var a4 = new T4[c];
            for (var i = 0; i < c; i++)
                (a1[i], a2[i], a3[i], a4[i]) = factory((R)cr, i);
            return (a1, a2, a3, a4);
        }
    }
}
