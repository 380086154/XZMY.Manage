using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.WindowsService.Service
{
    /// <summary>
    /// 会员信息 服务
    /// </summary>
    public class HyxxService
    {
        public DatabaseHelper db = null;

        public HyxxService(DatabaseHelper databaseHelper)
        {
            db = databaseHelper;
        }


    }
}
