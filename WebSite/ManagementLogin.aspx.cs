using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagementLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        SupervisorDAO supervisorDao = new SupervisorDAO();
        string supervisorId = supervisorDao.IsSupervisor(this.AccountTextBox.Text.Trim(), this.PasswordTextBox.Text.Trim());

        if (null == supervisorId || supervisorId.Equals(string.Empty))
        {
            this.ResultLable.Text = "账号不存在或密码错误，请核实后重新进行登录！";
        }
        else 
        {
            Session["identity"] = supervisorId;
            Response.Redirect("ManagementWelcome.aspx");
        }
    }
}