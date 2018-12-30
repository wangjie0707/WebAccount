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
    public class InitController : ApiController
    {
        public string Get()
        {

            return string.Empty;
            //return MFDSAUtil.GetTimestamp();
        }

        public RetValue Post([FromBody]string jsonStr)
        {
            RetValue ret = new RetValue();
            JsonData jsonData = JsonMapper.ToObject(jsonStr);


            string channelId = jsonData["ChannelId"].ToString();
            string innerVersion = jsonData["InnerVersion"].ToString();

            ChannelEntity entity = ChannelCacheModel.Instance.GetEntity(string.Format("[ChannelId]={0} and [InnerVersion]={1}", channelId, innerVersion));
            if (entity == null)
            {
                ret.HasError = true;
                ret.ErrorMsg = "渠道号不存在";
            }
            else
            {
                JsonData data = new JsonData();

                data["ServerTime"] = MFDSAUtil.GetTimestamp();
                data["SourceUrl"] = entity.SourceUrl;
                data["RechargeUrl"] = entity.RechargeUrl;
                data["TDAppId"] = entity.TDAppId;
                data["IsOpenTD"] = entity.IsOpenTD;
                data["PayServerNo"] = entity.PayServerNo;

                ret.Value = JsonMapper.ToJson(data);
            }

            return ret;
        }
    }
}
