using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.Enum;
using T2M.CoastLine.Utils.Model.Attributes;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;

namespace XZMY.Manage.Model.DataModel.User
{
    [DBTable("Sys_User")]
    public class UserAccount : EntityBase, IDataModel
    {
        /// <summary>
        /// 分店 Id
        /// </summary>
        public Guid BranchDataId { get; set; }

        public int Code { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Zipcode { get; set; }
        public string Location { get; set; }
        public DateTime RegisteredTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginCount { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EGender Gender { get; set; }
        /// <summary>
        /// 性别名称
        /// </summary>
        public string GenderName
        {
            get { return Gender.ToString(); }
        }
        public int Source { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EState State { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get { return State.ToString(); } }

        /*
         * PK	Column Name	Type	Not Null
0	Code	int	Checked
0	RealName	nvarchar(50)	Checked
0	Gender	int	Unchecked
0	Mobile	nvarchar(20)	Unchecked
0	Email	nvarchar(50)	Unchecked
0	QQ	nvarchar(20)	Unchecked
0	Postalcode	nvarchar(10)	Unchecked
0	Location	nvarchar(50)	Unchecked


            0	LoginName nvarchar(50)    Checked
            0	RealName nvarchar(50)    Checked
            0	RegisteredTime datetime    Checked
            0	LastLoginTime datetime    Checked
            0	LoginCount int Checked
            0	Source int Checked
            0	State int Checked
        */
    }
}
