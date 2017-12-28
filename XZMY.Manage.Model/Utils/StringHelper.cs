using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.Utils
{
    public class StringHelper
    {
        /// <summary>
        /// 历练的面向人群转换为年级的汉字
        /// </summary>
        /// <param name="SuitablePerson"> 面向人群 输入   ,6,7,9,</param>
        /// <returns></returns>
        public String GetSuitablePerson(string SuitablePerson)
        {
            StringBuilder sb = new StringBuilder();
            var list = SuitablePerson.Split(",");
            foreach (var m in list)
            {
                if (!String.IsNullOrEmpty(m))
                {
                    sb.AppendFormat("{0},", GetGrade(m.ToInt32(0)));
                }
            }
            string str = sb.ToString();
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }
        /// <summary>
        /// 根据年级ID 获取年级名称
        /// </summary>
        /// <param name="GradeSort"></param>
        /// <returns></returns>
        public String GetGrade(int GradeSort)
        {
            string str = "";
            switch (GradeSort)
            {
                case 7:
                    str = "小学一年级";
                    break;
                case 8:
                    str = "小学二年级";
                    break;
                case 9:
                    str = "小学三年级";
                    break;
                case 10:
                    str = "小学四年级";
                    break;
                case 11:
                    str = "小学五年级";
                    break;
                case 12:
                    str = "小学六年级";
                    break;
                case 13:
                    str = "初中一年级";
                    break;
                case 14:
                    str = "初中二年级";
                    break;
                case 15:
                    str = "初中三年级";
                    break;
                case 16:
                    str = "高中一年级";
                    break;
                case 17:
                    str = "高中二年级";
                    break;
                case 18:
                    str = "高中三年级";
                    break;
            }
            return str;
        }
    }
}
