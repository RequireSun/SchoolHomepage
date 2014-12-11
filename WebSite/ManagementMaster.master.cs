using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagementMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(null == Session["identity"] || string.Empty.Equals(Session["identity"].ToString()))
        {
            Response.Redirect("ManagementLogin.aspx");
        }
    }
}
