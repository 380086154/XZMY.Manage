using System;
using T2M.Common.Utils.ADONET.SQLServer;

namespace T2M.Common.Utils.Models
{
    public interface ISynchronizableDataModel : IDataModel
    {
        DateTime SyncTime { get; set; }
    }
}