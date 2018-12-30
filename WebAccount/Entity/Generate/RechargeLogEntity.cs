using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class RechargeLogEntity : MFAbstractEntity
{
    #region 重写基类属性
    /// <summary>
    /// 主键
    /// </summary>
    public override int? PKValue
    {
        get
        {
            return this.Id;
        }
        set
        {
            this.Id = value;
        }
    }
    #endregion

    #region 实体属性

    /// <summary>
    /// 编号
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public EnumEntityStatus Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ChannelId { get; set; }

    /// <summary>
    ///玩家帐号 
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    ///游戏服编号 
    /// </summary>
    public int GameServerId { get; set; }

    /// <summary>
    ///角色编号 
    /// </summary>
    public int RoldId { get; set; }

    /// <summary>
    ///充值产品编号 
    /// </summary>
    public string RechargeProductId { get; set; }

    /// <summary>
    ///订单号 
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    ///创建时间 
    /// </summary>
    public DateTime CreateTime { get; set; }

    #endregion
}
