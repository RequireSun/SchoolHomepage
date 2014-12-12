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
            this.OutlineRadioButtonList.Items.FindByValue(outline_id.ToString()).Selected = true;

            DataSet categoryDataset = newsDao.GetCategory(outline_id,"category");
            this.CategoryDropDownList.DataSource = categoryDataset;
            this.CategoryDropDownList.DataTextField = "name";
            this.CategoryDropDownList.DataValueField = "id";
            this.CategoryDropDownList.DataBind();
            this.CategoryDropDownList.Items.FindByValue(infoDatarow["category_id"].ToString()).Selected = true;

            this.TitleTextBox.Text = infoDatarow["title"].ToString();
            this.ArticleTextBox.Text = infoDatarow["article"].ToString();
        }
    }

    private void showFalseMessage(string message)
    {
        this.failure_div.Visible = true;
        this.success_div.Visible = false;

        this.failure_div.InnerText = message;
    }

    protected void OutlineRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string categoryIdString = OutlineRadioButtonList.SelectedValue;
        DataSet categoryDataset = null;
        if (null == Cache["category"])
        {
            NewsDAO newsDao = new NewsDAO();
            categoryDataset = newsDao.GetAllCategory();
            Cache.Insert("category", categoryDataset, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
        }
        else
        {
            categoryDataset = Cache["category"] as DataSet;
            if (null == categoryDataset)
            {
                NewsDAO newsDao = new NewsDAO();
                categoryDataset = newsDao.GetAllCategory();
                Cache.Insert("category", categoryDataset, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        this.CategoryDropDownList.DataSource = categoryDataset.Tables[categoryIdString];
        this.CategoryDropDownList.DataTextField = "name";
        this.CategoryDropDownList.DataValueField = "id";
        this.CategoryDropDownList.DataBind();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        string idString = Request.QueryString["id"];
        int idInt = Convert.ToInt32(idString);
        if (null == idString || idString.Equals(string.Empty) || 1 > idInt)
        {
            UtilFunctions.AlertBox("请输入正确的请求代号！",Page);
            return;
        }

        string title = TitleTextBox.Text;
        string article = ArticleTextBox.Text;

        NewsDAO newsDao = new NewsDAO();
        int categoryId = Convert.ToInt32(this.CategoryDropDownList.SelectedValue);
        if (1 > categoryId)
        {
            UtilFunctions.AlertBox("请选择正确的文章类型！", Page);
            return;
        }

        if (-1 == newsDao.EditNews(idInt, categoryId, title, article))
        {
            UtilFunctions.AlertBox("修改失败！", Page);
        }
        else
        {
            UtilFunctions.AlertBox("修改成功！", Page);
        }
    }
}