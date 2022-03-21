using System;

namespace Kzrnm.Competitive.IO
{
    using MI = System.Runtime.CompilerServices.MethodImplAttribute;
    /// <summary>
    /// <see cref="PropertyRepeatReader"/>
    /// </summary>
    public static class PropertyRepeatReaderSelect
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MI(256)]
        public static T[] Select<T>(this PropertyRepeatReader r, Func<PropertyConsoleReader, T> factory)
        {
            var c = r.count;
            var arr = new T[c];
            for (var i = 0; i < c; i++)
                arr[i] = factory(r.cr);
            return arr;
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        [MI(256)]
        public static T[] Select<T>(this PropertyRepeatReader r, Func<PropertyConsoleReader, int, T> factory)
        {
            var c = r.count;
            var arr = new T[c];
            for (var i = 0; i < c; i++)
                arr[i] = factory(r.cr, i);
            return arr;
        }
    }
}
