using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ManagementNewsSearch : System.Web.UI.Page
{
    private static int pageDefaultSize = 20;
    private static int pageJumpSize = 4;
    private static int minSearchSize = 2;
    private static int maxSearchSize = 50;
    private enum SearchType
    {
        Title = 0,
        Article = 1,
        TitleAndArticle = 2
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["page_request"] != null && Request.QueryString["search_type"] != null && Request.QueryString["search_content"] != null)
            {
                string str = Request.QueryString["search_content"];
                if (str.Equals(String.Empty))
                {
                    this.showFalseMessage("输入内容不能为空！");
                    return;
                }
                if (str.Length < minSearchSize)
                {
                    this.showFalseMessage("输入内容太短！");
                    return;
                }
                if (str.Length > maxSearchSize)
                {
                    this.showFalseMessage("输入内容太长！");
                    return;
                }
                TextBox1.Text = str;

                string pageRequestedString = Request.QueryString["page_request"];
                if (pageRequestedString.Equals(String.Empty))
                {
                    this.showFalseMessage("页码输入不正确！");
                    return;
                }
                int pageRequested = Int32.Parse(pageRequestedString);
                if (pageRequested < 1)
                {
                    this.showFalseMessage("页码输入不正确！");
                    return;
                }
          
                SearchType type = (SearchType)Int32.Parse(Request.QueryString["search_type"]);
                DropDownList1.SelectedIndex = (int)type;

                NewsDAO dao = new NewsDAO();
                int pageSize = 0;
                DataSet ds = new DataSet();
                switch (type)
                { 
                    case SearchType.Article:
                        pageSize = dao.GetNewsSizeByArticle(str, pageDefaultSize);
                        ds = dao.SearchNewsByArticle(str, pageDefaultSize, pageRequested);
                        initList(ds, pageSize);
                        break;
                    case SearchType.Title:
                        pageSize = dao.GetNewsSizeByTitle(str, pageDefaultSize);
                        ds = dao.SearchNewsByTitle(str, pageDefaultSize, pageRequested);
                        initList(ds, pageSize);
                        break;
                    case SearchType.TitleAndArticle:
                        pageSize = dao.GetNewsSizeByTitleAndArticle(str, pageDefaultSize);
                        ds = dao.SearchNewsByTitleAndArticle(str, pageDefaultSize, pageRequested);
                        initList(ds, pageSize);
                        break;
                }
            }
        }
    }

    private void initList(DataSet ds, int pageSize)
    {
        if (pageSize == 0 || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            this.showOverflowMessage("没有搜索到相关信息！");
        }
        else
        {
            string str = Request.QueryString["search_content"];
            int pageRequested = Int32.Parse(Request.QueryString["page_request"]);
            SearchType type = (SearchType)Int32.Parse(Request.QueryString["search_type"]);

            StringBuilder builder = new StringBuilder();
            if (1 < pageRequested)
            {
                this.pageNumberHref(builder, type, 1, str, "首页");
                this.pageNumberHref(builder, type, pageRequested - 1,str, "上一页");
            }
            else
            {
                builder.Append("<span>首页</span>");
                builder.Append("<span>上一页</span>");
            }

            for (int i = Math.Min(pageRequested - 1, pageJumpSize); 0 < i; --i)
            {
                this.pageNumberHref(builder, type, pageRequested - i, str, (pageRequested - i).ToString());
            }

            builder.Append("<span>");
            builder.Append(pageRequested.ToString());
            builder.Append("</span>");

            for (int i = 1; Math.Min(pageSize - pageRequested, pageJumpSize) >= i; ++i)
            {
                this.pageNumberHref(builder, type, pageRequested + i, str, (pageRequested + i).ToString());
            }

            if (pageSize > pageRequested)
            {
                this.pageNumberHref(builder, type, pageRequested + 1, str, "下一页");
                this.pageNumberHref(builder, type, pageSize, str, "尾页");
            }
            else
            {
                builder.Append("<span>下一页</span>");
                builder.Append("<span>尾页</span>");
            }

            this.page_select.InnerHtml = builder.ToString();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                addNewsToList(dr["id"].ToString(), dr["title"].ToString(), Convert.ToDateTime(dr["update_time"]).ToShortDateString());
            }
        }
    }
    private void pageNumberHref(StringBuilder builder, SearchType type, int pageRequested, string str, string pageName)
    {
        builder.Append("<a href='ManagementNewsSearch.aspx?search_content=");
        builder.Append(str);
        builder.Append("&search_type=");
        builder.Append((int)type);
        builder.Append("&page_request=");
        builder.Append(pageRequested);
        builder.Append("'>");
        builder.Append(pageName);
        builder.Append("</a>");
    }

    private void addNewsToList(string id, string title, string updateTime)
    {
        this.news_list.InnerHtml += "<li><a href='ManagementNewsModify.aspx?id=" + id + "'>" + title + "</a>" + updateTime + "</li>";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = TextBox1.Text;
        if (str == null || str.Equals(String.Empty))
        {
            this.showFalseMessage("输入内容不能为空！");
            return;
        }
        if (str.Length < minSearchSize)
        {
            this.showFalseMessage("输入内容太短！");
            return;
        }
        if (str.Length > maxSearchSize)
        {
            this.showFalseMessage("输入内容太长！");
            return;
        }

        SearchType type = (SearchType)DropDownList1.SelectedIndex;
        Response.Redirect("ManagementNewsSearch.aspx?search_content=" + str + "&search_type=" + (int)type + "&page_request=" + 1);
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
}