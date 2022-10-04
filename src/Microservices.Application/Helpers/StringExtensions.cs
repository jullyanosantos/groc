using System.Globalization;

namespace Microservices.Application.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool ContainsInsensitive(this string source, string search)
        {
            search = search ?? "";
            var aa = (new CultureInfo("pt-BR").CompareInfo).IndexOf(source, search, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0;

            return aa;
        }

        public static bool Is<T>(this string s)
        {
            if (typeof(T) == typeof(int))
            {
                return int.TryParse(s, out int tmp);
            }

            if (typeof(T) == typeof(long))
            {
                return long.TryParse(s, out long tmp);
            }

            return false;
        }

        /// <summary>
        /// Uppercases the first letter of a string and
        /// lowercases the remaining letters.
        /// </summary>
        /// <param name="text">A string.</param>
        /// <returns>The capitalized string.</returns>
        /// <remarks>Sentences are not taken into account.</remarks>
        /// <example>
        /// 	<code>
        /// string text = MainUtil.Capitalize("HELLO WORLD."); // "Hello world"
        /// string text = MainUtil.Capitalize("HELLO. HOW ARE YOU?"); // "Hello. how are you?"
        /// </code>
        /// </example>
        public static string Capitalize(string text)
        {
            string str = text.Trim();
            if (str.Length == 0)
                return string.Empty;
            return str.Substring(0, 1).ToUpperInvariant() + str.Substring(1).ToLowerInvariant();
        }

        /// <summary>Clips a string at a certain length.</summary>
        /// <param name="text">The text that will be clipped.</param>
        /// <param name="length">The maximum length of the returned string.</param>
        /// <param name="ellipsis">Indicates if the string should have en ellipsis (...) appended</param>
        /// <returns>The clipped string.</returns>
        /// <example>
        /// 	<code>
        /// string s0 = StringUtil.Clip("Hello world", 5, false); // "Hello"
        /// string s1 = StringUtil.Clip("Hello world", 5, true);  // "He..."
        /// </code>
        /// </example>
        public static string Clip(string text, int length, bool ellipsis)
        {
            text = text.Replace("&nbsp;", " ").Replace("  ", " ");
            if (text.Length > length)
            {
                if (ellipsis)
                    length -= 3;
                int length1 = text.LastIndexOf(" ", length, StringComparison.InvariantCulture);
                if (length1 < 0)
                    length1 = length;
                text = text.Substring(0, length1);
                if (ellipsis)
                    text += "...";
            }
            return text;
        }

        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        // <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }


        /// <summary>Removes a postfix from a string.</summary>
        /// <param name="postfix">The postfix.</param>
        /// <param name="value">The string value.</param>
        /// <returns></returns>
        public static string RemovePostfix(char postfix, string value)
        {
            if (value.Length == 0)
                return string.Empty;
            if ((int)value[value.Length - 1] != (int)postfix)
                return value;
            return value.Substring(0, value.Length - 1);
        }


        /// <summary>Removes a specific prefix from a string.</summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="value">The value.</param>
        public static string RemovePreFix(string prefix, string value)
        {
            if (prefix.Length == 0 || !value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return value;
            return value.Substring(prefix.Length);
        }
    }
}