using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// UtilFunctions 的摘要说明
/// </summary>
public class UtilFunctions
{
    public static void AlertBox(string AlertText, Page page)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script type='text/javascript'>alert('" + AlertText + "')</script>");
    }
}