using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XZMY.Manage.Service.Utils
{
    /// <summary>
    /// String 帮助类
    /// </summary>
    public static class StringHtml
    {
        public static List<string> PickupImgUrl(string html)
        {
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            MatchCollection matches = regImg.Matches(html);
            List<string> lstImg = new List<string>();

            foreach (Match match in matches)
            {
                lstImg.Add(match.Groups["imgUrl"].Value);
            }

            return lstImg;
        }

        /// <summary>
        /// HTML中提取图片地址
        /// </summary>
        public static string PickupImgUrlFirst(string html)
        {
            List<string> lstImg = PickupImgUrl(html);

            return lstImg.Count == 0 ? string.Empty : lstImg[0];
        }
        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="Html">包含Html字符串</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(this string Html)
        {
            return new Regex(@"<[^>]+>|</[^>]+>").Replace(Html, string.Empty);
        }

        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="Html">包含Html字符串</param>
        /// <returns></returns>
        public static string ReplaceHtml(string Html)
        {
            return new Regex(@"<[^>]+>|</[^>]+>").Replace(Html, string.Empty);
        }
        /// <summary>
        /// 删除HTML代码
        /// </summary>
        /// <param name="htmlstring"></param>
        /// <returns></returns>
        public static string KillHtml(string htmlstring)//删除HTML
        {
            //删除脚本
            htmlstring = Regex.Replace(htmlstring, @"<script[^>]+?>[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlstring = Regex.Replace(htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlstring = Regex.Replace(htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            htmlstring = Regex.Replace(htmlstring, @"//\(function\(\)[\s\S]+?}\)\(\);", "", RegexOptions.IgnoreCase);
            htmlstring = htmlstring.Replace("<", "");
            htmlstring = htmlstring.Replace(">", "");
            htmlstring = htmlstring.Replace("\r\n", "");
            return htmlstring.Trim();
        }

    }
}