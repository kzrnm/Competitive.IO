using System;
using System.Runtime.CompilerServices;

namespace Kzrnm.Competitive.IO
{
    using M = MethodImplAttribute;
    /// <summary>
    /// <see cref="RepeatReader{R}"/>
    /// </summary>
    public static class RepeatReaderSelectArray
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[]) SelectArray<R, T1, T2>(this RepeatReader<R> r,
            Func<R, (T1, T2)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i]) = factory(cr);
            return (arr1, arr2);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[]) SelectArray<R, T1, T2>(this RepeatReader<R> r,
            Func<R, int, (T1, T2)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i]) = factory(cr, i);
            return (arr1, arr2);
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[], T3[]) SelectArray<R, T1, T2, T3>(this RepeatReader<R> r,
            Func<R, (T1, T2, T3)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            var arr3 = new T3[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i], arr3[i]) = factory(cr);
            return (arr1, arr2, arr3);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[], T3[]) SelectArray<R, T1, T2, T3>(this RepeatReader<R> r,
            Func<R, int, (T1, T2, T3)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            var arr3 = new T3[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i], arr3[i]) = factory(cr, i);
            return (arr1, arr2, arr3);
        }

        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[], T3[], T4[]) SelectArray<R, T1, T2, T3, T4>(this RepeatReader<R> r,
            Func<R, (T1, T2, T3, T4)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            var arr3 = new T3[c];
            var arr4 = new T4[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i], arr3[i], arr4[i]) = factory(cr);
            return (arr1, arr2, arr3, arr4);
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [M(256)]
        public static (T1[], T2[], T3[], T4[]) SelectArray<R, T1, T2, T3, T4>(this RepeatReader<R> r,
            Func<R, int, (T1, T2, T3, T4)> factory) where R : ConsoleReader
        {
            var c = r.Count;
            var cr = r.cr;
            var arr1 = new T1[c];
            var arr2 = new T2[c];
            var arr3 = new T3[c];
            var arr4 = new T4[c];
            for (var i = 0; i < c; i++)
                (arr1[i], arr2[i], arr3[i], arr4[i]) = factory(cr, i);
            return (arr1, arr2, arr3, arr4);
        }
    }
}
