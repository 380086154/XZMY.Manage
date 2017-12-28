﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ConvertUtility
    {
        #region ToDecimal

        /// <summary>
        /// 将 String 转换为 Decimal
        /// <para>注：如转换失败则返回null</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this String str)
        {
            if (String.IsNullOrEmpty(str)) return null;
            Decimal obj;
            if (Decimal.TryParse(str.Trim(), out obj))
                return obj;
            return null;
        }

        /// <summary>
        /// 将 String 转换为 Decimal，可设默认返回值
        /// <para>注：如转换失败则返回用户传入值 defaultValue</para> 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue">为null时的默认返回值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(this String str, Decimal defaultValue)
        {
            var res = str.ToDecimal();
            return res == null ? defaultValue : res.Value;
        }

        #endregion
    }
}
