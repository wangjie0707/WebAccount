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
public partial class RechargeLogDBModel : MFAbstractSQLDBModel<RechargeLogEntity>
{
    #region RechargeLogDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private RechargeLogDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static RechargeLogDBModel instance = null;
    public static RechargeLogDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new RechargeLogDBModel();
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
        get { return "RechargeLog"; }
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
                _ColumnList = new List<string> { "Id", "Status", "ChannelId", "AccountId", "GameServerId", "RoldId", "RechargeProductId", "OrderId", "CreateTime" };
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
    protected override SqlParameter[] ValueParas(RechargeLogEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@ChannelId", entity.ChannelId) { DbType = DbType.String },
                new SqlParameter("@AccountId", entity.AccountId) { DbType = DbType.Int32 },
                new SqlParameter("@GameServerId", entity.GameServerId) { DbType = DbType.Int32 },
                new SqlParameter("@RoldId", entity.RoldId) { DbType = DbType.Int32 },
                new SqlParameter("@RechargeProductId", entity.RechargeProductId) { DbType = DbType.String },
                new SqlParameter("@OrderId", entity.OrderId) { DbType = DbType.String },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
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
    protected override RechargeLogEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        RechargeLogEntity entity = new RechargeLogEntity();
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
                        entity.ChannelId = Convert.ToString(reader["ChannelId"]);
                    break;
                case "accountid":
                    if (!(reader["AccountId"] is DBNull))
                        entity.AccountId = Convert.ToInt32(reader["AccountId"]);
                    break;
                case "gameserverid":
                    if (!(reader["GameServerId"] is DBNull))
                        entity.GameServerId = Convert.ToInt32(reader["GameServerId"]);
                    break;
                case "roldid":
                    if (!(reader["RoldId"] is DBNull))
                        entity.RoldId = Convert.ToInt32(reader["RoldId"]);
                    break;
                case "rechargeproductid":
                    if (!(reader["RechargeProductId"] is DBNull))
                        entity.RechargeProductId = Convert.ToString(reader["RechargeProductId"]);
                    break;
                case "orderid":
                    if (!(reader["OrderId"] is DBNull))
                        entity.OrderId = Convert.ToString(reader["OrderId"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
