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
        string procedureName = "News_Get_List_Category";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@News_Type", SqlDbType.Int, -1, categoryId),
                                      BasicDAO.MakeInParameter("@Page_Size", SqlDbType.Int, -1, pageSize),
                                      BasicDAO.MakeInParameter("@Page_Request", SqlDbType.Int, -1, pageRequest) };

        DataSet result = base.ExecStoredProcedureGetDataSet(procedureName, parameters) as DataSet;

        return (null == result) ? new DataSet() : result;
    }

    public int PublishNews(int categoryId, int supervisorId, string title, string article) 
    {
        if(1 > categoryId || 1 > supervisorId || null == title || null == article || title.Equals(string.Empty) || article.Equals(string.Empty))
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
}