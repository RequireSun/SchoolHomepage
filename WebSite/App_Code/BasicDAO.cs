using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// BasicDAO 的摘要说明
/// </summary>
public class BasicDAO
{
    private SqlConnection connection = null;
    //public SqlConnection CON { get { return this.connection; } }

    #region Open()开启数据库连接&Close()关闭数据库连接
    /// <summary>
    /// 开启数据库连接，若连接不存在，新建该连接并打开
    /// </summary>
    public void Open()
    {
        if (connection == null)
        {
            connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();
        }
        else if (System.Data.ConnectionState.Closed == connection.State)
        {
            connection.Open();
        }
    }

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void Close()
    {
        if (null != connection)
        {
            if (System.Data.ConnectionState.Open == connection.State)
            {
                connection.Close();
            }
        }
    }
    #endregion

    #region MakeInParameter()传入参数并且转换为SqlParameter类型
    /// <summary>
    /// 转换参数 将sql语句参数封装为SqlParameter对象（方向为输入）   
    /// </summary>
    /// <param name="ParameterName">存储过程名称或命令文本</param>
    /// <param name="DbType">参数类型</param></param>
    /// <param name="Size">参数大小</param>
    /// <param name="Value">参数值</param>
    /// <returns>新的 Parameter 对象</returns>
    public static SqlParameter MakeInParameter(string ParameterName, SqlDbType DbType, int Size, object Value)
    {
        return MakeParameter(ParameterName, DbType, Size, ParameterDirection.Input, Value);
    }

    /// <summary>
    /// 转换参数 将sql语句参数封装为SqlParameter对象（方向由输入确定）   
    /// </summary>
    /// <param name="ParameterName">存储过程名称或命令文本</param>
    /// <param name="DbType">参数类型</param></param>
    /// <param name="Size">参数大小</param>
    /// <param name="Direction">参数方向</param>
    /// <param name="Value">参数值</param>
    /// <returns>新的 Parameter 对象</returns>
    private static SqlParameter MakeParameter(string ParameterName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
    {
        SqlParameter parameter = null;

        if (0 < Size || -1 == Size)
        {
            parameter = new SqlParameter(ParameterName, DbType, Size);
        }
        else
        {
            parameter = new SqlParameter(ParameterName, DbType);
        }

        parameter.Direction = Direction;
        if (ParameterDirection.Output != Direction || null != Value)
        {
            parameter.Value = Value;
        }

        return parameter;
    }
    #endregion

    #region GetCommand()根据命令和参数生成SqlCommand
    /// <summary>
    /// 根据命令和参数生成SqlCommand
    /// </summary>
    /// <param name="sql">输入的sql语句</param>
    /// <param name="para">参数，允许为null</param>
    /// <returns>具有数据库连接和操作的command对象</returns>
    public SqlCommand GetCommand(string sql, SqlParameter[] parameters)
    {
        //this.Open();
        SqlCommand command = new SqlCommand(sql, connection);
        if (parameters != null)
        {
            foreach (SqlParameter p in parameters)
            {
                command.Parameters.Add(p);
            }
        }
        return command;
    }
    #endregion

    #region 执行参数命令文本
    /// <summary>
    /// 根据指定的参数执行命令（用于删除，更新，插入）
    /// </summary>
    /// <param name="sql">输入的sql语句</param>
    /// <param name="parameters">参数，允许为null</param>
    /// <returns>受影响的行数</returns>
    public int ExecNonQuery(string sql, SqlParameter[] parameters)
    {
        int affectedLine = 0;

        this.Open();
        SqlCommand command = this.GetCommand(sql, parameters);
        SqlTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction;

        try
        {
            affectedLine = command.ExecuteNonQuery();
            transaction.Commit();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
        finally
        {
            this.Close();
        }

        return affectedLine;
    }
    #endregion

    #region 返回SqlDataAdapter 对象

    /// <summary>
    /// 返回SqlDataAdapter 对象
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="para">SqlParameter数组</param>
    /// <returns>SqlDataAdapter</returns>

    public SqlDataAdapter ReADP(string sql, SqlParameter[] paras)
    {
        this.Open();
        // 创建数据适配器
        SqlDataAdapter da = new SqlDataAdapter(sql, connection);
        // 数据库执行的类型
        da.SelectCommand.CommandType = CommandType.Text;

        if (paras != null)
        {
            foreach (SqlParameter pa in paras)
            {
                da.SelectCommand.Parameters.Add(pa);
            }
        }
        this.Close();
        return da;
    }

    #endregion

    #region 返回dataset

    /// <summary>
    /// 返回dataset的执行函数
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="para"></param>
    /// <param name="table_name">为返回dataset的表名</param>
    /// <returns></returns>

    public DataSet GetDateSet(string sql, SqlParameter[] paras, string tablename)
    {
        SqlDataAdapter da = this.ReADP(sql, paras);
        DataSet ds = new DataSet();

        try
        {
            da.Fill(ds, tablename);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
        finally
        {
            this.Close();
        }

        return ds;
    }

    /// <summary>
    /// 返回dataset的执行函数  不指定表名
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="para"></param>
    /// <returns></returns>

    public DataSet GetDataSet(string sql, SqlParameter[] para)
    {
        SqlDataAdapter da = this.ReADP(sql, para);
        DataSet ds = new DataSet();

        try
        {
            da.Fill(ds);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
        finally
        {
            this.Close();
        }

        return ds;
    }

    #endregion

    #region 返回指定行相应列的内容

    /// <summary>
    /// 返回指定行相应列的内容
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="para">参数</param>
    /// <param name="cow"> 列的引索</param>
    /// <returns>该位置上的值的字符串形式</returns>

    public string ReString(string sql, SqlParameter[] para, int cow)
    {
        DataSet ds = this.GetDataSet(sql, para);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds.Tables[0].Rows[0][cow].ToString();
            }
            return "";
        }
        else
        {
            return "";
        }
    }

    public List<string> GetAllStringInColumn(string sql, SqlParameter[] para, string cowName)
    {
        DataSet ds = this.GetDataSet(sql, para);
        List<string> res = new List<string>();

        if (ds != null&&ds.Tables[0].Rows.Count != 0)
        {
            foreach(DataRow DRC in ds.Tables[0].Rows)
            {
                res.Add(DRC[cowName].ToString());
            }
        }

        return res;
    }

    #endregion

    public bool ClearTable(string inTableName) 
    {
        string sql = "delete from " + inTableName;
        int i = 0;
        try
        {
            i = ExecNonQuery(sql, null);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
        return i > 0 ? true : false;
    }

    public bool CheckExistInDB(string sql, SqlParameter[] para)
    {
        //this.Open();
        //SqlCommand sc = new SqlCommand(sql, con);
        //sc.Connection.Open();
        //System.Diagnostics.Debug.WriteLine(sql);
        //CloseConnection();

        SqlCommand cmd = this.ReCMD(sql, para);
        int count = Convert.ToInt32(cmd.ExecuteScalar());
        this.Close();

        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}