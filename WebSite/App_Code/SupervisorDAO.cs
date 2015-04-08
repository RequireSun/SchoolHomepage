using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// SupervisorDAO 的摘要说明
/// </summary>
public class SupervisorDAO : BasicDAO
{
    public string IsSupervisor(string alias, string cipher)
    {
        if (null == alias || null == cipher || alias.Equals(string.Empty) || cipher.Equals(string.Empty) || 20 < alias.Length || 20 < cipher.Length)
        {
            return string.Empty;
        }

        string sql = "SELECT id FROM supervisor WHERE alias = @inAlias and cipher = @inCipher";
        SqlParameter[] parameters = { BasicDAO.MakeInParameter("@inAlias", SqlDbType.NVarChar, alias.Length, alias) ,
                                      BasicDAO.MakeInParameter("@inCipher", SqlDbType.NVarChar, cipher.Length, cipher)};

        return base.GetSingleDataInColumn(sql, parameters, "id");
    }
}