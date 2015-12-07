using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HttpServer.Library
{
    public static class ExtensionMethods
    {
        //public static string JsonRequest = string.Empty;
        public static bool IsJsonRequest(this string jsonString)
        {
            bool res = false;
            string ajaxReques = string.Copy(jsonString);

            string[] lines = ajaxReques.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // 12 line is line with json string
            try
            {
                string json = lines[13];

                // После первого прохода конвертирует к таокму виду => email=mail%40address.com&password=765479
                // После чего второй проход уже ошибка выбивает
                var obj = JObject.Parse(json);

                // 9
                string s1 = string.Copy(ajaxReques);
                string s2 = string.Concat(ajaxReques, "\r\nTEST STRING", "\r\nTEST-TEST-TEST-TEST");
            }
            catch (IndexOutOfRangeException)
            {
                res = false;
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                // exception in parsing json => the jsonString is not json!
                return false;
            }
            return res;
        }

        public static bool IsJson(this string json)
        {
            //string json = string.Empty;
            //string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //try
            //{
            //    json = lines[13];
            //}
            //catch(IndexOutOfRangeException)
            //{
            //    return false;
            //}
            json = json.Trim();
            return (json.StartsWith("{") && json.EndsWith("}")) || (json.StartsWith("[") && json.EndsWith("]"));
        }

        #region Unuseful

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
        #endregion
    }
}
