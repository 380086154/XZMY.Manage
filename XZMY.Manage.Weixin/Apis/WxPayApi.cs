using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Weixin.Configuration;

namespace XZMY.Manage.Weixin.Apis
{
    public abstract class WxPayApi
    {
        public static WxAppConfig PayConfig { get; set; }

        private readonly WxPayConfig _config;

        /// <summary>
        /// 微信支付接口配置
        /// </summary>
        public WxPayConfig PayApiConfig => _config ?? PayConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        protected WxPayApi(WxPayConfig config)
        {
            if (config == null && PayConfig == null)
                throw new Exception(
                    "构造函数中的config 和 PayConfig 配置信息同时为空，请通过构造函数赋值，或者在程序入口处给 PayConfig 赋值！");
            _config = config;
        }

        /// <summary>
        /// 根据当前系统时间加随机序列来生成商户订单号
        /// @return 订单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", PayConfig.MchId, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }

        /**
        * 
        * 统一下单
        * @param WxPaydata inputObj 提交给统一下单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回，其他抛异常
        */

        public static WxPayData UnifiedOrder(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new KnownException("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new KnownException("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new KnownException("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new KnownException("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new KnownException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new KnownException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                inputObj.SetValue("notify_url", PayConfig.NotifyUrl); //异步通知url
            }

            inputObj.SetValue("appid", PayConfig.AppId); //公众账号ID
            inputObj.SetValue("mch_id", PayConfig.MchId); //商户号	    
            inputObj.SetValue("nonce_str", GenerateNonceStr()); //随机字符串

            //签名
            inputObj.SetValue("sign", inputObj.MakeSign());
            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            LogFactory.GetLogger().Debug("\r\nWxPayApi=>UnfiedOrder request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);
            LogFactory.GetLogger().Debug("\r\nWxPayApi=>UnfiedOrder response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);

            WxPayData result = new WxPayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result); //测速上报
            return result;
        }


        /**
       *  
       * 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数，
       * 微信浏览器调起JSAPI时的输入参数格式如下：
       * {
       *   "appId" : "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
       *   "timeStamp":" 1395712654",         //时间戳，自1970年以来的秒数     
       *   "nonceStr" : "e61463f8efa94090b1f366cccfbbb444", //随机串     
       *   "package" : "prepay_id=u802345jgfjsdfgsdg888",     
       *   "signType" : "MD5",         //微信签名方式:    
       *   "paySign" : "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
       * }
       * @return string 微信浏览器调起JSAPI时的输入参数，json格式可以直接做参数用
       * 更详细的说明请参考网页端调起支付API：http://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=7_7
       * 
       */
        public static string GetJsApiParameters(WxPayData unifiedOrderResult)
        {
            LogFactory.GetLogger().Debug("JsApiPay::GetJsApiParam is processing...");

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            LogFactory.GetLogger().Debug("Get jsApiParam : " + parameters);
            return parameters;
        }


        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回订单查询结果，其他抛异常
        */
        public static WxPayData OrderQuery(WxPayData inputObj, int timeOut = 6)
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new KnownException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", PayConfig.AppId);//公众账号ID
            inputObj.SetValue("mch_id", PayConfig.MchId);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名

            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            LogFactory.GetLogger().Debug("WxPayApi=>OrderQuery request : " + xml);
            string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            LogFactory.GetLogger().Debug("WxPayApi=>OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)((end - start).TotalMilliseconds);//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData();
            result.FromXml(response);

            ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */

        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /**
        * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
         * @return 时间戳
        */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
	    * 
	    * 测速上报
	    * @param string interface_url 接口URL
	    * @param int timeCost 接口耗时
	    * @param WxPayData inputObj参数数组
	    */
        private static void ReportCostTime(string interface_url, int timeCost, WxPayData inputObj)
        {
            //如果不需要进行上报
            if (WxPayConfig.REPORT_LEVENL == 0)
                return;


            //如果仅失败上报
            if (WxPayConfig.REPORT_LEVENL == 1 && inputObj.IsSet("return_code") && inputObj.GetValue("return_code").ToString() == "SUCCESS" &&
             inputObj.IsSet("result_code") && inputObj.GetValue("result_code").ToString() == "SUCCESS")
            {
                return;
            }

            //上报逻辑
            WxPayData data = new WxPayData();
            data.SetValue("interface_url", interface_url);
            data.SetValue("execute_time_", timeCost);
            //返回状态码
            if (inputObj.IsSet("return_code"))
            {
                data.SetValue("return_code", inputObj.GetValue("return_code"));
            }
            //返回信息
            if (inputObj.IsSet("return_msg"))
            {
                data.SetValue("return_msg", inputObj.GetValue("return_msg"));
            }
            //业务结果
            if (inputObj.IsSet("result_code"))
            {
                data.SetValue("result_code", inputObj.GetValue("result_code"));
            }
            //错误代码
            if (inputObj.IsSet("err_code"))
            {
                data.SetValue("err_code", inputObj.GetValue("err_code"));
            }
            //错误代码描述
            if (inputObj.IsSet("err_code_des"))
            {
                data.SetValue("err_code_des", inputObj.GetValue("err_code_des"));
            }
            //商户订单号
            if (inputObj.IsSet("out_trade_no"))
            {
                data.SetValue("out_trade_no", inputObj.GetValue("out_trade_no"));
            }
            //设备号
            if (inputObj.IsSet("device_info"))
            {
                data.SetValue("device_info", inputObj.GetValue("device_info"));
            }

            try
            {
                Report(data);
            }
            catch (Exception ex)
            {
                //不做任何处理
            }
        }

        /**
	    * 
	    * 测速上报接口实现
	    * @param WxPayData inputObj 提交给测速上报接口的参数
	    * @param int timeOut 测速上报接口超时时间
	    * @throws WxPayException
	    * @return 成功时返回测速上报接口返回的结果，其他抛异常
	    */
        public static WxPayData Report(WxPayData inputObj, int timeOut = 1)
        {
            string url = "https://api.mch.weixin.qq.com/payitil/report";
            //检测必填参数
            if (!inputObj.IsSet("interface_url"))
            {
                throw new KnownException("接口URL，缺少必填参数interface_url！");
            }
            if (!inputObj.IsSet("return_code"))
            {
                throw new KnownException("返回状态码，缺少必填参数return_code！");
            }
            if (!inputObj.IsSet("result_code"))
            {
                throw new KnownException("业务结果，缺少必填参数result_code！");
            }
            if (!inputObj.IsSet("user_ip"))
            {
                throw new KnownException("访问接口IP，缺少必填参数user_ip！");
            }
            if (!inputObj.IsSet("execute_time_"))
            {
                throw new KnownException("接口耗时，缺少必填参数execute_time_！");
            }

            inputObj.SetValue("appid", PayConfig.AppId);//公众账号ID
            inputObj.SetValue("mch_id", PayConfig.MchId);//商户号
            inputObj.SetValue("user_ip", WxPayConfig.IP);//终端ip
            inputObj.SetValue("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//商户上报时间	 
            inputObj.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign", inputObj.MakeSign());//签名
            string xml = inputObj.ToXml();

            LogFactory.GetLogger().Info("WxPayApi=>Report request : " + xml);

            string response = HttpService.Post(xml, url, false, timeOut);

            LogFactory.GetLogger().Info("WxPayApi=>Report response : " + response);

            WxPayData result = new WxPayData();
            result.FromXml(response);
            return result;
        }
    }
}
