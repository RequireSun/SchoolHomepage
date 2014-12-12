using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ManagementNewsModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string idString = Request.QueryString["id"];
            int idInt = Convert.ToInt32(idString);
            if (null == idString || idString.Equals(string.Empty) || 1 > idInt)
            {
                this.showFalseMessage("请输入正确的请求代号！");
                return;
            }

            NewsDAO newsDao = new NewsDAO();
            DataSet infoDataset = newsDao.GetNewsInfo(idInt);
            if (null == infoDataset || 0 == infoDataset.Tables.Count || 0 == infoDataset.Tables[0].Rows.Count)
            {
                this.showFalseMessage("未找到所请求的新闻！");
                return;
            }
            DataRow infoDatarow = infoDataset.Tables[0].Rows[0];

            int outline_id = Convert.ToInt32(infoDatarow["outline_id"].ToString());
            OutlineRadioButtonList.Items[outline_id-2].Selected = true;

            DataSet categoryDataset = newsDao.GetCategory(outline_id,"category");
            DropDownList1.DataSource = categoryDataset;
            DropDownList1.DataTextField = "name";
            DropDownList1.DataValueField = "id";
            DropDownList1.DataBind();
            for (int i = 0; i < DropDownList1.Items.Count; i++)
            {
                if (DropDownList1.Items[i].Text.Equals(infoDatarow["name"]))
                    DropDownList1.SelectedIndex = i;
            }

            titleTextBox.Text = infoDatarow["title"].ToString();
            articleTextBox.Text = infoDatarow["article"].ToString();
        }
    }

    private void showFalseMessage(string message)
    {
        this.failure_div.Visible = true;
        this.success_div.Visible = false;

        this.failure_div.InnerText = message;
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int categoryID = OutlineRadioButtonList.SelectedIndex;
        DataSet ds = null;
        if (null == Cache["category"])
        {
            NewsDAO newsDao = new NewsDAO();
            ds = newsDao.GetAllCategory();
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
        int id = Convert.ToInt32(Request.QueryString["id"]);
        NewsDAO newsDao = new NewsDAO();
        int categoryID = Convert.ToInt32(newsDao.GetCategoryID(DropDownList1.SelectedValue).Tables[0].Rows[0]["id"].ToString());

        if (newsDao.EditNews(id, categoryID, title, article) == -1)
            promptLabel.Text = "修改失败";
        else
            promptLabel.Text = "修改成功";
    }
}