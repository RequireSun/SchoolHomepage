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
    public string GetInformation(int categoryId) 
    {
        if(null == categoryId || 1 > categoryId)
        {
            return string.Empty;
        }

        string sql = "SELECT article FROM information WHERE category_id = @CategoryId";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@CategoryId", SqlDbType.Int, -1, categoryId) };

        return base.GetSingleDataInColumn(sql, parameters, "article");
    }
}