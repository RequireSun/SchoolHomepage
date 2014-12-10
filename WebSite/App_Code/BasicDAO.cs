using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// BasicDAO是用来与数据库进行交互的基础类
/// </summary>
public class BasicDAO
{
    private SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    #region Open()开启数据库连接&Close()关闭数据库连接
    /// <summary>
    /// 开启数据库连接，若连接不存在，新建该连接并打开
    /// </summary>
    private void Open()
    {
        if (null == connection)
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
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
    private void Close()
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
    private SqlCommand GetCommand(string sql, SqlParameter[] parameters)
    {
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

    #region ExecNonQuery()根据指定的参数执行命令（用于删除，更新，插入）
    /// <summary>
    /// 根据指定的参数执行命令（用于删除，更新，插入）
    /// </summary>
    /// <param name="sql">输入的sql语句</param>
    /// <param name="parameters">参数，允许为null</param>
    /// <returns>受影响的行数</returns>
    protected int ExecNonQuery(string sql, SqlParameter[] parameters)
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

    #region GetAdapter()根据sql语句和参数获得SqlDataAdapter对象
    /// <summary>
    /// 根据sql语句和参数获得SqlDataAdapter对象
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">SqlParameter数组</param>
    /// <returns>生成的Adapter</returns>
    private SqlDataAdapter GetAdapter(string sql, SqlParameter[] parameters)
    {
        // 创建数据适配器
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);
        // 数据库执行的类型
        dataAdapter.SelectCommand.CommandType = CommandType.Text;

        if (parameters != null)
        {
            foreach (SqlParameter pa in parameters)
            {
                dataAdapter.SelectCommand.Parameters.Add(pa);
            }
        }
        return dataAdapter;
    }
    #endregion

    #region GetDateSet()根据sql语句和参数获取一组数据
    /// <summary>
    /// 返回DataSet的执行函数
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">SqlParameter数组</param>
    /// <param name="tableName">数据存储在DataSet中的目的表名</param>
    /// <returns>存储数据的DataSet</returns>

    protected DataSet GetDateSet(string sql, SqlParameter[] parameters, string tableName)
    {
        SqlDataAdapter adapter = this.GetAdapter(sql, parameters);
        DataSet dataset = new DataSet();

        try
        {
            adapter.Fill(dataset, tableName);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }

        return dataset;
    }

    /// <summary>
    /// 返回DataSet的执行函数 不指定表名
    /// </summary>
    /// <param name="sql">sql语句</param>
    /// <param name="parameters">SqlParameter数组</param>
    /// <returns>存储数据的DataSet</returns>
    protected DataSet GetDataSet(string sql, SqlParameter[] parameters)
    {
        SqlDataAdapter adapter = this.GetAdapter(sql, parameters);
        DataSet dataset = new DataSet();

        try
        {
            adapter.Fill(dataset);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }

        return dataset;
    }
    #endregion

    #region GetSingleData()返回指定行相应列的内容
    /// <summary>
    /// 返回指定行相应列的内容（常用于查询单条信息）
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="parameters">参数数组</param>
    /// <param name="cowNumber">列的引索</param>
    /// <returns>该位置上的值的字符串形式</returns>
    protected string GetSingleDataInColumn(string sql, SqlParameter[] parameters, int cowNumber)
    {
        DataSet dataset = this.GetDataSet(sql, parameters);

        if (null != dataset && 0 != dataset.Tables.Count && 0 != dataset.Tables[0].Rows.Count)
        {
            return dataset.Tables[0].Rows[0][cowNumber].ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 返回指定行相应列的内容（常用于查询单条信息）
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="parameters">参数数组</param>
    /// <param name="cowName">列的引索</param>
    /// <returns>该位置上的值的字符串形式</returns>
    protected string GetSingleDataInColumn(string sql, SqlParameter[] parameters, string cowName)
    {
        DataSet dataset = this.GetDataSet(sql, parameters);

        if (null != dataset && 0 != dataset.Tables.Count && 0 != dataset.Tables[0].Rows.Count)
        {
            return dataset.Tables[0].Rows[0][cowName].ToString();
        }
        else
        {
            return string.Empty;
        }
    }
    #endregion

    #region GetAllDataInColumn()返回语句结果的相应列的内容的字符串形式的数组
    /// <summary>
    /// 返回指定行相应列的内容组（常用于查询多条信息）
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="parameters">参数数组</param>
    /// <param name="cowName">列的引索</param>
    /// <returns>该位置上的值的字符串形式的数组</returns>
    protected List<string> GetAllDataInColumn(string sql, SqlParameter[] parameters, string cowName)
    {
        DataSet dataset = this.GetDataSet(sql, parameters);
        List<string> result = new List<string>();

        if (null != dataset && 0 != dataset.Tables.Count && 0 != dataset.Tables[0].Rows.Count)
        {
            foreach (DataRow DRC in dataset.Tables[0].Rows)
            {
                result.Add(DRC[cowName].ToString());
            }
        }

        return result;
    }
    #endregion

    #region ClearTable()清空表（请谨慎使用）
    /// <summary>
    /// 清空表（请谨慎使用）
    /// </summary>
    /// <param name="tableName">目标表名</param>
    /// <returns>是否清除成功（若目标表本身就为空，也会返回错误）</returns>
    protected bool ClearTable(string tableName) 
    {
        string sql = "delete from " + tableName;
        int affectedLine = 0;

        try
        {
            affectedLine = ExecNonQuery(sql, null);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        return affectedLine > 0 ? true : false;
    }
    #endregion

    #region CheckExistInDatabase()执行某条语句，并检测结果
    /// <summary>
    /// CheckExistInDatabase()执行某条语句，并检测结果
    /// </summary>
    /// <param name="tableName">目标表名</param>
    /// <returns>返回语句执行结果（若所执行的语句本身就没有返回值，则也会返回错误）</returns>
    protected bool CheckExistInDatabase(string sql, SqlParameter[] parameters)
    {
        SqlCommand command = this.GetCommand(sql, parameters);
        this.Close();
        int count = Convert.ToInt32(command.ExecuteScalar());
        this.Close();

        return count > 0 ? true : false;
    }
    #endregion

    #region ExecStoredProcedure()执行存储过程，并返回结果
    /// <summary>
    /// ExecStoredProcedure()执行存储过程，并返回结果
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="parameters">参数数组</param>
    /// <returns>返回存储过程执行结果</returns>
    protected object ExecStoredProcedure(string procedureName, SqlParameter[] parameters)
    {
        SqlCommand command = GetCommand(procedureName, parameters);
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter parameterReturn = new SqlParameter();
        parameterReturn.ParameterName = "@return";
        parameterReturn.Direction = ParameterDirection.ReturnValue;
        command.Parameters.Add(parameterReturn);
        
        this.Open();
        command.ExecuteNonQuery();
        this.Close();

        return parameterReturn.Value;
    }
    #endregion

    #region ExecStoredProcedureGetDataSet()执行存储过程，并返回结果表
    /// <summary>
    /// ExecStoredProcedureGetDataSet()执行存储过程，并返回结果
    /// </summary>
    /// <param name="sql">传入的sql语句</param>
    /// <param name="parameters">参数数组</param>
    /// <returns>返回存储过程执行结果</returns>
    protected DataSet ExecStoredProcedureGetDataSet(string procedureName, SqlParameter[] parameters)
    {
        SqlDataAdapter adapter = this.GetAdapter(procedureName, parameters);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        DataSet dataset = new DataSet();

        this.Open();
        try
        {
            adapter.Fill(dataset);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
        this.Close();

        return dataset;
    }
    #endregion
}