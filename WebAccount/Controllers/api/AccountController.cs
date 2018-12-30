using LitJson;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAccount.Controllers.api
{
    public class AccountController : ApiController
    {
        // GET: api/Account
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        public int Get(int id)
        {

            //return AccountDBModel.Instance.Get(id);
            return 0;
        }

        // POST: api/Account
        public RetValue Post([FromBody]string jsonData)
        {

            RetValue ret = new RetValue();
            JsonData jsonStr = JsonMapper.ToObject(jsonData);
            //时间戳
            long t = Convert.ToInt64(jsonStr["t"].ToString());
            //设备标识符
            string deviceIdentifier = jsonStr["deviceIdentifier"].ToString();
            string deviceModel = jsonStr["deviceModel"].ToString();
            //签名
            string sign = jsonStr["sign"].ToString();

            //1、判断时间戳 允许时间差3秒
            if (MFDSAUtil.GetTimestamp() - t > 3)
            {
                ret.HasError = true;
                ret.ErrorMsg = "请求无效";
                return ret;
            }

            //2、验证签名
            string signServer = MFEncryptUtil.Md5(string.Format("{0}:{1}", t, deviceIdentifier));
            if (!signServer.Equals(sign, StringComparison.CurrentCultureIgnoreCase))
            {
                ret.HasError = true;
                ret.ErrorMsg = "请求无效";
                return ret;
            }

            int type = Convert.ToInt32(jsonStr["Type"].ToString());
            string userName = jsonStr["UserName"].ToString();
            string pwd = jsonStr["Pwd"].ToString();
            string channelId = jsonStr["channelId"].ToString();
            if (type == 0)
            {
                //注册
                MFReturnValue<int> retValue = AccountCacheModel.Instance.Register(userName, pwd, channelId, deviceIdentifier, deviceModel);
                ret.HasError = retValue.HasError;
                ret.ErrorMsg = retValue.Message;

                int userId = retValue.Value;
                AccountEntity entity = AccountCacheModel.Instance.GetEntity(userId);

                ret.Value = JsonMapper.ToJson(new RetAccountEntity(entity));


            }
            else
            {
                //登陆
                AccountEntity entity = AccountCacheModel.Instance.LogOn(userName, pwd, deviceIdentifier, deviceModel);
                if (entity != null)
                {
                    ret.Value = JsonMapper.ToJson(new RetAccountEntity(entity));
                }
                else
                {
                    ret.HasError = true;
                    ret.ErrorMsg = "账户不存在";
                }
            }
            return ret;

        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
