using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Weixin.Configuration
{
    public class WxPayConfig
    {
        /// <summary>
        /// 微信支付分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户支付密钥
        /// </summary>
        public string MchKey { get; set; }


        /// <summary>
        /// APPSECRET：公众帐号secert(企业号则为企业应用的secert)（仅JSAPI支付的时候需要配置）
        /// </summary>
        public string AppSecret { get; set; }


        /// <summary>
        /// 微信支付结果通知回调接口
        /// 由微信发起的接口访问
        /// 在此接口中 可以做 订单交易状态的改变 商品库存修改 交易信息保存 等等....
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 证书路径
        /// </summary>
        public string SSLCERT_PATH { get; set; }

        /// <summary>
        /// 证书密码
        /// </summary>
        public string SSLCERT_PASSWORD { get; set; }


        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string IP = "8.8.8.8";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 0;


    }

    /// <summary>
    /// 交易类型
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// 公众号支付
        /// </summary>
        JSAPI = 1,

        /// <summary>
        /// 原生扫码支付
        /// </summary>
        NATIVE = 2

    }

    public static class TradeTypeExtensions
    {
        public static string GetDisplayName(this TradeType tradeType)
        {
            switch (tradeType)
            {
                case TradeType.JSAPI:
                    return "JSAPI";
                case TradeType.NATIVE:
                    return "NATIVE";
                default:
                    return "JSAPI";
            }
        }
    }
}
