using XZMY.Manage.Log.Models;

namespace XZMY.Manage.Log
{
    public interface ILogger
    {
        void AddLog(LogEntity log);
        void Commit();
        void Clear();
    }
}