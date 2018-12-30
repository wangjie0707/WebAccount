using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// DBModel
/// </summary>
public partial class ChannelDBModel : MFAbstractSQLDBModel<ChannelEntity>
{
    #region ChannelDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private ChannelDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static ChannelDBModel instance = null;
    public static ChannelDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new ChannelDBModel();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region 实现基类的属性和方法

    #region ConnectionString 数据库连接字符串
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    protected override string ConnectionString
    {
        get { return DBConn.DBAccount; }
    }
    #endregion

    #region TableName 表名
    /// <summary>
    /// 表名
    /// </summary>
    protected override string TableName
    {
        get { return "Channel"; }
    }
    #endregion

    #region ColumnList 列名集合
    private IList<string> _ColumnList;
    /// <summary>
    /// 列名集合
    /// </summary>
    protected override IList<string> ColumnList
    {
        get
        {
            if (_ColumnList == null)
            {
                _ColumnList = new List<string> { "Id", "Status", "ChannelId", "InnerVersion", "SourceUrl", "RechargeUrl", "TDAppId", "IsOpenTD", "PayServerNo", "ChannelStatus" };
            }
            return _ColumnList;
        }
    }
    #endregion

    #region ValueParas 转换参数
    /// <summary>
    /// 转换参数
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected override SqlParameter[] ValueParas(ChannelEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@ChannelId", entity.ChannelId) { DbType = DbType.Int32 },
                new SqlParameter("@InnerVersion", entity.InnerVersion) { DbType = DbType.Int32 },
                new SqlParameter("@SourceUrl", entity.SourceUrl) { DbType = DbType.String },
                new SqlParameter("@RechargeUrl", entity.RechargeUrl) { DbType = DbType.String },
                new SqlParameter("@TDAppId", entity.TDAppId) { DbType = DbType.String },
                new SqlParameter("@IsOpenTD", entity.IsOpenTD) { DbType = DbType.Byte },
                new SqlParameter("@PayServerNo", entity.PayServerNo) { DbType = DbType.Int32 },
                new SqlParameter("@ChannelStatus", entity.ChannelStatus) { DbType = DbType.Byte },
                new SqlParameter("@RetMsg", SqlDbType.NVarChar, 255),
                new SqlParameter("@ReturnValue", SqlDbType.Int)
            };
        return parameters;
    }
    #endregion

    #region GetEntitySelfProperty 封装对象
    /// <summary>
    /// 封装对象
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    protected override ChannelEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        ChannelEntity entity = new ChannelEntity();
        foreach (DataRow row in table.Rows)
        {
            var colName = row.Field<string>(0);
            if (reader[colName] is DBNull)
                continue;
            switch (colName.ToLower())
            {
                case "id":
                    if (!(reader["Id"] is DBNull))
                        entity.Id = Convert.ToInt32(reader["Id"]);
                    break;
                case "status":
                    if (!(reader["Status"] is DBNull))
                        entity.Status = (EnumEntityStatus)Convert.ToInt32(reader["Status"]);
                    break;
                case "channelid":
                    if (!(reader["ChannelId"] is DBNull))
                        entity.ChannelId = Convert.ToInt32(reader["ChannelId"]);
                    break;
                case "innerversion":
                    if (!(reader["InnerVersion"] is DBNull))
                        entity.InnerVersion = Convert.ToInt32(reader["InnerVersion"]);
                    break;
                case "sourceurl":
                    if (!(reader["SourceUrl"] is DBNull))
                        entity.SourceUrl = Convert.ToString(reader["SourceUrl"]);
                    break;
                case "rechargeurl":
                    if (!(reader["RechargeUrl"] is DBNull))
                        entity.RechargeUrl = Convert.ToString(reader["RechargeUrl"]);
                    break;
                case "tdappid":
                    if (!(reader["TDAppId"] is DBNull))
                        entity.TDAppId = Convert.ToString(reader["TDAppId"]);
                    break;
                case "isopentd":
                    if (!(reader["IsOpenTD"] is DBNull))
                        entity.IsOpenTD = Convert.ToByte(reader["IsOpenTD"]);
                    break;
                case "payserverno":
                    if (!(reader["PayServerNo"] is DBNull))
                        entity.PayServerNo = Convert.ToInt32(reader["PayServerNo"]);
                    break;
                case "channelstatus":
                    if (!(reader["ChannelStatus"] is DBNull))
                        entity.ChannelStatus = Convert.ToByte(reader["ChannelStatus"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
