using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using T2M.Common.DataServiceComponents.Data.Impl.Query;
using T2M.Common.DataServiceComponents.Data.Query;
using T2M.Common.DataServiceComponents.Data.Query.Interface;
using T2M.Common.DataServiceComponents.Service;
using XZMY.Manage.Model.DataModel;
using XZMY.Manage.Model.ViewModel.WeixinUserInfo;
using XZMY.Manage.Service.Handlers.WeixinUserInfo;
using XZMY.Manage.Service.Utils;
using XZMY.Manage.Service.Weixin.Tools;

namespace XZMY.Manage.Service.Sys
{
    /// <summary>
    /// 微信用户服务
    /// </summary>
    public class WeixinUserInfoService
    {
        #region Handler

        /// <summary>
        /// 新增或更新微信用户
        /// </summary>
        /// <param name="doc"></param>
        /// <returns>是否首次关注：true 是, false 否</returns>
        public bool SaveOrUpdate(XmlDocument doc)
        {
            var isFirstSubscribe = true;
            var openId = WeixinXml.GetFromXml(doc, "FromUserName");

            if (string.IsNullOrWhiteSpace(openId)) return isFirstSubscribe;

            var entity = GetByOpenId(openId).ConvertTo<VmWeixinUserInfoEdit>();

            if (entity == null)
            {
                entity = new VmWeixinUserInfoEdit();
                entity.OpenId = openId;
            }
            entity.XmlDocument = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);

            if (entity.DataId == Guid.Empty)
            {
                var handler = new CreateHandler(entity);
                var res = handler.Invoke();

                if (res.Code != 0)
                {
                    LogHelper.Log("新用户关注", "创建失败");
                }
                else
                {
                    LogHelper.Log("新用户关注", "执行结果：" + res.Success);
                }
            }
            else
            {
                isFirstSubscribe = false;
                var handler = new ModifyHandler(entity);
                var res = handler.Invoke();

                LogHelper.Log("更新用户信息", "执行结果：" + res.Success);
            }
            LogHelper.Commit();
            return isFirstSubscribe;
        }

        #endregion

        /// <summary>
        /// 根据 OpenId 获取微信用户
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public WeixinUserInfoDto GetByOpenId(string openId)
        {
            var service = new CustomSearchService<WeixinUserInfoDto>
            {
                CustomConditions = new List<CustomCondition<WeixinUserInfoDto>>
                {
                    new CustomConditionPlus<WeixinUserInfoDto>
                    {
                        Value = openId ?? string.Empty,
                        Operation = SqlOperation.Equals,
                        Member = new Expression<Func<WeixinUserInfoDto, object>>[] {
                            x => x.OpenId,
                        }
                    }
                }
            };

            return service.Invoke().FirstOrDefault();
        }

        /// <summary>
        /// 获取所有微信用户
        /// </summary>
        /// <returns></returns>
        public IList<WeixinUserInfoDto> GetAll()
        {
            var service = new CustomSearchService<WeixinUserInfoDto>
            {
                CustomConditions = new List<CustomCondition<WeixinUserInfoDto>>
                {
                    new CustomConditionPlus<WeixinUserInfoDto>
                    {
                        Value = string.Empty,
                        Operation = SqlOperation.Like,
                        Member = new Expression<Func<WeixinUserInfoDto, object>>[] {
                            x => x.RemarkName,
                        }
                    }
                }
            };

            return service.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IList<WeixinUserInfoDto> GetByIdList(IList<Guid> list)
        {
            var service = new GetEntityByIdListService<WeixinUserInfoDto>(list);
            return service.Invoke();
        }
    }
}
