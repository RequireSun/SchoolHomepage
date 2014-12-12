using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// NewsManagement 的摘要说明
/// </summary>
public class NewsManagementDAO : BasicDAO
{
	public NewsManagementDAO()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet getCategory(int categoryId, string name)
    {
        string sql = "select name from category where outline_id=@id";
        SqlParameter[] pa = { MakeInParameter("@id", SqlDbType.Int, -1, categoryId) };
        DataSet ds = GetDataSet(sql, pa, name);
        return (null == ds) ? new DataSet() : ds;
    }

    public DataSet getAllCategory()
    {
        DataSet ds = getCategory(2,"新闻");
        DataTable dt = getCategory(3,"通知").Tables[0].Copy();
        ds.Tables.Add(dt);
        return ds;
    }

    public int editNews(int newsID, int categoryID, string title, string article)
    {
        if (1 > categoryID || null == title || null == article || title.Equals(string.Empty) || article.Equals(string.Empty))
        {
            return 0;
        }

        string sql = "News_Edit";
        SqlParameter[] pa = { MakeInParameter("@News_ID",SqlDbType.Int,-1,newsID),
                            MakeInParameter("@Category_ID",SqlDbType.Int,-1,categoryID),
                            MakeInParameter("@Title",SqlDbType.NChar,title.Length,title),
                            MakeInParameter("@Article",SqlDbType.NVarChar,article.Length,article) };

        return Int32.Parse(ExecStoredProcedure(sql,pa).ToString());
    }

    public DataSet getCategoryID(string categoryName)
    {
        string sql = "select id from category where name=@categoryName";
        SqlParameter[] pa = { MakeInParameter("@categoryName", SqlDbType.NVarChar, categoryName.Length, categoryName) };
        DataSet ds = GetDataSet(sql, pa);
        return (null == ds) ? new DataSet() : ds;
    }

    public DataSet getNewsInfo(int newsID)
    {
        string sql = "select category_id, title, article, outline_id, name from news INNER JOIN category on news.category_id = category.id where news.id=@newsID";
        SqlParameter[] pa = {MakeInParameter("@newsID",SqlDbType.Int,-1,newsID) };
        DataSet ds = GetDataSet(sql, pa);
        return (null == ds) ? new DataSet() : ds;
    }

    
}