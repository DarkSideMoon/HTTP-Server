using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public static class ExtensionMethods
    {
        public static string RightOf(this string src, string s)
        {
            string ret = string.Empty;
            int idx = src.IndexOf(s);

            if (idx != -1)
                ret = src.Substring(idx + s.Length);

            return ret;
        }

        public static string LeftOf(this string src, string s)
        {
            string ret = src;
            int idx = src.IndexOf(s);

            if (idx != -1)
                ret = src.Substring(0, idx);

            return ret;
        }

        public static bool If<T>(this T v, Func<T, bool> predicate, Action<T> action)
        {
            bool ret = predicate(v);

            if (ret)
            {
                action(v);
            }

            return ret;
        }

        /// <summary>
        /// Returns true if the object is null.
        /// </summary>
        public static bool IfNull<T>(this T obj)
        {
            return obj == null;
        }

        /// <summary>
        /// If the object is null, performs the action and returns true.
        /// </summary>
        public static bool IfNull<T>(this T obj, Action action)
        {
            bool ret = obj == null;

            if (ret)
                action();

            return ret;
        }

        /// <summary>
        /// Implements a ForEach for generic enumerators.
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>
        /// Implements ForEach for non-generic enumerators.
        /// </summary>
        // Usage: Controls.ForEach<Control>(t=>t.DoSomething());
        public static void ForEach<T>(this IEnumerable collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}
