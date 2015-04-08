using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    int maxSearchSize = 50;
    int minSearchSize = 2;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = masterTextBox.Text;
        if (str.Equals(String.Empty))
        {
            masterLabel.Text = "输入内容不能为空！";
            return;
        }
        if (str.Length < minSearchSize)
        {
            masterLabel.Text = "输入内容太短!";
            return;
        }
        if (str.Length > maxSearchSize)
        {
            masterLabel.Text = "输入内容太长！";
            return;
        }
        Response.Redirect("NewsSearch.aspx?search_content=" + str + "&search_type=" + 2 + "&page_request=" + 1);
    }
}
