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
    public class GameServerController : ApiController
    {
        // GET: api/GameServer
        public IEnumerable<string> Get()
        {
            //for (int i = 1; i <= 38; i++)
            //{
            //    GameServerCacheModel.Instance.Create(
            //        new GameServerEntity()
            //        {
            //            Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released,
            //            RunStatus = 1,
            //            IsCommand = false,
            //            IsNew = false,
            //            Name = "测试服" + i,
            //            Ip = "192.168.31.190",
            //            Port = 1000 + i,
            //            CreateTime = DateTime.Now,
            //            UpdateTime = DateTime.Now
            //        });
            //}

            return new string[] { "value1", "value2" };
        }

        // GET: api/GameServer/5
        public List<RetGameServerEntity> Get(int id)
        {
            //return GameServerCacheModel.Instance.GetServerPageList();
            //return GameServerCacheModel.Instance.GetGameServerList(id);
            return null;
        }

        // POST: api/GameServer
        public object Post([FromBody]string jsonData)
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

            if (type == 0)
            {
                string channelId = jsonStr["ChannelId"].ToString();
                string innerVersion = jsonStr["InnerVersion"].ToString();

                //先获取渠道状态 根据渠道状态 来加载不同的区服
                ChannelEntity entity = ChannelCacheModel.Instance.GetEntity(string.Format("[ChannelId]={0} and [InnerVersion]={1}", channelId, innerVersion));
                if (entity == null)
                {
                    ret.HasError = true;
                    ret.ErrorMsg = "渠道号不存在";
                }

                //获取页签
                return GameServerCacheModel.Instance.GetGameServerPageList(string.Format("[ChannelStatus]={0}", entity.ChannelStatus));
            }
            else if (type == 1)
            {
           
                string channelId = jsonStr["ChannelId"].ToString();
                string innerVersion = jsonStr["InnerVersion"].ToString();

                //先获取渠道状态 根据渠道状态 来加载不同的区服
                ChannelEntity entity = ChannelCacheModel.Instance.GetEntity(string.Format("[ChannelId]={0} and [InnerVersion]={1}", channelId, innerVersion));
                if (entity == null)
                {
                    ret.HasError = true;
                    ret.ErrorMsg = "渠道号不存在";
                }

                int pageIndex = int.Parse(jsonStr["pageIndex"].ToString());
                //获取区服列表
                return GameServerCacheModel.Instance.GetGameServerList(pageIndex, string.Format("[ChannelStatus]={0}", entity.ChannelStatus));

            }
            else if (type == 2)
            {
                //更新最后登陆信息
                int userId = int.Parse(jsonStr["userId"].ToString());
                int lastServerId = int.Parse(jsonStr["lastServerId"].ToString());
                string lastServerName = jsonStr["lastServerName"].ToString();

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["Id"] = userId;
                dic["LastLogOnServerId"] = lastServerId;
                dic["LastLogOnServerName"] = lastServerName;
                dic["LastLogOnServerTime"] = DateTime.Now;

                AccountCacheModel
                    .Instance
                    .Update("LastLogOnServerId=@LastLogOnServerId,LastLogOnServerName=@LastLogOnServerName,LastLogOnServerTime=@LastLogOnServerTime", "Id=@Id", dic);

            }

            return ret;
        }

        // PUT: api/GameServer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GameServer/5
        public void Delete(int id)
        {
        }
    }
}
