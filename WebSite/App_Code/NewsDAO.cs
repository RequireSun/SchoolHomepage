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
}