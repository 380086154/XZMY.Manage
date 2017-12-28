/******************************************
* CopyRight:   重庆礼仪之邦电子商务有限公司
* FileName:    StringUtility.cs
* Author:      Liuxp
* CreateDate:  2012-10-30 13:43
* Description: 的字符串进行处理
* History:     暂无
******************************************/
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// 字符串进行处理
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// 对String字符串进行编码，防止信息中包含破坏JSON格式的字符。同时使 HTML、JavaScript、Style 等等以“&lt;”开始的标签失效
        /// <para>实现方法与 JavaScript 中的 escape 相似</para> 
        /// <para>注：前台HTML页面中无需解码即可正常显示</para> 
        /// </summary>
        /// <returns>String</returns>
        public static String Escape(this String str)
        {
            //本来不想把这逻辑在这里处理，但是遇到了，为空直接原路返回。
            if (String.IsNullOrEmpty(str)) return str;

            var sb = new StringBuilder();

            var ba = Encoding.Unicode.GetBytes(str.Replace("<", "&lt;"));
            for (var i = 0; i < ba.Length; i += 2)
            {
                sb.Append("\\u");
                sb.Append(ba[i + 1].ToString("X2"));

                sb.Append(ba[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 截取两个指定字符串之间的内容，若没有查找到符合条件的内容则返回String.Empty
        /// </summary>
        /// <param name="str"></param>
        /// <param name="starttagstr">前置字符串</param>
        /// <param name="endtagstr">后缀字符串</param>
        /// <returns></returns>
        public static String SubString(this String str, String starttagstr, String endtagstr)
        {
            try
            {
                return str.Substring(str.IndexOf(starttagstr) + starttagstr.Length + 1,
                    str.IndexOf(endtagstr) - (str.IndexOf(starttagstr) + starttagstr.Length + 1)).TrimLineCharacter().Replace("=\r\n", "");
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// 截取指定长度的字符串，常用于在列表中显示标题
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static String GetThumbnail(this String source, Int32 length)
        {
            if (source.Length <= length) return source;
            return source.Substring(0, length) + "...";
        }

        /// <summary>
        /// 去除字符串中的换行符
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String TrimLineCharacter(this String source)
        {
            String str = source.Trim();
            while (str.StartsWith("\r") || str.StartsWith("\n") || str.EndsWith("\r") || str.EndsWith("\n"))
            {
                str = str.Trim();
                str = str.Trim('\r').Trim('\n');
            }
            return str;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="str">分割标识</param>
        /// <returns></returns>
        public static String[] Split(this String source, String str)
        {
            var list = new List<String>();
            while (true)
            {
                var index = source.IndexOf(str);
                if (index < 0) { list.Add(source); break; }
                var rs = source.Substring(0, index);
                //if (!String.IsNullOrEmpty(rs))
                list.Add(rs);
                source = source.Substring(index + str.Length);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 把字符串分割为指定长度的子字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sublength">子字符串长度</param>
        /// <returns></returns>
        public static String[] Split(this String source, Int32 sublength)
        {
            if (sublength <= 0) throw new ArgumentException("Length must greatter than zero.");

            Int32 i = 0, len = 0;
            var size = source.Length / sublength;
            if (source.Length % sublength != 0) size += 1;
            var result = new String[size];
            while (len < source.Length)
            {
                var length = sublength + len > source.Length ? source.Length - len : sublength;
                result[i] = source.Substring(len, length);
                len += result[i].Length;
                i++;
            }
            return result;
        }

        /// <summary>
        /// 获取汉字的拼音首字母
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static String GetPyChar(this String s)
        {
            return String.Join("", s.Select(c =>
            {
                var cc = c.ToString(CultureInfo.InvariantCulture);
                if (!cc.IsCHZN()) return cc;
                var array = System.Text.Encoding.Default.GetBytes(cc);
                var i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
                if (i < 0xB0A1) return "*";
                if (i < 0xB0C5) return "a";
                if (i < 0xB2C1) return "b";
                if (i < 0xB4EE) return "c";
                if (i < 0xB6EA) return "d";
                if (i < 0xB7A2) return "e";
                if (i < 0xB8C1) return "f";
                if (i < 0xB9FE) return "g";
                if (i < 0xBBF7) return "h";
                if (i < 0xBFA6) return "g";
                if (i < 0xC0AC) return "k";
                if (i < 0xC2E8) return "l";
                if (i < 0xC4C3) return "m";
                if (i < 0xC5B6) return "n";
                if (i < 0xC5BE) return "o";
                if (i < 0xC6DA) return "p";
                if (i < 0xC8BB) return "q";
                if (i < 0xC8F6) return "r";
                if (i < 0xCBFA) return "s";
                if (i < 0xCDDA) return "t";
                if (i < 0xCEF4) return "w";
                if (i < 0xD1B9) return "x";
                if (i < 0xD4D1) return "y";
                if (i < 0xD7FA) return "z";
                return "*";
            }).ToArray());

        }

        #region RandomString

        public enum CharacterCollection
        {
            Number = 1,
            UpperLetter = 2,
            LowerLetter = 4,
            SpecialCharacter = 8
        }

        private static Dictionary<CharacterCollection, String> CharacterCollectionDic;

        private const String Numbers = "0123456789";
        private const String LowerLetters = "abcdefghijklmnopqrstuvwxyz";
        private const String UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const String SpecialCharacters = "~!@#$%^&*()_+{}[];'\\:\"|,./<>?";

        /// <summary>
        /// 获取以元字符集为随机集合生成的随机字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String GetRandomString(this IEnumerable<Char> source, Int32 length)
        {
            return GetRandomResult(length, source.ToList());
        }



        /// <summary>
        /// 获取随机生成的字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="characters">使用的字符</param>
        /// <returns></returns>
        public static String GetRandomString(int length, params CharacterCollection[] characters)
        {
            if (CharacterCollectionDic == null)
            {
                CharacterCollectionDic = new Dictionary<CharacterCollection, String>();
                CharacterCollectionDic.Add(CharacterCollection.Number, Numbers);
                CharacterCollectionDic.Add(CharacterCollection.UpperLetter, UpperLetters);
                CharacterCollectionDic.Add(CharacterCollection.LowerLetter, LowerLetters);
                CharacterCollectionDic.Add(CharacterCollection.SpecialCharacter, SpecialCharacters);
            }
            characters = characters.Distinct().ToArray();
            String sourcestr = String.Empty;
            foreach (var item in characters)
            {
                sourcestr += CharacterCollectionDic[item];
            }

            return GetRandomResult(length, sourcestr.ToCharArray());
        }

        private static String GetRandomResult(int length, IList<Char> sourcestr)
        {
            String outstr = String.Empty;
            Random ran = new Random(DateTime.Now.Millisecond + length - sourcestr.GetHashCode());
            while (outstr.Length < length)
            {
                outstr += sourcestr[ran.Next(0, sourcestr.Count())];
            }
            return outstr;
        }


        #endregion

        private static Dictionary<Char, Char> filpMap;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String Flip(this String source)
        {
            if (filpMap == null)
            {
                filpMap = new Dictionary<Char, Char>();
                var sourcestr = LowerLetters.Reverse().ToArray();
                var flipstr = "zʎxʍʌnʇsɹbdouɯlʞɾıɥƃɟǝpɔqɐ";
                for (int i = 0; i < sourcestr.Length; i++)
                {
                    filpMap.Add(sourcestr[i], flipstr[i]);
                }
            }
            var chary = source.ToCharArray();
            for (int i = 0; i < chary.Length; i++)
            {
                if (chary[i] >= 'a' && chary[i] <= 'z')
                    chary[i] = filpMap[chary[i]];
            }

            return new String(chary);
        }

        /// <summary>
        /// 将字符串用MD5加密
        /// <para>实现方式为：System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string password, string passwordFormat)</para>
        /// </summary>
        /// <param name="str">需要转换为MD5的字符串</param>
        /// <returns>String</returns>
        public static String ToMd5(this String str)
        {
            if (String.IsNullOrWhiteSpace(str)) return null;

            return Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");//MD5加密
        }


        private static string KeyString = "FDIUNBDCA";
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 把字符串用默认的密钥加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String EncryptBySimpleDES(this String str)
        {
            return str.EncryptBySimpleDES(KeyString);
        }

        /// <summary>
        /// 把字符串用指定的密钥加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String EncryptBySimpleDES(this String str, String Key)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
            byte[] bIV = IV;
            byte[] bStr = Encoding.UTF8.GetBytes(str);
            try
            {
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 把字符串用默认的密钥解密
        /// </summary>
        /// <param name="DecryptStr"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String DecryptBySimpleDES(this String str)
        {
            return str.DecryptBySimpleDES(true);
        }

        /// <summary>
        /// 把字符串用默认的密钥解密
        /// </summary>
        /// <param name="DecryptStr"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String DecryptBySimpleDES(this String str, Boolean replace)
        {
            if (replace)
                return str.Replace(' ', '+').DecryptBySimpleDES(KeyString);
            return str.DecryptBySimpleDES(KeyString);
        }

        /// <summary>
        /// 把字符串用指定的密钥解密
        /// </summary>
        /// <param name="DecryptStr"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static String DecryptBySimpleDES(this String DecryptStr, String Key)
        {
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(Key.Substring(0, 8));
                byte[] bIV = IV;
                byte[] bStr = Convert.FromBase64String(DecryptStr);
                DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, desc.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);
                cStream.Write(bStr, 0, bStr.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
