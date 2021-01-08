using System;

namespace Kzrnm.Competitive.IO
{
    /// <summary>
    /// <see cref="PropertyRepeatReader"/>
    /// </summary>
    public static class PropertyRepeatReaderSelect
    {
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        public static T[] Select<T>(this PropertyRepeatReader r, Func<PropertyConsoleReader, T> factory)
        {
            var arr = new T[r.count];
            for (var i = 0; i < r.count; i++)
                arr[i] = factory(r.cr);
            return arr;
        }
        /// <summary>
        /// Repeat <paramref name="factory"/>()
        /// </summary>
        public static T[] Select<T>(this PropertyRepeatReader r, Func<PropertyConsoleReader, int, T> factory)
        {
            var arr = new T[r.count];
            for (var i = 0; i < r.count; i++)
                arr[i] = factory(r.cr, i);
            return arr;
        }
    }
}
