using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class NewsManagement : System.Web.UI.Page
{
    public NewsManagementDAO dao = new NewsManagementDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = Request.QueryString["id"];
        int id = Int32.Parse(str);
        if (!Page.IsPostBack)
        {
            DataRow dr = dao.getNewsInfo(id).Tables[0].Rows[0];

            int outline_id = Int32.Parse(dr["outline_id"].ToString());
            RadioButtonList1.Items[outline_id-2].Selected = true;

            DataSet ds = dao.getCategory(outline_id,"category");
            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "name";
            DropDownList1.DataBind();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                if (DropDownList1.Items[i].Text.Equals(dr["name"]))
                    DropDownList1.SelectedIndex = i;
            }

            titleTextBox.Text = dr["title"].ToString();
            articleTextBox.Text = dr["article"].ToString();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int categoryID = RadioButtonList1.SelectedIndex;
        DataSet ds = null;
        if (null == Cache["category"])
        {
            ds = dao.getAllCategory();
            Cache.Insert("category", ds, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        else
        {
            ds = (DataSet)Cache["category"];
        }

        DropDownList1.DataSource = ds.Tables[categoryID];
        DropDownList1.DataTextField = "name";
        DropDownList1.DataBind();
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        //Response.Redirect("");
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        string title = titleTextBox.Text;
        string article = articleTextBox.Text;
        int id = Int32.Parse(Request.QueryString["id"]);
        int categoryID = Int32.Parse(dao.getCategoryID(DropDownList1.SelectedValue).Tables[0].Rows[0]["id"].ToString());

        if (dao.editNews(id, categoryID, title, article) == -1)
            promptLabel.Text = "修改失败";
        else
            promptLabel.Text = "修改成功";
    }
}