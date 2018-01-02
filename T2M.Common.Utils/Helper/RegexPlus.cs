using System;
using System.Text.RegularExpressions;

namespace T2M.Common.Utils.Helper
{
    /// <summary>
    /// 提供对字符串进行正则表达式验证的常用扩展方法
    /// </summary>
    public static class RegexPlus
    {
        #region Fields

        private const String CHINESE_REGEX_TEXT = "[\u4e00-\u9fa5]";
        private const String EMAIL_REGEX_TEXT = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        private const String URI_REGEX_TEXT = @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$";

        #endregion

        #region Public methods

        /// <summary>
        /// 判断待验证内容是否包含数字。
        /// </summary>
        /// <param name="input">待验证的内容。</param>
        /// <returns>true，待验证内容包含数字。false，不包含。</returns>
        public static Boolean ContainsNumber(String input)
        {
            foreach (var c in input)
            {
                if (Char.IsNumber(c))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 判断待验证内容是否包含中文信息。
        /// </summary>
        /// <param name="input">待验证的内容。</param>
        /// <returns>true，待验证内容包含中文信息。false，不包含。</returns>
        public static Boolean IsChineseCharacter(String input)
        {
            var re = new Regex(CHINESE_REGEX_TEXT);
            return re.IsMatch(input);
        }

        /// <summary>
        /// 判断待验证内容是否包含电子邮件信息。
        /// </summary>
        /// <param name="input">待验证的内容。</param>
        /// <returns>true，待验证内容包含电子邮件信息。false，不包含。</returns>
        public static Boolean IsEmail(String input)
        {
            var re = new Regex(EMAIL_REGEX_TEXT);
            return (re.IsMatch(input));
        }

        /// <summary>
        /// 判断待验证内容是否包含URI信息。
        /// </summary>
        /// <param name="input">待验证的内容。/param>
        /// <returns>true，待验证内容包含URI信息。false，不包含。</returns>
        public static Boolean IsUri(String input)
        {
            var re = new Regex(URI_REGEX_TEXT);
            return (re.IsMatch(input));
        }

        #endregion
    }
}
