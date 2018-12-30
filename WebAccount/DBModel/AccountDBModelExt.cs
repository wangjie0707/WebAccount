using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public partial class AccountDBModel
{

    /// <summary>
    /// 玩家注册
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <param name="channelId">渠道号</param>
    /// <returns></returns>
    public MFReturnValue<int> Register(string userName,string pwd,string channelId, string deviceIdentifier, string deviceModel) {
        MFReturnValue<int> retValue = new MFReturnValue<int>();

        //1、验证用户名是否存在

        //2、如果不存在添加数据

        using (SqlConnection conn = new SqlConnection(DBConn.DBAccount))
        {
            conn.Open();

            //开始事物的目的，只打开一次数据库
            SqlTransaction trans = conn.BeginTransaction();

            List<AccountEntity> lst = GetListWithTran(this.TableName,"Id","UserName='"+ userName  + "'" , trans: trans,isAutoStatus:false);

            if (lst == null || lst.Count == 0)
            {
                //说明用户不存在
                AccountEntity entity = new AccountEntity();
                entity.Status = EnumEntityStatus.Released;
                entity.UserName = userName;
                entity.Pwd = MFEncryptUtil.Md5(pwd);
                entity.ChannelId = channelId;
                entity.LastLogOnServerTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.DeviceIdentifier = deviceIdentifier;
                entity.DeviceModel = deviceModel;

                MFReturnValue<object> ret = this.Create(trans, entity);

                if (!ret.HasError)
                {
                    retValue.Value = (int)ret.OutputValues["Id"];
                    trans.Commit();
                }
                else
                {
                    retValue.HasError = true;
                    retValue.Message = "用户名已经存在";
                    trans.Rollback();
                }
            }
            else {
                //说明存在
                retValue.HasError = true;
                retValue.Message = "用户名已经存在";
            }


        };

        return retValue;
    }

    /// <summary>
    /// 玩家登陆
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="pwd"></param>
    /// <returns></returns>
    public AccountEntity LogOn(string userName, string pwd, string deviceIdentifier, string deviceModel) {

        string condition = string.Format("[UserName]='{0}' and [Pwd]='{1}'",userName, MFEncryptUtil.Md5(pwd));
        AccountEntity entity = this.GetEntity(condition);

        if (entity!=null)
        {
            entity.DeviceIdentifier = deviceIdentifier;
            entity.DeviceModel = deviceModel;
            this.Update(entity);
        }

        return entity;
    }


}