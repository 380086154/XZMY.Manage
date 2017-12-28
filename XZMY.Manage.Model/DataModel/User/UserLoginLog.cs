using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2M.Common.Utils.ADONET.SQLServer;
using T2M.Common.Utils.Models;
using T2M.CoastLine.Utils.Model.Attributes;

namespace XZMY.Manage.Model.DataModel.User
{
    [DBTable("Sys_UserLoginLog")]
    public class UserLoginLog : EntityBase, IDataModel
    {
        public Guid UserId { get; set; }

        public string LoginName { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginTime { get; set; }

        /*
         PK	Column Name	Type	Not Null
        0	UserId	uniqueidentifier	Checked
        0	LoginName	nvarchar(50)	Checked
        0	LoginIP	nvarchar(15)	Checked
        0	LoginTime	datetime	Checked

         */
    }
}
