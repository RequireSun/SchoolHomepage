using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagementInformationModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string categoryType = Request.QueryString["id"];
        int categoryId = Convert.ToInt32(categoryType);
        if (null == categoryType || categoryType.Equals(string.Empty) || null == categoryId)
        {
            this.showFalseMessage("请输入正确的请求代号！");
            return;
        }
    }
}