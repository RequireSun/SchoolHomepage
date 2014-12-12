using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManagementNews : System.Web.UI.Page
{
    public static int pageJumpSize = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            string categoryType = Request.QueryString["type"];
            int categoryId = Convert.ToInt32(categoryType);
            if (null == categoryType || categoryType.Equals(string.Empty))
            {
                this.showFalseMessage("请输入正确的请求代号！");
                return;
            }

            string pageRequestString = Request.QueryString["page_request"];
            int pageRequest = Convert.ToInt32(pageRequestString);
            if (null == pageRequestString || pageRequestString.Equals(string.Empty))
            {
                this.showFalseMessage("请输入正确的页码！");
                return;
            }

            NewsDAO newsDao = new NewsDAO();
            int pageCount = newsDao.GetNewsPageCount(categoryId, 20);
            if (0 == pageCount)
            {
                this.showOverflowMessage("该栏目目前还没有资源！");
                this.initPageNumber(pageCount, pageRequest, categoryId);
                return;
            }

            DataSet dataset = newsDao.GetSingleCategoryNewsListWithPageNumber(categoryId, 20, pageRequest);
            if (null == dataset || 0 == dataset.Tables.Count || 0 == dataset.Tables[0].Rows.Count)
            {
                this.showOverflowMessage("页码超出范围！");
                return;
            }

            this.initPageNumber(pageCount, pageRequest, categoryId);

            foreach (DataRow dr in dataset.Tables[0].Rows)
            {
                this.addNewsToList(dr["id"].ToString(), dr["title"].ToString(), dr["update_time"].ToString());
            }
        }
    }

    private void showFalseMessage(string message)
    {
        this.failure_div.Visible = true;
        this.success_div.Visible = false;

        this.failure_div.InnerText = message;
    }

    private void showOverflowMessage(string message)
    {
        this.failure_div.Visible = false;
        this.success_div.Visible = true;
        this.overflow_div.Visible = true;
        this.news_div.Visible = false;

        this.overflow_div.InnerText = message;
    }

    private void addNewsToList(string id, string title, string updateTime) 
    {
        this.news_list.InnerHtml += "<li><a href='ManagementNewsModify.aspx?id=" + id + "'>" + title + "</a>" + updateTime + "</li>";
    }

    private void initPageNumber(int pageCount,int pageCurrent, int typeNumber) 
    {
        StringBuilder builder = new StringBuilder();

        if (1 < pageCurrent)
        {
            this.pageNumberHref(builder, typeNumber, 1, "首页");
        }
        else
        {
            builder.Append("首页");
        }

        if (1 < pageCurrent)
        {
            this.pageNumberHref(builder, typeNumber, pageCurrent - 1, "上一页");
        }
        else 
        {
            builder.Append("上一页");
        }

        for (int i = Math.Min(pageCurrent - 1, pageJumpSize); 0 < i; --i)
        {
            this.pageNumberHref(builder, typeNumber, pageCurrent - i, (pageCurrent - i).ToString());
        }

        builder.Append(pageCurrent.ToString());

        for (int i = 1; Math.Min(pageCount - pageCurrent, pageJumpSize) >= i; ++i)
        {
            this.pageNumberHref(builder, typeNumber, pageCurrent + i, (pageCurrent + i).ToString());
        }

        if (pageCount > pageCurrent)
        {
            this.pageNumberHref(builder, typeNumber, pageCurrent + 1, "下一页");
        }
        else
        {
            builder.Append("下一页");
        }

        if (pageCurrent < pageCount)
        {
            this.pageNumberHref(builder, typeNumber, pageCount, "尾页");
        }
        else
        {
            builder.Append("尾页");
        }

        this.page_select.InnerHtml = builder.ToString();
    }

    private void pageNumberHref(StringBuilder builder, int typeNumber, int pageRequest, string pageName)
    {
        builder.Append("<a href='ManagementNews.aspx?type=");
        builder.Append(typeNumber);
        builder.Append("&page_request=");
        builder.Append(pageRequest);
        builder.Append("'>");
        builder.Append(pageName);
        builder.Append("</a>");
    }
}