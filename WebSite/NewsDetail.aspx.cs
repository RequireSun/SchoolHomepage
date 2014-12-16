using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data;
using System.Linq;
using System.Text;
=======
using System.Linq;
>>>>>>> master
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
<<<<<<< HEAD
        if (!Page.IsPostBack)
        {
            string NewsID = Request.QueryString["id"];  //获得页面传值的id值
            int newsID = Convert.ToInt32(NewsID);  //将其转换成int型
            if (null == NewsID || NewsID.Equals(string.Empty))
            {
                this.showFalseMessage("请输入正确的请求代号！");
                return;
            }

            ServiceNews serviceNews = new ServiceNews();//调用webservice
            DataSet dataSet = serviceNews.GetNewsInfo(newsID);
            if (null == dataSet || 0 == dataSet.Tables.Count || 0 == dataSet.Tables[0].Rows.Count)
            {
                this.showFalseMessage("目前尚未有新闻！");
                return;
            }
            init_displaynewsTitle(dataSet.Tables[0]);
            init_displaynewsArticle(dataSet.Tables[0]);
        }

    }

    //显示错误信息
    private void showFalseMessage(string message)
    {
        this.failure_div.Visible = true;
        this.success_div.Visible = false;

        this.failure_div.InnerText = message;
    }

    //显示是否越界
    private void showOverflowMessage(string message)
    {
        this.failure_div.Visible = false;
        this.success_div.Visible = true;
        this.overflow_div.Visible = true;
        this.news_div.Visible = false;

        this.overflow_div.InnerText = message;
    }
    //显示新闻的题目
    public void init_displaynewsTitle(DataTable dataTable)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DataRow dr in dataTable.Rows)
        {
            stringBuilder.Append(dr["title"].ToString());
        }
        this.news_title.InnerHtml = stringBuilder.ToString();
    }
    //显示新闻的内容
    public void init_displaynewsArticle(DataTable dataTable)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (DataRow dr in dataTable.Rows)
        {
            stringBuilder.Append(dr["article"].ToString());
        }
        this.news_article.InnerHtml = stringBuilder.ToString();
=======

>>>>>>> master
    }
}