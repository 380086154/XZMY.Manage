using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Service.Auth.Data;
using XZMY.Manage.Service.Auth.Data.SqlServer;
using XZMY.Manage.Service.Auth.Models;
using XZMY.Manage.Service.Auth.Models.DataModels.SqlServer;

namespace XZMY.Manage.Service.Auth
{
    /// <summary>
    /// 角色数据源
    /// </summary>
    public class AuthDataSource
    {
        private IAuthDataInitializer _initializer;
        private IAuthDataLoader _loader;
        private IAuthDataCacheLoader _cacheLoader;
        private IAuthDataCacheWritter _cacheWritter;
        private object _gate;

        private Dictionary<Guid, RoleResource> _roleResource;
        private Dictionary<Guid, UserResource> _userResource;
        private RoleResource _allResource;
        private IList<Sys_Module> _allModules;
        private IList<Sys_Action> _allActions;
        
        public AuthDataSource()
        {
            _gate = new object();
            _initializer = new SqlAuthDataInitalizer();
            _loader = new SqlAuthDataLoader();
            _cacheLoader = new BasicAuthDataCacheLoader();
            _cacheWritter = new BasicAuthDataCacheWritter();
            Initialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            _roleResource = _cacheLoader.LoadRoleResource(_cacheWritter.Gate);
            if (_roleResource == null)
                _roleResource = _initializer.LoadRoleResource();
            _userResource = new Dictionary<Guid, UserResource>();
        }

        /// <summary>
        /// 根据用户Id获取 资源信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserResource GetUserResource(Guid userid)
        {
            if (_userResource.ContainsKey(userid)) return _userResource[userid];
            lock (_gate)
            {
                if (_userResource.ContainsKey(userid)) return _userResource[userid];
                var ur = _cacheLoader.LoadUserResource(userid, _cacheWritter.Gate);
                if (ur != null)
                {
                    _userResource[userid] = ur;
                    return ur.DeepClone();
                }
            }
            if (_roleResource == null)
                lock (_gate)
                    if (_roleResource == null)
                        _roleResource = _initializer.LoadRoleResource();

            lock (_gate)
            {
                if (_userResource.ContainsKey(userid)) return _userResource[userid];
                var ur = _loader.GetUserResource(userid, _roleResource);
                if (ur != null)
                {
                    _userResource[userid] = ur;
                    _cacheWritter.SaveUserResource(ur);
                    return ur.DeepClone();
                }
            }
            return null;
        }

        /// <summary>
        /// 根据角色Id获取 Module 与 Action 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public RoleResource GetRoleResource(Guid roleid)
        {
            if (_roleResource == null)
                lock (_gate)
                    if (_roleResource == null)
                        _roleResource = _initializer.LoadRoleResource();

            if (_roleResource.ContainsKey(roleid)) return _roleResource[roleid].DeepClone();

            lock (_gate)
            {
                _roleResource.Remove(roleid);
                var res = _loader.GetRoleResource(roleid);
                if (res != null)
                    _roleResource[roleid] = res;

                return res.DeepClone();
            }
        }

        /// <summary>
        /// 获取搜索资源
        /// </summary>
        /// <returns></returns>
        public RoleResource GetAllResource()
        {
            if (_allResource == null)
                lock (_gate)
                    if (_allResource == null)
                        _allResource = _loader.GetAllResource();

            return _allResource.DeepClone();
        }
        /// <summary>
        /// 获取所有 Module
        /// </summary>
        /// <returns></returns>
        public IList<Sys_Module> GetAllModules()
        {
            if (_allModules == null)
                lock (_gate)
                    if (_allModules == null)
                        _allModules = _initializer.GetModuleList();

            return _allModules.DeepClone();
        }
        /// <summary>
        /// 获取所有 Action
        /// </summary>
        /// <returns></returns>
        public IList<Sys_Action> GetAllActions()
        {
            if (_allActions == null)
                lock (_gate)
                    if (_allActions == null)
                        _allActions = _initializer.GetActionList();

            return _allActions.DeepClone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        public void ClearUserResourceCache(Guid userid)
        {
            lock (_gate)
            {
                _userResource.Remove(userid);
                _cacheWritter.DeleteUserResource(userid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        public void ClearRoleResourceCache(Guid roleid)
        {
            lock (_gate)
            {
                _cacheWritter.DeleteAll();
            }

            if (_roleResource == null)
                lock (_gate)
                    if (_roleResource == null)
                        _roleResource = _initializer.LoadRoleResource();

            lock (_gate)
            {
                _roleResource.Remove(roleid);
                var res = _loader.GetRoleResource(roleid);
                if (res != null)
                    _roleResource[roleid] = res;

                var keys = _userResource.Where(m => m.Value.Roles.Contains(roleid)).Select(m => m.Key).ToList();
                keys.ForEach(m => { _userResource.Remove(m); _cacheWritter.DeleteUserResource(m); });
                _cacheWritter.SaveRoleResource(_roleResource);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearAllCache()
        {
            lock (_gate)
            {
                _userResource.Clear();
                _roleResource = null;
                _cacheWritter.DeleteAll();
                _allResource = null;
            }
        }
    }
}
