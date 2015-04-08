using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewsList : System.Web.UI.Page
{
    private string liTags = "<li>{0}</li>";
    private string hrefTags = "<a href='{0}'>{1}</a>";
    private string spanTags = "<span>{0}</span>";
    private string newsListLink = "NewsList.aspx?type={0}&page_request={1}";
    private string newsDetailLink = "NewsDetail.aspx?id={0}";
    public static int pageJumpSize = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            string categoryType = Request.QueryString["type"];  //获取页面传递的type值
            int categoryId = Convert.ToInt32(categoryType);  //转换成int型
            if (null == categoryType || categoryType.Equals(string.Empty))   //传值为空
            {
                this.showFalseMessage("请输入正确的请求代号！");
                return;
            }

            string pageRequestString = Request.QueryString["page_request"];   //获取页面传值的page_request值
            int pageRequest = Convert.ToInt32(pageRequestString);
            if (null == pageRequestString || pageRequestString.Equals(string.Empty))
            {
                this.showFalseMessage("请输入正确的页码！");
                return;   //告诉计算机执行完毕，可以没有return
            }

            ServiceNews serviceNews = new ServiceNews();  //调用webservice
            int pageCount = serviceNews.GetNewsPageCountCategory(categoryId, 20);  //用存储过程获得子类page数
            if (0 == pageCount)
            {
                this.showOverflowMessage("该栏目目前还没有资源！");
                this.initPageNumber(pageCount, pageRequest, categoryId);
                return;
            }
            this.initPageNumber(pageCount, pageRequest, categoryId);

            DataSet dataset = serviceNews.GetSingleCategoryNewsListWithPageNumber(categoryId, 20, pageRequest);  //用存储过程获得新闻列表
            if (null == dataset || 0 == dataset.Tables.Count || 0 == dataset.Tables[0].Rows.Count)
            {
                this.showOverflowMessage("页码超出范围！");
                return;
            }
            this.initNewsList(dataset.Tables[0]);
        }
    }

    //显示错误信息
    private void showFalseMessage(string message)
    {
        this.failure_div.Visible = true;
        this.success_div.Visible = false;

        this.failure_div.InnerText = message;
    }

    //显示越界信息
    private void showOverflowMessage(string message)
    {
        this.failure_div.Visible = false;
        this.success_div.Visible = true;
        this.overflow_div.Visible = true;
        this.news_div.Visible = false;

        this.overflow_div.InnerText = message;
    }

    //显示页面信息列表
    private void initNewsList(DataTable dataTable) 
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach(DataRow dr in dataTable.Rows)
        {
            stringBuilder.Append(string.Format(liTags,       //liTags = "<li>{0}</li>";
                                 string.Format(hrefTags,      // hrefTags ="<a href='{0}'>{1}</a>";
                                 string.Format(newsDetailLink,dr["id"].ToString()), dr["title"].ToString()) + Convert.ToDateTime(dr["update_time"]).ToShortDateString()));
        }                                     // newsDetailLink = "NewsDetail.aspx?id={0}";

        this.news_list.InnerHtml = stringBuilder.ToString();
    }

    //显示上下页跳转设置
    private void initPageNumber(int pageCount, int pageCurrent, int typeNumber) 
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (1 < pageCurrent)
        {
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, 1), "首页"));
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, pageCurrent - 1), "上一页"));
        }
        else 
        {
            stringBuilder.Append(string.Format(spanTags, "首页"));
            stringBuilder.Append(string.Format(spanTags, "上一页"));
        }

        for (int i = Math.Min(pageCurrent - 1, pageJumpSize); 0 < i; --i)
        {
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, pageCurrent - i), (pageCurrent - i).ToString()));
        }

        stringBuilder.Append(string.Format(spanTags, pageCurrent));

        for (int i = 1; Math.Min(pageCount - pageCurrent, pageJumpSize) >= i; ++i)
        {
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, pageCurrent + i), (pageCurrent + i).ToString()));
        }

        if (pageCount > pageCurrent)
        {
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, pageCurrent + 1), "下一页"));
            stringBuilder.Append(string.Format(hrefTags, string.Format(newsListLink, typeNumber, pageCount), "尾页"));
        }
        else
        {
            stringBuilder.Append(string.Format(spanTags, "下一页"));
            stringBuilder.Append(string.Format(spanTags, "尾页"));
        }

        this.page_select.InnerHtml = stringBuilder.ToString();
    }
}