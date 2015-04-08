using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    private string liTags = "<li>{0}</li>";
    private string hrefTags = "<a href='{0}'>{1}</a>";
    private string newsDetailLink = "NewsDetail.aspx?id={0}";

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            DataSet newsDataset = new DataSet();
            DataSet notifyDataset = new DataSet();

            ServiceNews serviceNews = new ServiceNews();
            newsDataset = serviceNews.GetSingleOutlineNewsListWithPageNumber(2, 5, 1);
            notifyDataset = serviceNews.GetSingleOutlineNewsListWithPageNumber(3, 5, 1);

            if (null == newsDataset || 0 == newsDataset.Tables.Count)
            {
                this.news_list.InnerText = "目前尚未有新闻";
            }
            else
            {
                this.news_list.InnerHtml = getDetailList(newsDataset.Tables[0]);
            }

            if (null == notifyDataset || 0 == notifyDataset.Tables.Count)
            {
                this.news_list.InnerText = "目前尚未有通知";
            }
            else
            {
                this.notify_list.InnerHtml = getDetailList(notifyDataset.Tables[0]);
            }

        }
    }

    private string getDetailList(DataTable datatable)
    {
        StringBuilder builder = new StringBuilder();

        foreach(DataRow dr in datatable.Rows)
        {
            builder.Append(string.Format(liTags,
                           string.Format(hrefTags,
                           string.Format(newsDetailLink,dr["id"].ToString()),dr["title"].ToString())) + Convert.ToDateTime(dr["update_time"]).ToShortDateString());
        }

        return builder.ToString();
    }
}