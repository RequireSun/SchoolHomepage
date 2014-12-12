using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// NewsDAO 的摘要说明
/// </summary>
public class NewsDAO : BasicDAO
{
    public DataSet GetSingleCategoryNewsListWithPageNumber(int categoryId, int pageSize, int pageRequest)
    {
        if (1 > categoryId || 1 > pageSize || 1 > pageRequest)
        {
            return new DataSet();
        }

        string procedureName = "News_Get_List_Category";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@News_Type", SqlDbType.Int, -1, categoryId),
                                      BasicDAO.MakeInParameter("@Page_Size", SqlDbType.Int, -1, pageSize),
                                      BasicDAO.MakeInParameter("@Page_Request", SqlDbType.Int, -1, pageRequest) };

        DataSet result = base.ExecStoredProcedureGetDataSet(procedureName, parameters) as DataSet;

        return (null == result) ? new DataSet() : result;
    }

    public int PublishNews(int categoryId, int supervisorId, string title, string article)
    {
        if (1 > categoryId || 1 > supervisorId ||
            null == title || null == article || title.Equals(string.Empty) || article.Equals(string.Empty))
        {
            return 0;
        }

        string procedureName = "News_Publish";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@Category_ID", SqlDbType.Int, -1, categoryId),
                                      BasicDAO.MakeInParameter("@Supervisor_ID", SqlDbType.Int, -1, supervisorId),
                                      BasicDAO.MakeInParameter("@Title", SqlDbType.NVarChar, -1, title),
                                      BasicDAO.MakeInParameter("@Article", SqlDbType.NVarChar, -1, article) };

        return Convert.ToInt32(base.ExecStoredProcedure(procedureName, parameters));
    }

    public int DeleteNews(int newsId)
    {
        if (1 > newsId)
        {
            return 0;
        }

        string procedureName = "News_Delete";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@News_ID", SqlDbType.Int, -1, newsId) };

        return Convert.ToInt32(base.ExecStoredProcedure(procedureName, parameters));
    }

    public int GetNewsPageCount(int categoryId, int pageSize)
    {
        if (1 > categoryId || 1 > pageSize)
        {
            return 0;
        }

        string procedureName = "News_Calculate_Page";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@Category_Type", SqlDbType.Int, -1, categoryId),
                                      BasicDAO.MakeInParameter("@Page_Size", SqlDbType.Int, -1, pageSize) };

        return Convert.ToInt32(base.ExecStoredProcedure(procedureName, parameters));
    }

    public DataSet GetCategory(int categoryId, string name)
    {
        string sql = "select id, name from category where outline_id=@id";
        SqlParameter[] pa = { MakeInParameter("@id", SqlDbType.Int, -1, categoryId) };
        DataSet ds = GetDataSet(sql, pa, name);
        return (null == ds) ? new DataSet() : ds;
    }

    public DataSet GetAllCategory()
    {
        string sql = "SELECT * FROM outline";
        DataTable outlines = base.GetDataTable(sql, null);
        DataSet dataSet = new DataSet();

        foreach (DataRow dr in outlines.Rows)
        {
            sql = "SELECT * FROM category WHERE outline_id = @outlineId";
            SqlParameter[] parameters = { BasicDAO.MakeInParameter("@outlineId", SqlDbType.Int, -1, Convert.ToInt32(dr["id"])) };
            DataTable categoryTable = base.GetDataTable(sql, parameters);
            categoryTable.TableName = dr["id"].ToString();
            dataSet.Tables.Add(categoryTable);
        }

        return dataSet;
    }

    public int EditNews(int newsID, int categoryID, string title, string article)
    {
        if (1 > categoryID || null == title || null == article || title.Equals(string.Empty) || article.Equals(string.Empty))
        {
            return 0;
        }

        string sql = "News_Edit";
        SqlParameter[] parameter = { BasicDAO.MakeInParameter("@News_ID",SqlDbType.Int,-1,newsID),
                                     BasicDAO.MakeInParameter("@Category_ID",SqlDbType.Int,-1,categoryID),
                                     BasicDAO.MakeInParameter("@Title",SqlDbType.NChar,title.Length,title),
                                     BasicDAO.MakeInParameter("@Article",SqlDbType.NVarChar,article.Length,article) };

        return Convert.ToInt32(ExecStoredProcedure(sql, parameter).ToString());
    }

    public DataSet GetCategoryID(string categoryName)
    {
        string sql = "select id from category where name=@categoryName";
        SqlParameter[] pa = { MakeInParameter("@categoryName", SqlDbType.NVarChar, categoryName.Length, categoryName) };
        DataSet ds = GetDataSet(sql, pa);
        return (null == ds) ? new DataSet() : ds;
    }

    public DataSet GetNewsInfo(int newsID)
    {
        string sql = "select category_id, title, article, outline_id, name from news INNER JOIN category on news.category_id = category.id where news.id=@newsID";
        SqlParameter[] pa = { MakeInParameter("@newsID", SqlDbType.Int, -1, newsID) };
        DataSet ds = GetDataSet(sql, pa);
        return (null == ds) ? new DataSet() : ds;
    }
}