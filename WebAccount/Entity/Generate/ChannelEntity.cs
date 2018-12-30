using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// 
/// </summary>
[Serializable]
public partial class ChannelEntity : MFAbstractEntity
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
    public int ChannelId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int InnerVersion { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string SourceUrl { get; set; }

    /// <summary>
    ///充值回调地址 
    /// </summary>
    public string RechargeUrl { get; set; }

    /// <summary>
    ///TD统计帐号 
    /// </summary>
    public string TDAppId { get; set; }

    /// <summary>
    ///是否开启统计 
    /// </summary>
    public byte IsOpenTD { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PayServerNo { get; set; }

    /// <summary>
    ///0=测试服
    ///1=苹果正式服
    ///2=安卓正式服
    /// </summary>
    public byte ChannelStatus { get; set; }

    #endregion
}
