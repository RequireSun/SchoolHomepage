using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// InformationDAO 的摘要说明
/// </summary>
public class InformationDAO : BasicDAO
{
    public DataSet GetInformation(int categoryId) 
    {
        if(1 > categoryId)
        {
            return new DataSet();
        }

        string sql = "SELECT * FROM View_Information_List WHERE category_id = @CategoryId";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@CategoryId", SqlDbType.Int, -1, categoryId) };

        return base.GetDataSet(sql, parameters);
    }

    public string GetInformationName(int informationId)
    {
        if (1 > informationId)
        {
            return string.Empty;
        }

        string sql = "SELECT name FROM category WHERE id = @InformationId";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@InformationId", SqlDbType.Int, -1, informationId) };

        return base.GetSingleDataInColumn(sql, parameters, "name");
    }

    public int UpdateInformation(int informationId, string inArticle)
    {
        if (1 > informationId)
        {
            return 0;
        }
        string article = (null == inArticle) ? string.Empty : inArticle;

        string procedureName = "Information_Modify";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@Category_Id", SqlDbType.Int, -1, informationId),
                                      BasicDAO.MakeInParameter("@Article", SqlDbType.NVarChar, article.Length, article) };

        return Convert.ToInt32(base.ExecStoredProcedure(procedureName, parameters));
    }
}