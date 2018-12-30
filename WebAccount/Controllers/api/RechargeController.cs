using LitJson;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web.Http;

namespace WebAccount.Controllers.api
{
    public class RechargeController : ApiController
    {
        public string Get(string orderId, int gameServerId, int roleId)
        {
            return string.Format("{0}:{1}:{2}", orderId, gameServerId, roleId);
        }

        public RetValue Post([FromBody]string jsonStr)
        {
            RetValue ret = new RetValue();

            JsonData jsonData = JsonMapper.ToObject(jsonStr);

            //时间戳
            long t = Convert.ToInt64(jsonData["t"].ToString());
            string deviceIdentifier = jsonData["deviceIdentifier"].ToString();
            string deviceModel = jsonData["deviceModel"].ToString();
            string sign = jsonData["sign"].ToString();

            //1.判断时间戳 如果大于3秒 直接返回错误
            if (MFDSAUtil.GetTimestamp() - t > 3)
            {
                ret.HasError = true;
                ret.ErrorMsg = "请求无效";
                return ret;
            }

            //2.验证签名
            string signServer = MFEncryptUtil.Md5(string.Format("{0}:{1}", t, deviceIdentifier));
            if (!signServer.Equals(sign, StringComparison.CurrentCultureIgnoreCase))
            {
                ret.HasError = true;
                ret.ErrorMsg = "请求无效";
                return ret;
            }

            //订单号 付费服务器识别码_玩家账号_要充值到哪个GameServerId_角色ID_充值的产品Id_时间
            string orderId = jsonData["orderId"].ToString();

            string[] arr = orderId.Split('_');
            if (arr.Length == 6)
            {
                //1.记录充值日志
                RechargeLogEntity rechargeLogEntity = new RechargeLogEntity();
                rechargeLogEntity.AccountId = arr[1].ToInt();
                string channelId = AccountCacheModel.Instance.GetEntity(rechargeLogEntity.AccountId).ChannelId;

                rechargeLogEntity.ChannelId = channelId;
                rechargeLogEntity.GameServerId = arr[2].ToInt();
                rechargeLogEntity.RoldId = arr[3].ToInt();
                rechargeLogEntity.RechargeProductId = arr[4].ToString();
                rechargeLogEntity.OrderId = orderId;
                rechargeLogEntity.CreateTime = DateTime.Now;

                RechargeLogCacheModel.Instance.Create(rechargeLogEntity);


                //2.找到对应的游戏服
                int gameServerId = arr[2].ToInt();

                GameServerEntity entity = GameServerCacheModel.Instance.GetEntity(gameServerId);
                if (entity != null)
                {
                    //发送socket请求 给游戏服
                    Socket rechargeServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    rechargeServer.Connect(new IPEndPoint(IPAddress.Parse(entity.Ip), entity.Port));
                    string str = string.Format("{0}_{1}_{2}", channelId, arr[3], arr[4]);
                    rechargeServer.Send(System.Text.UTF8Encoding.UTF8.GetBytes(str));
                }
                else
                {
                    ret.HasError = true;
                    ret.ErrorMsg = "充值失败";
                }
            }
            else
            {
                ret.HasError = true;
                ret.ErrorMsg = "充值失败";
            }
            return ret;
        }
    }
}