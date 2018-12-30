using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public partial class AccountCacheModel
{

    /// <summary>
    ///  玩家注册
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <param name="channelId">渠道号</param>
    /// <returns></returns>
    public MFReturnValue<int> Register(string userName, string pwd, string channelId,string deviceIdentifier,string deviceModel)
    {
        return this.DBModel.Register(userName, pwd, channelId, deviceIdentifier, deviceModel);
    }

    public AccountEntity LogOn(string userName, string pwd, string deviceIdentifier, string deviceModel)
    {
        return this.DBModel.LogOn(userName, pwd, deviceIdentifier, deviceModel);
    }
}