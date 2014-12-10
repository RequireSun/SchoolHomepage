using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SqlTestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NewsDAO newsDao = new NewsDAO();
        this.result_label.Text = newsDao.PublishNews(1, 4, "123", "123").ToString();
    }
}