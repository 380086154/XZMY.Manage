using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class RegexUtils
    {
        private static readonly Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static readonly Regex RegEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        private static readonly Regex RegHTMLUrl = new Regex(@"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$");
        private static readonly Regex RegMobile = new Regex(@"^[1][3-8][0-9]{9}$");

        /// <summary>
        /// 判断字符串中是否含有数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean ContainsNumber(this String str)
        {
            foreach (var ch in str)
            {
                if (Char.IsNumber(ch))
                    return true;
            }
            return false;
        }

        public static bool IsMobile(this string inputEmail)
        {
            return (RegMobile.IsMatch(inputEmail));
        }
        /// <summary>
        /// 判断是否符合EMail格式
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns></returns>
        public static bool IsEmail(this string inputEmail)
        {
            return (RegEmail.IsMatch(inputEmail));
        }

        /// <summary>
        /// 判断是否符合Html地址格式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsHtmlUrl(this string input)
        {
            return (RegHTMLUrl.IsMatch(input));
        }

        /// <summary>
        /// 判断是否是中文字符串
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsCHZN(this string inputData)
        {
            return RegCHZN.IsMatch(inputData);
        }	
    }
}
